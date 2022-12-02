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
    /// Interaction logic for VariableCauseRule.xaml
    /// </summary>
    public class RowVC
    {
    private String _v;
    private string _c;
    private string _rul;
        public String variable
     {
        get { return _v; }
        set { _v = value; }
      }
     public String cause
    {
        get { return _c; }
        set { _c = value; }
        }
     public String rule
        {
            get { return _rul; }
            set { _rul = value; }
        }


    }
    public partial class VariableCauseRule : Window
    {
        private MainWindow main = null;
        public LinkedList<VariableCause> ruleList = new LinkedList<VariableCause>();
        ObservableCollection<RowVC> data = new ObservableCollection<RowVC>();
        RowVC temp = null;
        bool inAction = false;
        VariableCause varCau = null;
        public VariableCauseRule(MainWindow main)
        {
            this.main = main;
            InitializeComponent();
            fill_table(main);
        }
        public void fill_table(MainWindow main)
        {
            data.Clear();
            if (main.process.ElementAt(main.selectedIndex) != null)
            {
                if (main.process.ElementAt(main.selectedIndex).rulList != null)
                {
                    foreach (Rule rule in main.process.ElementAt(main.selectedIndex).rulList)
                    {
                        if (rule is VariableCause)
                        {
                            VariableCause c = (VariableCause)rule;
                            ruleList.AddLast(c);

                            String var = "";

                            for (int i = 0; i < c.variableList.Count; i++)
                            {
                                Variable v = c.variableList.ElementAt(i);
                                string state = "";
                                state = main.state(v);
                                var += v.name + " en " + state;
                                if (i != c.variableList.Count - 1)
                                {
                                    var += ",";
                                }

                            }
                            String cau = "";

                            for (int j = 0; j < c.causeList.Count; j++)
                            {
                                cau += c.causeList.ElementAt(j).staticCause;
                                if (j != c.causeList.Count - 1)
                                {
                                    cau += ",";
                                }
                               
                            }
                            string rul = c.ruleR;

                            RowVC r = new RowVC() { variable = var, cause = cau, rule = rul };
                            data.Add(r);
                            datag.DataContext = data;
                        }
                    }
                }
            }
        }
        private void button_close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            VariableRule vr = new VariableRule(main, null, this);
            vr.Show();
            this.IsEnabled = false;

        }

        private void button_delete_Click(object sender, RoutedEventArgs e)
        {
            if (datag.SelectedIndex != -1)
            {
                inAction = true;
                RowVC r = ((RowVC)datag.SelectedItem);
                data.Remove(r);
                datag.DataContext = data;

                foreach (Rule rule in main.process.ElementAt(main.selectedIndex).rulList)
                {
                    if (rule is VariableCause)
                        if (((VariableCause)rule).ruleR.Equals(temp.rule))
                        {
                            main.process.ElementAt(main.selectedIndex).rulList.Remove(rule); break;
                        }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una variable  y sus causas.");
            }
        }
        private int index = -1;
        private void datag_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!inAction)
            {
                RowVC row = ((RowVC)datag.SelectedItem);
                temp = row;
                index = datag.SelectedIndex;

            }
            if (temp != null)
            {
                if (main.process.ElementAt(main.selectedIndex).rulList != null)
                {
                    foreach (Rule rule in main.process.ElementAt(main.selectedIndex).rulList)
                    {
                        if (rule is VariableCause)
                        {
                            VariableCause c = (VariableCause)rule;
                            if (c.ruleR.Equals(temp.rule))
                            {
                                varCau = c; break;
                            }
                        }
                    }
                }
            }
            inAction = false;
        }

        private void button_mod_Click(object sender, RoutedEventArgs e)
        {
            if (datag.SelectedItem != null)
            {
                VariableRule v = new VariableRule(main, varCau, this);
                v.Show();
                this.IsEnabled = false;
            }
            else
            {
                MessageBox.Show("Debe selecionar una variable  y sus causas.");
            }
        }
    }
}
