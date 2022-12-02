using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using TesisAnaReglas.BussinesLogic;
using TesisAnaReglas.DbConnect;
using TesisAnaReglas.RoleBase;
using TesisAnaReglas.UserControls;
using System.IO;
using System.Security.Permissions;
using System.Text;
using RadioButton = System.Windows.Forms.RadioButton;

namespace TesisAnaReglas
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public class pas_user
    {
        private String proces;
        private bool us;
        

        public String process
        {
            get { return proces; }
            set { proces = value; }
        }

        public bool acces
        {
            get { return us; }
            set { us = value; }
        }

        public pas_user(string process, bool acces) : base()
        {
            this.process = process;
            this.acces = acces;

        }
    }

    public partial class MainWindow : Window
    {
        public LinkedList<Process> process = new LinkedList<Process>();
        public ObservableCollection<Process> data = new ObservableCollection<Process>();
        public bool inAction = false;
        public Process temp = null;
        public int selectedIndex = -1; //posicion del que se esta parado arriba ,analizando
        public bool isNew = false;
        public string pasW = null;
        public LinkedList<pas_user> user_pass = new LinkedList<pas_user>();
        private String mientras;

        public void elena(String s)
        {
            
            mientras = s;
        
        }
       
        
        public MainWindow()
        {
            InitializeComponent();

            if (selectedIndex == -1)
            {
                add.IsEnabled = false;
            }
        }

        private void generalData_Click(object sender, RoutedEventArgs e)
        {
            Window1 w = new Window1(this);
            w.Show();
        }

        private void variable_Click(object sender, RoutedEventArgs e)
        {
            VariablesWindow v = new VariablesWindow(this);
            v.Show();
        }

        private void cause_Click(object sender, RoutedEventArgs e)
        {
            Window2 caus = new Window2(this);
            caus.Show();
        }

        private void recomendation_Click(object sender, RoutedEventArgs e)
        {
            RecomendationWindow rec = new RecomendationWindow(this);
            rec.Show();
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            this.Close();
        }

        private void cause_rule_Click(object sender, RoutedEventArgs e)
        {
            CauseRuleWindow carule = new CauseRuleWindow(this);
            carule.Show();
        }

        private void var_cause_Click(object sender, RoutedEventArgs e)
        {
            VariableCauseRule rul_var = new VariableCauseRule(this);
            rul_var.Show();

        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            isNew = true;
            Window1 gd = new Window1(this);
            gd.Show();
        }

        private void Save_Ppocess_Click(object sender, RoutedEventArgs e)
        {
            if (selectedIndex != -1)
            {
                string path = "";
                var file = new System.Windows.Forms.SaveFileDialog
                {
                    Filter = "TXT (*txt)|*.txt",
                    AddExtension = true
                };

                if (file.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
                path = file.FileName;
                System.IO.StreamWriter objWriter;
                objWriter = new System.IO.StreamWriter(path);
                objWriter.WriteLine("Proceso en la Industria Alimentaria");
                objWriter.WriteLine(process.ElementAt(selectedIndex).processName);
                objWriter.WriteLine(process.ElementAt(selectedIndex).dataBaseName);
                objWriter.WriteLine(process.ElementAt(selectedIndex).pass);
                objWriter.WriteLine(process.ElementAt(selectedIndex).userName);
                objWriter.WriteLine(process.ElementAt(selectedIndex).passWord);
                objWriter.WriteLine("Variable");

                foreach (Variable var in process.ElementAt(selectedIndex).varList)
                {
                    string file_data = var.name + "," + var.type.type.ToString();
                    if (var.type is Continua)
                    {
                        float min = ((Continua) var.type).min;
                        float max = ((Continua) var.type).max;
                        file_data += "," + min + "," + max;
                    }
                    objWriter.WriteLine(file_data);
                }

                objWriter.WriteLine("Causes");
                foreach (Cause cau in process.ElementAt(selectedIndex).cauList)
                {
                    string file_data = cau.staticCause;
                    objWriter.WriteLine(file_data);
                }

                objWriter.WriteLine("Recomendation");
                foreach (Recomendation rec in process.ElementAt(selectedIndex).recList)
                {
                    string file_data = rec.staticRecomendation;
                    objWriter.WriteLine(file_data);
                }

                LinkedList<VariableCause> vc = new LinkedList<VariableCause>();
                LinkedList<CauseRecomendation> cr = new LinkedList<CauseRecomendation>();
                for (int i = 0; i < process.ElementAt(selectedIndex).rulList.Count; i++)
                {
                    if (process.ElementAt(selectedIndex).rulList.ElementAt(i) is VariableCause)
                    {
                        vc.AddFirst(((VariableCause) process.ElementAt(selectedIndex).rulList.ElementAt(i)));

                    }
                    else if (process.ElementAt(selectedIndex).rulList.ElementAt(i) is CauseRecomendation)
                    {
                        cr.AddFirst(((CauseRecomendation) process.ElementAt(selectedIndex).rulList.ElementAt(i)));
                    }
                }
                objWriter.WriteLine("Cause_Recomendation");
                foreach (CauseRecomendation rec in cr)
                {
                    string file_data = rec.cause.staticCause + "*";

                    if (rec.recomendationList.Count > 0)
                    {
                        for (int i = 0; i < rec.recomendationList.Count; i++)
                        {
                            file_data += rec.recomendationList.ElementAt(i).staticRecomendation;

                            if (i != rec.recomendationList.Count - 1)
                            {
                                file_data += " +";
                            }
                        }
                        file_data += "*" + rec.ruleR;
                    }
                    objWriter.WriteLine(file_data);
                }

                objWriter.WriteLine("Variable_Cause");
                foreach (VariableCause var in vc)
                {
                    string file_data = "";
                    if (var.variableList.Count > 0)
                    {
                        for (int i = 0; i < var.variableList.Count; i++)
                        {
                            file_data += var.variableList.ElementAt(i).name + ",";
                            VariableType type = var.variableList.ElementAt(i).type;

                            if (type is Valvula)
                            {
                                StateV val = ((Valvula) type).estat;
                                file_data += type.type + "," + val;
                            }
                            else if (type is Continua)
                            {
                                StateC con = ((Continua) type).estat;
                                file_data += type.type + "," + con;
                            }
                            else if (type is Discreta)
                            {
                                StateD dis = ((Discreta) type).estat;
                                file_data += type.type + "," + dis;
                            }

                            if (i != var.variableList.Count - 1)
                            {
                                file_data += "+";
                            }
                        }
                        file_data += "*";
                        if (var.causeList.Count > 0)
                        {
                            for (int i = 0; i < var.causeList.Count; i++)
                            {
                                file_data += var.causeList.ElementAt(i).staticCause;

                                if (i != var.causeList.Count - 1)
                                {
                                    file_data += "+";
                                }
                            }
                            file_data += "*" + var.ruleR + "*" + var.ruleN;
                        }
                    }

                    objWriter.WriteLine(file_data);

                }

                objWriter.Close();
                // FileIOPermission f2 = new FileIOPermission(FileIOPermissionAccess.Read, path);
                System.IO.File.SetAttributes(path, FileAttributes.IntegrityStream | FileAttributes.Normal);
            }
            else
                MessageBox.Show("Debe seleccionar un proceso");
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var path = "";
            var file = new System.Windows.Forms.SaveFileDialog
            {
                Filter = "DRL (*drl)|*.drl",
                AddExtension = true
            };

            if (file.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            path = file.FileName;
            System.IO.StreamWriter objWriter;
            Encoding ansi = Encoding.Default;
            objWriter = new System.IO.StreamWriter(path, true, ansi);

            objWriter.WriteLine("package CheeseProcess");

            objWriter.WriteLine("import Process.Principal_Drools;");
            objWriter.WriteLine("import Process.Variable;");
            objWriter.WriteLine("import Process.VariableContinua;");
            objWriter.WriteLine("import Process.VariableDiscreta;");
            objWriter.WriteLine("import Process.Valvula;");
            objWriter.WriteLine("import Process.Causa; ");
            objWriter.WriteLine("import Process.Estados;");
            objWriter.WriteLine("import Process.Resultados;");
            objWriter.WriteLine("import Process.Conjunta;");
            objWriter.WriteLine("import javax.swing.JFrame;");
            objWriter.WriteLine("import Visual.ProcesarDatos;");
            objWriter.WriteLine("import java.util.ArrayList;");
            objWriter.WriteLine("import javax.swing.JTree;");
            objWriter.WriteLine("import cu.edu.cujae.ceis.tree.general.GeneralTree;");
            objWriter.WriteLine("import cu.edu.cujae.ceis.tree.binary.BinaryTreeNode;");
            objWriter.WriteLine("");


            LinkedList<string> list = new LinkedList<string>();
            if (selectedIndex != -1)
            {
                var currentProcess = process.ElementAt(selectedIndex);
                foreach (Rule rul in currentProcess.rulList)
                {
                    list = new LinkedList<string>();
                    String a = rul.ruleR;
                    char[] ab = a.ToCharArray();
                    String temp = "";
                    for (int i = 0; i < ab.Length; i++)
                    {
                        if (!ab[i].ToString().Equals("/"))
                        {
                            temp += ab[i].ToString();
                        }
                        else
                        {
                            list.AddLast(temp);
                            temp = "";
                        }
                    }
                    list.AddLast(temp);
                    foreach (string r in list)
                    {
                        objWriter.WriteLine(r);
                    }
                }
                objWriter.Close();
                exportBinFiles(currentProcess, path);
            }

            else
            {
                MessageBox.Show("Debe seleccionar un proceso");
                objWriter.Close();
            }
        }

        private void exportBinFiles(Process process, String path)
        {

            LinkedList<Cause> miLista = new LinkedList<Cause>();
            LinkedList<Variable> miLista2 = new LinkedList<Variable>();
            LinkedList<Recomendation> miLista3 = new LinkedList<Recomendation>();
            StreamWriter objWriter;


            String temp = path.Substring(0, path.Length - 4);
            String fullName = temp + ".anm";

            objWriter = new System.IO.StreamWriter(fullName);
            try
            {
                miLista2 = process.varList;
                miLista = process.cauList;
                miLista3 = process.recList;
                foreach (var var in miLista2)
                {
                    string file_data = var.name + "," + var.type.type.ToString();
                    if (var.type is Continua)
                    {
                        float min = ((Continua) var.type).min;
                        float max = ((Continua) var.type).max;
                        file_data += min + "," + max;
                    }
                    objWriter.WriteLine(file_data);
                }
                objWriter.WriteLine("*causa");
                foreach (var current in miLista)
                {
                    objWriter.WriteLine(current.staticCause);
                }

                objWriter.WriteLine("*recomendacion");
                foreach (var current in miLista3)
                {
                    objWriter.WriteLine(current.staticRecomendation);
                }

                objWriter.WriteLine("-1");

                objWriter.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Problemas al crear: " + e.Message);
            }
            //Los archivos generados van a ser de solo lectura
            //FileAttributes attributes = System.IO.File.GetAttributes(fullName);
            System.IO.File.SetAttributes(fullName, FileAttributes.IntegrityStream | FileAttributes.Normal);
            System.IO.File.SetAttributes(path, FileAttributes.IntegrityStream | FileAttributes.Normal);
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            var path = "";
            var file = new System.Windows.Forms.OpenFileDialog();
            if (file.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            path = file.FileName;
            //verifica la extensión del archivo 
            if (path.Substring(path.LastIndexOf('.')).Trim().ToUpper() == ".TXT")
            {
                StreamReader objReader;
                objReader = new System.IO.StreamReader(path);
                selectedIndex++;

                string a = objReader.ReadLine().ToString();

                if (a.StartsWith("Proceso"))
                {
                    string process_name = objReader.ReadLine().ToString();
                    bool exist = false;

                    foreach (Process p in process)
                    {

                        if (p.processName.Equals(process_name))
                        {
                            exist = true;
                        }
                    }
                    if (exist)
                    {
                        MessageBox.Show("Este proceso ya existe.");
                        objReader.Close();

                    }
                    else
                    {
                        string dataBase = objReader.ReadLine().ToString();
                        string pass = objReader.ReadLine().ToString();
                        string user_name = objReader.ReadLine().ToString();
                        string passWord = objReader.ReadLine().ToString();

                        Process appLogic = new Process(user_name, passWord, process_name, dataBase, pass);

                        if (appLogic.userName.Equals(ConnectionHandler.getConnectionHandler().UserName))
                        {
                            pasW = pass;
                            PassWord pas = new PassWord(this, appLogic, objReader);
                            pas.Show();
                        }
                        else
                        {
                            file_process(appLogic, objReader);
                            add.IsEnabled = false;
                            user_pass.AddLast(new pas_user(process_name, false));
                            Title = "Sistema Generador de Base de Conocimiento en la Industria Alimentaria  (" +
                                    process_name + ")";
                            LB_arbol_proceso.Content = "Árbol de infercia: " + process_name;
                            updateTree();
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Formato incorrecto.");
                }
            }
            else
            {
                MessageBox.Show("Formato incorrecto.");

            }
        }

        public void file_process(Process appLogic, StreamReader objReader)
        {

            string temp = "";
            while (!objReader.EndOfStream)
            {
                string s = objReader.ReadLine().ToString();
                if (s.Equals("Variable"))
                {
                    temp = "Variable";
                }
                else if (s.Equals("Causes"))
                {
                    temp = "Causes";
                }
                else if (s.Equals("Recomendation"))
                {
                    temp = "Recomendation";
                }
                else if (s.Equals("Cause_Recomendation"))
                {
                    temp = "Cause_Recomendation";
                }
                else if (s.Equals("Variable_Cause"))
                {
                    temp = "Variable_Cause";
                }
                if (temp.Equals("Variable") && !s.Equals("Variable"))
                {
                    string name = s.Substring(0, s.IndexOf(','));
                    s = s.Replace(name + ",", "");
                    string type = s.Substring(0, s.Length);
                    //s = s.Replace(type + ",", "");
                    VariableType v;
                    if (type.Equals("Válvula"))
                    {
                        v = new VariableType();
                        v.type = "Válvula";

                    }
                    else if (type.Equals("Discreta"))
                    {
                        v = new VariableType();
                        v.type = "Discreta";
                    }
                    else
                    {
                        string typ = type.Substring(0, type.IndexOf(','));
                        type = type.Replace(typ + ",", "");

                        string min = type.Substring(0, type.IndexOf(','));
                        type = type.Replace(min + ",", "");

                        string max = type.Substring(0, type.Length);

                        v = new Continua(typ, float.Parse(min), float.Parse(max));
                        v.type = "Continua";
                    }
                    Variable var = new Variable(name, v);
                    appLogic.varList.AddLast(var);

                }
                else if (temp.Equals("Causes") && !s.Equals("Causes"))
                {
                    string staticCause = s.ToString();
                    Cause cau = new Cause(staticCause);
                    appLogic.cauList.AddLast(cau);
                }
                else if (temp.Equals("Recomendation") && !s.Equals("Recomendation"))
                {
                    string staticRec = s.ToString();
                    Recomendation rec = new Recomendation(staticRec);
                    appLogic.recList.AddLast(rec);
                }
                else if (temp.Equals("Cause_Recomendation") && !s.Equals("Cause_Recomendation"))
                {
                    string cauRec = s.ToString();

                    string cause = cauRec.Substring(0, cauRec.IndexOf('*'));
                    cauRec = cauRec.Replace(cause + "*", "");

                    string rest = cauRec.Substring(0, cauRec.IndexOf('*'));
                    cauRec = cauRec.Replace(rest + "*", "");

                    string restTwo = cauRec.Substring(0, cauRec.Length);

                    LinkedList<Recomendation> listRec = new LinkedList<Recomendation>();
                    while (!rest.Equals(""))
                    {
                        string rec = "";
                        if (rest.IndexOf('+') != -1)
                        {
                            rec = rest.Substring(0, rest.IndexOf('+'));
                            rest = rest.Replace(rec + "+", "");
                        }
                        else
                        {
                            rec = rest.Substring(0, rest.Length);
                            rest = rest.Replace(rec, "");
                        }
                        Recomendation r = new Recomendation();
                        r.staticRecomendation = rec;
                        listRec.AddLast(r);
                    }
                    CauseRecomendation causer = new CauseRecomendation(new Cause(cause), listRec, restTwo, cause);

                    appLogic.rulList.AddLast(causer);
                }
                else if (temp.Equals("Variable_Cause") && !s.Equals("Variable_Cause"))
                {
                    string varCau = s.ToString();

                    string var = varCau.Substring(0, varCau.IndexOf('*'));
                    varCau = varCau.Replace(var + "*", "");

                    string cau = varCau.Substring(0, varCau.IndexOf('*'));
                    varCau = varCau.Replace(cau + "*", "");

                    string rule = varCau.Substring(0, varCau.IndexOf('*'));
                    varCau = varCau.Replace(rule + "*", "");

                    string restTwo = varCau.Substring(0, varCau.Length);

                    LinkedList<Variable> listVar = new LinkedList<Variable>();
                    LinkedList<Cause> listCau = new LinkedList<Cause>();
                    VariableType vt = new VariableType();
                    string name = "";
                    while (!var.Equals(""))
                    {
                        string v = "";
                        string estado = "nada";
                        if (var.IndexOf('+') != -1)
                        {
                            //la variable completa
                            v = var.Substring(0, var.IndexOf('+'));
                            var = var.Replace(v + "+", "");
                            //el name
                            name = v.Substring(0, v.IndexOf(','));
                            v = v.Replace(name + ",", "");
                            //el typo
                            string type = v.Substring(0, v.IndexOf(','));
                            v = v.Replace(type + ",", "");
                            //el state
                            string state = v.Substring(0, v.Length);
                            v = state.Replace(state, "");

                            if (type.Equals("Válvula"))
                            {
                                StateV val = StateV.Normal;
                                estado = "Normal";
                                if (state.Equals("Abierta"))
                                {
                                    val = StateV.Abierta;
                                    estado = "Abierta";
                                }    
                                else if (state.Equals("Cerrada"))
                                {
                                    val = StateV.Cerrada;
                                    estado = "Cerrada";
                                }    
                                vt = new Valvula(val, type);
                                vt.estado = estado;
                            }
                            else if (type.Equals("Discreta"))
                            {
                                StateD d = StateD.Negativo;
                                estado = "Negativo";
                                if (state.Equals("Positivo"))
                                {
                                    d = StateD.Positivo;
                                    estado = "Positivo";
                                }
                                vt.estado = estado;
                                vt = new Discreta(d, type);
                            }
                            else if (type.Equals("Continua"))
                            {
                                StateC c = StateC.Normal;
                                estado = "Normal";
                                if (state.Equals("Bajo"))
                                {
                                    c = StateC.Bajo;
                                    estado = "Bajo";
                                }
                                else if (state.Equals("Alto"))
                                {
                                    c = StateC.Alto;
                                    estado = "Alto";
                                }
                                vt = new Continua(c, type);
                                vt.estado = estado;
                            }
                        }
                        else
                        {
                            //la variable completa
                            v = var.Substring(0, var.Length);
                            var = var.Replace(v, "");
                            //el nombre
                            name = v.Substring(0, v.IndexOf(','));
                            v = v.Replace(name + ",", "");
                            //el tipo
                            string type = v.Substring(0, v.IndexOf(','));
                            v = v.Replace(type + ",", "");
                            //el estate
                            string state = v.Substring(0, v.Length);
                            v = state.Replace(state, "");

                            if (type.Equals("Válvula"))
                            {
                                StateV val = StateV.Normal;
                                estado = "Normal";
                                if (state.Equals("Abierta"))
                                {
                                    val = StateV.Abierta;
                                    estado = "Abierta";
                                }
                                 else if (state.Equals("Cerrada"))
                                {
                                    val = StateV.Cerrada;
                                    estado = "Cerrada";
                                }
                                vt = new Valvula(val, type);
                                vt.estado = estado;
                            }
                            else if (type.Equals("Discreta"))
                            {
                                StateD d = StateD.Negativo;
                                estado = "Negativo";
                                if (state.Equals("Positivo"))
                                {
                                    d = StateD.Positivo;
                                    estado = "Positivo";
                                }

                                vt = new Discreta(d, type);
                                vt.estado = estado;

                            }
                            else if (type.Equals("Continua"))
                            {

                                StateC c = StateC.Normal;
                                estado = "Normal";
                                if (state.Equals("Bajo"))
                                {
                                    c = StateC.Bajo;
                                    estado = "Bajo";
                                }
                                  else if (state.Equals("Alto"))
                                {
                                    c = StateC.Alto;
                                    estado = "Alto";
                                }
                                vt = new Continua(c, type);
                                vt.estado = estado;
                            }
                        }
                        Variable a = new Variable(name, vt);
                       
                        listVar.AddLast(a);
                    }

                    while (!cau.Equals(""))
                    {
                        string c = "";
                        if (cau.IndexOf('+') != -1)
                        {
                            c = cau.Substring(0, cau.IndexOf('+'));
                            cau = cau.Replace(c + "+", "");
                        }
                        else
                        {
                            c = cau.Substring(0, cau.Length);
                            cau = cau.Replace(c, "");
                        }
                        Cause cause = new Cause();
                        cause.staticCause = c;
                        listCau.AddLast(cause);
                    }
                    VariableCause varC = new VariableCause(listVar, listCau, rule, restTwo);
                    appLogic.rulList.AddLast(varC);
                }
            }
            process.AddLast(appLogic);
            data.Add(appLogic);
            dataGridProcces.DataContext = data;
            objReader.Close();
        }

        private void modif_user_Click(object sender, RoutedEventArgs e)
        {
            ChangePassword ch = new ChangePassword();
            ch.Show();
        }

        private void dataGridProcces_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (!inAction)
            {
                Process p = ((Process) dataGridProcces.SelectedItem);
                temp = p;
                selectedIndex = dataGridProcces.SelectedIndex;

                if (user_pass.Count > 0)
                {
                    foreach (pas_user acces in user_pass)
                    {
                        if (acces.process.Equals(p.processName) && acces.acces)
                        {

                            add.IsEnabled = true;
                            break;

                        }
                        else
                        {
                            add.IsEnabled = false;
                        }

                    }

                    Title = "Sistema Generador de Base de Conocimiento en la Industria Alimentaria (" + p.processName +
                            ")";
                    LB_arbol_proceso.Content = "Árbol de infercia: " + p.processName;
                }
            }

            inAction = false;

            updateTree();

        }

        public void updateTree()
        {
            treeView.Items.Clear();
            Process current = process.ElementAt(selectedIndex);
            LB_arbol_proceso.Content = "Árbol de inferencia: " + current.processName;
            if (current.rulList.Count > 0)
            {
                foreach (var rul in current.rulList)
                {
                    if (rul is VariableCause)
                    {
                        VariableCause temp = (VariableCause) rul;
                        var nameVar = temp.variableList.First().name;

                        for (var i = 1; i < temp.variableList.Count; i++)
                        {
                            var vari = temp.variableList.ElementAt(i);
                            nameVar = nameVar + " y " + vari.name;
                        }

                        var item = new TreeViewItem();

                        item.Header = "V-C " + nameVar;
                        item.Tag = temp;
                        treeView.Items.Add(item);
                        foreach (var causa in temp.causeList)
                        {
                            var temp2 = new TreeViewItem();
                            temp2.Header = causa.staticCause;
                            item.Items.Add(temp2);
                        }
                    }
                    if (rul is CauseRecomendation)
                    {
                        CauseRecomendation temp = (CauseRecomendation) rul;
                        var nameVar = temp.cause.staticCause;
                        var item = new TreeViewItem();
                        item.Header = "C-R " + nameVar;
                        item.Tag = temp;
                        treeView.Items.Add(item);
                        foreach (var recom in temp.recomendationList)
                        {
                            var temp2 = new TreeViewItem();
                            temp2.Header = recom.staticRecomendation;
                            item.Items.Add(temp2);
                        }
                    }
                }

            }

        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            if (process.Count > 0)
                updateTree();
        }

        public string sim(string r)
        {
            char[] ab = r.ToCharArray();

            String temp = "";

            for (int k = 0; k < ab.Length; k++)
            {
                int ascci = Convert.ToInt32(ab[k]);

                if (ascci >= 65 && ascci <= 90 || ascci >= 97 && ascci <= 122)
                {
                    temp += ab[k].ToString();
                }
            }
            return temp;
        }

        public string state(Variable v)
        {
            string state = "";
            VariableType vtype = v.type;

            if (vtype is Continua)
            {
                if (((Continua) vtype).estat == StateC.Normal)
                {
                    state = "Normal";
                }
                else if (((Continua) vtype).estat == StateC.Alto)
                {
                    state = "Alto";
                }
                else
                {
                    if (((Continua) vtype).estat == StateC.Bajo)
                    {
                        state = "Bajo";
                    }
                }

            }
            else if (vtype is Valvula)
                {
                    if (((Valvula) vtype).estat == StateV.Abierta)
                    {
                        state = "Abierto";
                    }                   
                    if (((Valvula) vtype).estat == StateV.Normal)
                    {
                        state = "Normal";
                    } 
                    if (((Valvula) vtype).estat == StateV.Cerrada)
                    {
                            state = "Cerrado";
                    }                 

                }

                else if (vtype is Discreta)
                {
                    if (((Discreta) vtype).estat == StateD.Negativo)
                    {
                        state = "Negativo";
                    }
                    else
                    {
                        if (((Discreta) vtype).estat == StateD.Positivo)
                        {
                            state = "Positivo";
                        }
                    }
                }
            return state;
        }


    }

}

