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
    /// Interaction logic for CauseRuleWindow.xaml
    /// </summary>

    public class RowE
    {
        private String _c;
        private String _v;
   
        public String cause
        {
            get { return _c; }
            set { _c = value; }
        }

        public String variable
        {
            get { return _v; }
            set { _v = value; }
        }
   }


    public partial class VariableCauseWindows : Window
    {
        private MainWindow main = null;
       
        public LinkedList<VariableCause> vcList = new LinkedList<VariableCause>();
        private LinkedList<Cause> causeList = new LinkedList<Cause>();
        private LinkedList<Variable> variableList = new LinkedList<Variable>();
        ObservableCollection<RowE> data = new ObservableCollection<RowE>();
        public bool inAction = false;
        private RowE temp = null;

        public VariableCauseWindows(MainWindow main)
        {
            this.main = main;
            InitializeComponent();

         if (main.appLogic != null)
            {
                if (main.appLogic.rulList != null)
                {
                    foreach (Rule rule in main.appLogic.rulList)
                    {
                        if (rule is VariableCause)
                        {
                            VariableCause c = (VariableCause) rule;
                            vcList.AddLast(c);

                            causeList = c.causeList;
                            variableList = c.variableList;

                            String ca = "";
                            String re = "";

                            foreach (Cause cau in causeList)
                            {
                                ca += cau.staticCause + " , ";
                            }

                            foreach (Variable var in variableList)
                            {
                                re += var.name + " , ";
                            }

                            RowE r  = new RowE() {cause = ca, variable = re};
                            data.Add(r);
                            datag.DataContext = data; 
                        }
                    }
                    
                }
                
            }
        }

        private void button_add_recomen(object sender, RoutedEventArgs e)
        {
            VariableRule v = new VariableRule(main);
            v.Show();
            this.Close(); 
        }

       

        private void button_del_Click(object sender, RoutedEventArgs e)
        {
            inAction = true;
            RowE r = ((RowE)datag.SelectedItem);
            data.Remove(r);
            datag.DataContext = data;
            foreach (Cause cau in main.appLogic.cauList)
            {
                if(cau.staticCause.Equals(temp.cause))
                causeList.AddLast(cau);
              
            }
            foreach (Rule rule in main.appLogic.rulList)
            {
                if (rule is CauseRecomendation)
                    if (((CauseRecomendation)rule).cause.staticCause.Equals(temp.cause))
                    {
                        main.appLogic.rulList.Remove(rule); break;
                    }
            }

        }

        private int index = -1;
        private void datag_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (!inAction)
            {
                RowE row = ((RowE)datag.SelectedItem);
                temp = row;
                index = datag.SelectedIndex;
            }
            inAction = false;
        }

        private void button_mod_Click(object sender, RoutedEventArgs e)
        {
            VariableRule v = new VariableRule(main);
            v.Show();
            this.Close();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

       
    }
}
