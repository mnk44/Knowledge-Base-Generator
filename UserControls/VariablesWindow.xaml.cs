using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for VariablesWindow.xaml
    /// </summary>
    /// 
    public class RuleVariable
    {
        private Rule _c;
        private Variable _r;

        public Rule rule
        {
            get { return _c; }
            set { _c = value; }
        }

        public Variable variable
        {
            get { return _r; }
            set { _r = value; }
        }
    }
    public partial class VariablesWindow : Window
    {
        private MainWindow main { get; set; }
        private LinkedList<Variable> varList = new LinkedList<Variable>();
        public ObservableCollection<Variable> data = new ObservableCollection<Variable>();
        public Variable temp = null;
        public bool inAction = false;

        public VariablesWindow(MainWindow main)
        {
            InitializeComponent();
            this.main = main;
            fill_table(main);
         }
        public void fill_table(MainWindow main)
        {
            data.Clear();
            if (main.process.ElementAt(main.selectedIndex) != null)
            {
                if (main.process.ElementAt(main.selectedIndex).varList != null)
                {
                    foreach (Variable var in main.process.ElementAt(main.selectedIndex).varList)
                    {
                        data.Add(var);
                        varList.AddLast(var);
                    }
                        dgTest.DataContext = data;
                   }
               }
           }
        private void add_variable(object sender, RoutedEventArgs e)
        {
            variable var = new variable(main,null,this);
            var.Show();
            this.IsEnabled = false;
        }

        private void remove_variable(object sender, RoutedEventArgs e)
        {
            exist_rule(temp);
            if (dgTest.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una variable.");            
            }
            else {
                if (existSimple.Count > 0 || existMultiple.Count>0)
                {
                    const string message = "Si usted elimina esta variable, tambien eliminará la regla donde esta se encuentre.";
                    const string caption = "Alerta";
                    var result = MessageBox.Show(message, caption, MessageBoxButton.OKCancel);
                    if (result == MessageBoxResult.OK)
                    {
                        delete_rule();
                        inAction = true;
                        Variable var = ((Variable)dgTest.SelectedItem);
                        varList.Remove(var);
                        data.Remove(var);
                        dgTest.DataContext = data;
                        main.process.ElementAt(main.selectedIndex).varList.Remove(var);
                    }
                }
                else {
                    delete_rule();
                    inAction = true;
                    Variable var = ((Variable)dgTest.SelectedItem);
                    varList.Remove(var);
                    data.Remove(var);
                    dgTest.DataContext = data;
                    main.process.ElementAt(main.selectedIndex).varList.Remove(var);
                }
                existSimple.Clear();
            }
        }

        private void modify_variable(object sender, RoutedEventArgs e)
        {
          
            if (dgTest.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una variable");
            }
            else
            {

                exist_rule(temp);
              
                if (existSimple.Count > 0 || existMultiple.Count > 0)
                {
                    const string message = "Esta variable ya tiene asociadas sus causas,si la modifica también se modificá en la asociaciones donde se encuentra.";
                    const string caption = "Alerta";
                    var result = MessageBox.Show(message, caption, MessageBoxButton.OKCancel);

                    if (result == MessageBoxResult.OK)
                    {
                        variable v = new variable(main, temp, this);
                        v.Show();
                        this.IsEnabled = false;
                    }
                }
                else
                {
                    variable var = new variable(main, temp, this);
                    var.Show();
                    this.IsEnabled = false;
                }
            }
         }
               
        private void DgTest_OnSelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (!inAction)
            {
             Variable var = ((Variable)dgTest.SelectedItem);
             temp = var;
             }
            inAction = false;
        }
        private void button_acept_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public LinkedList<RuleVariable> existSimple = new LinkedList<RuleVariable>();
        public LinkedList<RuleVariable> existMultiple = new LinkedList<RuleVariable>();
        private void exist_rule(Variable v)
        {
              if (v != null)
            {
                if (main.process.ElementAt(main.selectedIndex).rulList != null)
                {
                    foreach (Rule rul in main.process.ElementAt(main.selectedIndex).rulList)
                    {
                        if (rul is VariableCause)
                        {
                            VariableCause vc = ((VariableCause)rul);
                            for (int i = 0; i < vc.variableList.Count; i++)
                            {
                                if (v.name.Equals(vc.variableList.ElementAt(i).name) & vc.variableList.Count==1)
                                {
                                   existSimple.AddFirst(new RuleVariable() { rule = rul, variable = vc.variableList.ElementAt(i) }); break;
                                }else
                                   if (v.name.Equals(vc.variableList.ElementAt(i).name) & vc.variableList.Count >1)
                                {
                                    existMultiple.AddFirst(new RuleVariable() { rule = rul, variable = vc.variableList.ElementAt(i) }); break;
                                }
                            }
                        }
                    }
                }
            }
        
    }
        public void delete_rule()
        { 
            foreach (RuleVariable var in existSimple)
            {
                main.process.ElementAt(main.selectedIndex).rulList.Remove(var.rule);
             }
            foreach (RuleVariable var in existMultiple)
            {
                ((VariableCause)main.process.ElementAt(main.selectedIndex).rulList.Find(var.rule).Value).variableList.Remove(var.variable);
            }
        }

        public void modif_rule()
        {
            foreach (RuleVariable rul in existMultiple)
            {
                (((VariableCause)main.process.ElementAt(main.selectedIndex).rulList.Find(rul.rule).Value).variableList.Find(rul.variable).Value) = temp;
            }
            foreach (RuleVariable rul in existSimple)
            {
                (((VariableCause)main.process.ElementAt(main.selectedIndex).rulList.Find(rul.rule).Value).variableList.Find(rul.variable).Value) = temp;
            }
        }

       
    }
}
