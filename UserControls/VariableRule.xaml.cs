using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TesisAnaReglas.BussinesLogic;

namespace TesisAnaReglas.UserControls
{
    /// <summary>
    /// Interaction logic for VariableRule.xaml
    /// </summary>
    public partial class VariableRule : Window
    {
        private MainWindow main = null;
        private LinkedList<Variable> varList = new LinkedList<Variable>();
        private LinkedList<Variable> varListA = new LinkedList<Variable>();
        private LinkedList<Cause> cauList = new LinkedList<Cause>();
        public ObservableCollection<Cause> dataC = new ObservableCollection<Cause>();
        public ObservableCollection<Variable> dataV = new ObservableCollection<Variable>();
        public bool inAction = false;
        public VariableCause variable = null;
        private VariableCauseRule wind = null;
        public Variable temp = null;
       
        public VariableRule(MainWindow main, VariableCause vc, VariableCauseRule varW)
        {
            wind = varW;
            this.main = main;
            InitializeComponent();
            if (main.process.ElementAt(main.selectedIndex).varList != null)
            {
                foreach (Variable var in main.process.ElementAt(main.selectedIndex).varList)
                {
                    varListA.AddLast(var);
                }   
            }
            List<Variable> list = new List<Variable>();
            list = varListA.OrderBy(p => p.name).ToList();  //Retorna una lista ordenara por Nombre

            foreach (Variable order in list)
            {
                comboBox_variable.Items.Add(order.name);
            }

            if (vc!=null) {

                variable = vc;
                if (variable.variableList != null)
                {
                    foreach (Variable v in variable.variableList)
                    {
                        dataV.Add(v);
                    }
                    dataGrid.DataContext = dataV;
                }
                if (variable.causeList != null)
                {
                    foreach (Cause c in variable.causeList)
                    {
                        dataC.Add(c);
                    }
                }
            }

            fill_table(main);
        }

        public void fill_table(MainWindow main)
        {
            if (dataV != null)
            {
                dataGrid.DataContext = dataV;
            }
        }

