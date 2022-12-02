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

    public class RowA
    {
        private String _c;
        private string _r;
   
        public String cause
        {
            get { return _c; }
            set { _c = value; }
        }

        public String recomendation
        {
            get { return _r; }
            set { _r = value; }
        }
   }
  public partial class CauseRuleWindow : Window
    {
        private MainWindow main = null;
        public LinkedList<CauseRecomendation> ruleList = new LinkedList<CauseRecomendation>();
        private LinkedList<Cause> causeList = new LinkedList<Cause>();
        ObservableCollection<RowA> data = new ObservableCollection<RowA>();
        public bool inAction = false;
        private RowA temp = null;

        public CauseRuleWindow(MainWindow main)
        {
            this.main = main;
            InitializeComponent();
            fill_table(main);
            
            if (main.process.ElementAt(main.selectedIndex).cauList != null)
            {
                foreach (Cause cau in main.process.ElementAt(main.selectedIndex).cauList)
                {
                 causeList.AddLast(cau);
                 }
            }

            List<Cause> c = new List<Cause>();
            c = causeList.OrderBy(p => p.staticCause).ToList();  //Retorna una lista ordenara por Nombre

      
            foreach (Cause order in c) {
            comboBox_cause.Items.Add(order.staticCause);
            }
        }
        public void fill_table(MainWindow main)
        {
            data.Clear();
          
          
                if (main.process.ElementAt(main.selectedIndex).rulList != null)
                {
                    foreach (Rule rule in main.process.ElementAt(main.selectedIndex).rulList)
                    {
                        if (rule is CauseRecomendation)
                        {
                            CauseRecomendation c = (CauseRecomendation)rule;
                            ruleList.AddLast(c);

                            String s = "";

                        for (int i = 0; i < c.recomendationList.Count; i++)
                        {
                            Recomendation rec = c.recomendationList.ElementAt(i);

                            s += rec.staticRecomendation;
                            if (i != c.recomendationList.Count - 1)
                            {
                                s += ",";
                            }
                        }
                            RowA r = new RowA() { cause = c.cause.staticCause, recomendation = s };
                            data.Add(r);
                            datag.DataContext = data;
                        
                    }
                }
            }
         }
        private void button_add_recomen(object sender, RoutedEventArgs e)
         {
           
            if (comboBox_cause.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar una causa.");
            }
            else {
                bool b =exit(comboBox_cause.SelectedValue.ToString()) ;
                               
                if (b)
                {
                    MessageBox.Show("Esta causa ya tiene asociada sus recomendaciones.");

                }
                else { 
                CauseRecWindow cau = new CauseRecWindow(main, comboBox_cause.SelectedItem.ToString(),this);
                cau.Show();
                    this.IsEnabled = false;
              }
            }
        }
        public bool exit(string c) {
            bool exit = false;
            foreach (Rule rule in main.process.ElementAt(main.selectedIndex).rulList)
            {
                if (rule is CauseRecomendation)
                    if (((CauseRecomendation)rule).cause.staticCause.Equals(c))
                    {
                      exit = true; break;
                    }
            } 
            return exit;
        }
        private void button_del_Click(object sender, RoutedEventArgs e)
        {
            if (datag.SelectedIndex != -1)
            {
                inAction = true;
                RowA r = ((RowA)datag.SelectedItem);
                data.Remove(r);
                datag.DataContext = data;
                              
                    foreach (Rule rule in main.process.ElementAt(main.selectedIndex).rulList)
                {
                    if (rule is CauseRecomendation)
                        if (((CauseRecomendation)rule).cause.staticCause.Equals(temp.cause))
                        {
                            main.process.ElementAt(main.selectedIndex).rulList.Remove(rule); break;
                           
                        }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una causa");
            }
        }
        private int index = -1;
        private void datag_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (!inAction)
            {
                RowA row = ((RowA)datag.SelectedItem);
                temp = row;
                index = datag.SelectedIndex;
            }
            inAction = false;
        }

        private void button_mod_Click(object sender, RoutedEventArgs e)
        {
            if (datag.SelectedItem!=null) {

                CauseRecWindow v = new CauseRecWindow(main, temp.cause,this);
                v.Show();
                this.IsEnabled = false;
            }
            else
            {
                MessageBox.Show("Debe selecionar una causa");
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void clean_Data()
        {
          comboBox_cause.Text = "";
        }
     
    }
}