        private void button_acep_Click(object sender, RoutedEventArgs e)
        {
           if (dataC.Count() > 0 )
            {
                if (variable != null)
                {
                    if (dataV.Count() > 0)
                    {
                        foreach (Variable v in dataV)
                        {
                            varList.AddLast(v);
                        }
                        foreach (Cause c in dataC)
                        {
                            cauList.AddLast(c);
                        }

                           variable.variableList = varList;
                           variable.causeList = cauList;
                        
                        if (variable.variableList.Count() == 1)
                        {
                            variable.ruleR = add_rule(variable);

                        }
                        else
                        {
                            variable.ruleR = add_rule2(variable);
                        }
                        Close();
                        wind.fill_table(main);
                    }
                    else
                    {
                      MessageBox.Show("Debe insertar al menos una variable.");
                    }
            }
                else
                {
                    if (dataV.Count() > 0)
                    {
                        foreach (Variable var in dataV)
                        {
                            varList.AddLast(var);
                        }


                        foreach (Cause c in dataC)
                        {
                            cauList.AddLast(c);
                        }
                        bool exist = variable_in_Rule(varList,cauList);
                        if (exist)
                        {
                            MessageBox.Show("Estas variables ya tienen asociadas sus  causas.");
                            varList.Clear();
                            cauList.Clear();
                        }
                        else
                        {
                            VariableCause vc = new VariableCause();
                            vc.variableList = varList;
                            string nameRule = "" + main.process.ElementAt(main.selectedIndex).getRandomName(); ;
                            vc.ruleN = nameRule;
                            vc.causeList = cauList;
                            string rule = "";
                            if (vc.variableList.Count() == 1)
                            {
                                rule = add_rule(vc);

                            }
                            else
                            {
                                rule = add_rule2(vc);
                            }

                           // MessageBox.Show(rule);
                            vc = new VariableCause(varList, cauList, rule, nameRule);
                            main.process.ElementAt(main.selectedIndex).rulList.AddLast(vc);
                            this.Close();
                            wind.fill_table(main);

                        }
                    }
                    else
                    {
                        MessageBox.Show("Debe insertar al menos una variable.");
                    }
                   
                 }
            }
            else
            {
                causeRule cr = new causeRule(main, this);
                cr.Show();
                this.IsEnabled = false;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            causeRule cr = new causeRule(main, this);
            cr.Show();
            this.IsEnabled = false;
        }
        private void button_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //existe si los antecedentes y consecuentes de la reglas son iguales
       public bool variable_in_Rule( LinkedList<Variable> variables, LinkedList<Cause> causas)
        {
            bool exist = false;
            bool existVar = false;
           
            for (int i = 0; i < main.process.ElementAt(main.selectedIndex).rulList.Count; i++)
            {
                if (main.process.ElementAt(main.selectedIndex).rulList.ElementAt(i) is VariableCause)
                {
                  LinkedList<Variable> vars = ((VariableCause)main.process.ElementAt(main.selectedIndex).rulList.ElementAt(i)).variableList;
                  LinkedList<Cause> cau = ((VariableCause)main.process.ElementAt(main.selectedIndex).rulList.ElementAt(i)).causeList;
                    existVar = equalsList(variables,vars);
                    if (existVar == true) {
                        exist = true;
                        break;
                    }
               }
            }
            return exist;
        }
        //comparar si el consecuente(lista de causa) esta en una regla
        private bool equalsListCause(LinkedList<Cause> cause, LinkedList<Cause> cause2)
        {
            bool flag = false;
            int cont = 0;
            int size = cause.Count;
            int size2 = cause2.Count;
            if (size2==size) {
               foreach (var C in cause)
                {
                    foreach (var C2 in cause2)
                    {
                        if (C2.staticCause == C.staticCause)
                        {
                            cont++;
                          }
                     }
                }
            }
            else { flag = false;             
            }
            if (cont==size) {
                flag = true;
            }
          
            return flag;
        }

        //comparar si dos listas de variables son iguales en las reglas
        private bool equalsList(LinkedList<Variable> variable, LinkedList<Variable> variable2)
        {
            int siz1 = variable.Count;
            int size2 = variable2.Count;
            bool flag = false;
            int cont = 0;
            if (siz1 == size2)
            {
                foreach (var V in variable)
                {
                    foreach (var V2 in variable2)
                    {
                        if (V2.name == V.name)
                        {
                            VariableType vt = V.type;
                            VariableType vt2 = V2.type;

                            if (vt is Continua & vt2 is Continua)
                            {
                                if (((Continua)vt).estat == ((Continua)vt2).estat)
                                {
                                    cont++;
                                }
                            }
                            else if (vt is Discreta & vt2 is Discreta)
                            {
                                if (((Discreta)vt).estat == ((Discreta)vt2).estat)
                                {
                                    cont++;
                                }
                            }
                            else
                              if (vt is Valvula & vt2 is Valvula)
                            {
                                if (((Valvula)vt).estat == ((Valvula)vt2).estat)
                                {
                                    cont++;
                                }
                            }
                         }
                        else { flag = false; }
                    }
                }
            }
            else
            {
                flag = false;
            }
            if (cont == siz1)
            {
                flag = true;
            }
            return flag;
        }
      
        public void clean_Data()
        {
            comboBox_variable.Text = "";
        }

        private void button_Click_2(object sender, RoutedEventArgs e)
        {
            if (comboBox_variable.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar una variable.");
            }
            else
            {
                bool exist = false;
                foreach (Variable var in dataV)
                {
                    if (var.name.Equals(comboBox_variable.SelectedItem.ToString())) {
                        exist = true;  break;
                    }
                }
                if (!exist)
                {                     
                    VariableRule2 vr = new VariableRule2(main, this, comboBox_variable.SelectedItem.ToString(),null);
                    vr.Show();
                    this.IsEnabled = false;
                    clean_Data();
                }
                else {
                    MessageBox.Show("Esta variable ya existe.");
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            wind.IsEnabled = true;
        }

        private void button_delete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una variable.");
            }
            else
            {
                Variable var = ((Variable)dataGrid.SelectedItem);
                dataV.Remove(var);
                dataGrid.DataContext = dataV;
              }
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!inAction)
            {
                Variable v = ((Variable)dataGrid.SelectedItem);
                 temp = v;
             
            }
            inAction = false;
        }

        private void button_modif_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una variable.");
            }
            else
            {             
                VariableRule2 vr = new VariableRule2(main,this,temp.name,temp);
                vr.Show();
                this.IsEnabled = false;
            }
            }
        public string state(Variable v)
        {
           string state = "";
           VariableType vtype = v.type;

                        if (vtype is Continua)
                        {
                            if (((Continua)vtype).estat == StateC.Alto)
                            {
                                state = "ALTO";
                            }
                            else if (((Continua)vtype).estat == StateC.Normal)
                            {
                                state = "NORMAL";
                            }
                            else{
                                if (((Continua)vtype).estat == StateC.Bajo)
                                {
                                    state = "BAJO";
                                }
                            }

                        }
                        else
                        {
                            if (vtype is Valvula)
                            {
                                if (((Valvula)vtype).estat == StateV.Abierta)
                                {
                                    state = "ABIERTO";
                                }
                                else if (((Valvula)vtype).estat == StateV.Normal)
                                {
                                    state = "NORMAL";
                                }
                                else{
                                    if (((Valvula)vtype).estat == StateV.Cerrada)
                                    {
                                        state = "CERRADO";
                                    }
                                }

                            }

                            else if (vtype is Discreta)
                            {
                                if (((Discreta)vtype).estat == StateD.Negativo)
                                {
                                    state = "NEGATIVO";
                                }
                                else
                                {
                                    if (((Discreta)vtype).estat == StateD.Positivo)
                                    {
                                        state = "POSITIVO";
                                    }
                                }
                        }
                    }
                    return state;
        }
        private string add_rule(VariableCause vc)
        {
            string N = vc.ruleN;
            string rule = "rule \"" + N + "\"" + "/when ";
          if (vc.variableList.Count > 0)
            {
                string state = "";

                if (vc.variableList.Count == 1)
                {
                    for (int i = 0; i < vc.variableList.Count; i++)
                    {
                        Variable v = vc.variableList.ElementAt(i);
                        VariableType vtype = v.type;

                        if (vtype is Continua)
                        {
                            string value = "";
                            float max = 0;
                            float min = 0;
                            foreach (Variable var in main.process.ElementAt(main.selectedIndex).varList)
                            {
                                if (var.name.Equals(v.name))
                                {
                                    max = ((Continua)var.type).max;
                                    min = ((Continua)var.type).min;
                                }
                            }
                            if (((Continua)vtype).estat == StateC.Alto)
                            {

                                state = "ALTO";
                                value = " > " + max;
                            }
                            else if (((Continua)vtype).estat == StateC.Normal)
                            {
                                state = "NORMAL";
                                value = " < " + min + " & " + " > " + max;
                            }
                            else
                            {
                                if (((Continua)vtype).estat == StateC.Bajo)
                                {
                                    state = "BAJO";
                                    value = " < " + min;

                                }
                            }
                            rule += "(VariableContinua(nombreVar  matches \"" + v.name + "\", currentEstado == Estados." + state + ", valorVC" + value + ")  )" + " / then/ GeneralTree<String> tree = new GeneralTree<String>();";
                            rule += "/BinaryTreeNode<String> root = new BinaryTreeNode<String>(new String(\"" + v.name + "\"));";
                            rule += "/BinaryTreeNode<String> nodeA = new BinaryTreeNode<String>(new String(\"" + state + "  y valor " + value + "\"));";
                            rule += "/ tree.insertNode(root, null);" + "/ tree.insertNode(nodeA, root);";

                        }
                        else
                        {
                            if (vtype is Valvula)
                            {
                                if (((Valvula)vtype).estat == StateV.Abierta)
                                {
                                    state = "ABIERTA";
                                }
                                else if (((Valvula)vtype).estat == StateV.Normal)
                                {
                                    state = "NORMAL";
                                }
                                else
                                {
                                    if (((Valvula)vtype).estat == StateV.Cerrada)
                                    {
                                        state = "CERRADA";
                                    }
                                }
                                rule += "(Valvula(nombreVar matches  \"" + v.name + "\", currentEstado == Estados." + state + "))/then /GeneralTree<String> tree = new GeneralTree<String>();";
                                rule += "/ BinaryTreeNode<String> root = new BinaryTreeNode<String>(new String(\"" + v.name + "\"));" + "/BinaryTreeNode<String> nodeA = new BinaryTreeNode<String>(new String(\"" + state + "\"));";
                                rule += "/tree.insertNode(root, null);" + "/ tree.insertNode(nodeA, root);";

                            }

                            else if (vtype is Discreta)
                            {
                                if (((Discreta)vtype).estat == StateD.Negativo)
                                {
                                    state = "NEGATIVO";
                                }
                                else
                                {
                                    if (((Discreta)vtype).estat == StateD.Positivo)
                                    {
                                        state = "POSITIVO";
                                    }
                                }
                                rule += "(VariableDiscreta(nombreVar matches  \"" + v.name + "\", currentEstado == Estados." + state + "))/then /GeneralTree<String> tree = new GeneralTree<String>();";
                                rule += "/ BinaryTreeNode<String> root = new BinaryTreeNode<String>(new String(\"" + v.name + "\"));" + "/BinaryTreeNode<String> nodeA = new BinaryTreeNode<String>(new String(\"" + state + "\"));";
                                rule += "/tree.insertNode(root, null);" + "/ tree.insertNode(nodeA, root);";
                            }

                        }
                    }
                }
            }
            // Las causas de la regla

            if (vc.causeList.Count > 0)
            {
                for (int i = 0; i < vc.causeList.Count; i++)
                {
                    string r = vc.causeList.ElementAt(i).staticCause;
                    string temp = main.sim(r);
                    rule += "/ BinaryTreeNode<String>" + "node" + temp + "= new BinaryTreeNode<String>(new String(" + "\"" + r + "\"" + "));";
                    if (vc.variableList.Count() == 1)
                    {
                        rule += "/ tree.insertNode(" + "node" + temp + ", nodeA);";
                    }

                }

                rule += "/ProcesarDatos.varAnalizadas.add(new Variable(\"\",\"" + vc.variableList.ElementAt(0).name + "\"" + ",tree));";
                rule += "/ProcesarDatos.insertarRespueta(\"";

                for (int j = 0; j < vc.causeList.Count; j++)
                {
                    String r = vc.causeList.ElementAt(j).staticCause;
                    rule += r;
                    if (j != vc.causeList.Count - 1)
                    {
                        rule += ",";
                    }
                }
                rule += "\");";

                rule += "/javax.swing.JOptionPane.showMessageDialog(new JFrame(),\"Las posibles causas son: ";

                for (int j = 0; j < vc.causeList.Count; j++)
                {
                    String r = vc.causeList.ElementAt(j).staticCause;
                    rule += r;
                    if (j != vc.causeList.Count - 1)
                    {
                        rule += " , ";
                    }
                }
            }

            rule += "\");/end/";
            return rule;
        }
       // cuando existen mas de una variable(regla conjunta)
        private string add_rule2(VariableCause vc)
        {
            string rule = "rule \"" + vc.ruleN + "\"" + "/when ";
            int cont = 0;
            rule += "/$conj: Conjunta() /(";
            string state = "";

            for (int i = 0; i < vc.variableList.Count; i++)
            {
                Variable v = vc.variableList.ElementAt(i);
                VariableType vtype = v.type;
              
                if (vtype is Continua)
                {
                    string value = "";
                    float max = 0;
                    float min = 0;
                    foreach (Variable var in main.process.ElementAt(main.selectedIndex).varList)
                    {
                        if (var.name.Equals(v.name))
                        {
                            max = ((Continua)var.type).max;
                            min = ((Continua)var.type).min;
                        }
                    }
                    
                    if (((Continua)vtype).estat == StateC.Alto)
                    {
                        state = "ALTO";
                        value = " > " + max;
                    }
                    else if (((Continua)vtype).estat == StateC.Normal)
                    {
                        state = "NORMAL";
                        value = " < " + min + " & " + " > " + max;
                    }
                    else
                    {
                        if (((Continua)vtype).estat == StateC.Bajo)
                        {
                            state = "BAJO";
                            value = " < " + min;
                        }
                    }
                    if (cont == 0)
                    {
                        rule += "($listavariables: ArrayList(size >= 1) from collect (";
                    }
                    else
                        if (cont == 1)
                    {
                        rule += "($listavariablesC: ArrayList(size >= 1) from collect (";
                    }
                    else
                        if (cont == 2)
                    {
                        rule += "($listavariablesD: ArrayList(size >= 1) from collect (";

                    }
                    cont++;
                    rule += "/ VariableContinua(nombreVar  matches \"" + v.name + "\", currentEstado == Estados." + state + ", valorVC" + value + ")";
                    rule += "/from $conj.getvConjunta() ) )";
                    //          ruleThen += "/BinaryTreeNode<String> " + main.sim(v.name) + " = new BinaryTreeNode<String>(new String(\"" + v.name + "\"));";
                    // ruleThen += "/BinaryTreeNode<String> " + main.sim(v.name) + "A = new BinaryTreeNode<String>(new String(\"" + state + "  y valor " + value + "\"));";
                    if (i != vc.variableList.Count - 1)
                    {
                        rule += "/and";
                    }
                }
                else
                {
                    if (vtype is Valvula)
                    {
                        if (((Valvula)vtype).estat == StateV.Abierta)
                        {
                            state = "ABIERTA";
                        }
                        else if (((Valvula)vtype).estat == StateV.Normal)
                        {
                            state = "NORMAL";
                        }
                        else
                        {
                            if (((Valvula)vtype).estat == StateV.Cerrada)
                            {
                                state = "CERRADA";
                            }
                        }
                        rule += "($listavalvulas: ArrayList(size >= 1) from collect (";
                        rule += "/Valvula(nombreVar matches  \"" + v.name + "\", currentEstado == Estados." + state + ")";
                        
                        rule += "/from $conj.getvConjunta() ) )";
                        }

                    else if (vtype is Discreta)
                    {
                        if (((Discreta)vtype).estat == StateD.Negativo)
                        { 
                            state = "NEGATIVO";
                        }
                        else
                        {
                            if (((Discreta)vtype).estat == StateD.Positivo)
                            {
                                state = "POSITIVO";
                            }
                        }
                        rule += "($listaDiscreta: ArrayList(size >= 1) from collect (";
                        rule += "/VariableDiscreta(nombreVar matches  \"" + v.name + "\", currentEstado == Estados." + state + ")";

                        rule += "/from $conj.getvConjunta() ) )";
                      }
                 }
            }
              rule += " ) / then/ GeneralTree<String> tree = new GeneralTree<String>();";

              //rule +=ruleThen;
            // Las causas de la regla

            if (vc.causeList.Count > 0)
            {
                // for (int i = 0; i < vc.causeList.Count; i++)
                //{
                    // c = vc.causeList.ElementAt(i).staticCause;
                //    string temp = main.sim(c);
                  //  rule += "/ BinaryTreeNode<String>" + "node" + temp + "= new BinaryTreeNode<String>(new String(" + "\"" + c + "\"" + "));";
                    //if (vc.variableList.Count() == 1)
                    //{
                      //  rule += "/ tree.insertNode(" + "node" + temp + ", nodeA);";
                    //}

                // }

                rule += "/ProcesarDatos.varAnalizadas.add(new Variable(\"\",\"" + vc.variableList.ElementAt(0).name + "\"" + ",tree));";
                rule += "/ProcesarDatos.insertarRespueta(\"";

                for (int j = 0; j < vc.causeList.Count; j++)
                {
                    String r = vc.causeList.ElementAt(j).staticCause;
                    rule += r;
                    if (j != vc.causeList.Count - 1)
                    {
                        rule += ",";
                    }
                }
                rule += "\");";

                rule += "/javax.swing.JOptionPane.showMessageDialog(new JFrame(),\"Las posibles causas son: ";

                for (int j = 0; j < vc.causeList.Count; j++)
                {
                    String r = vc.causeList.ElementAt(j).staticCause;
                    rule += r;
                    if (j != vc.causeList.Count - 1)
                    {
                        rule += " , ";
                    }
                }
            }

            rule += "\");/end/";
            return rule;
        }

    }
}
                  