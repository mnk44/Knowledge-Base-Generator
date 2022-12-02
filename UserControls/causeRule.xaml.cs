using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
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
    /// Interaction logic for causeRule.xaml
    /// </summary>
    public partial class causeRule : Window
    {
        private MainWindow main = null;
        private LinkedList<Cause> cauList = new LinkedList<Cause>();
        private LinkedList<Cause> cauListA = new LinkedList<Cause>();
        ObservableCollection<Cause> data = new ObservableCollection<Cause>();
        public bool inAction = false;
        VariableRule wind = null;
        public causeRule(MainWindow main, VariableRule varW)
        {
            InitializeComponent();
            this.main = main;
            wind = varW;

            if (wind.dataC != null)
            {
                foreach (Cause c in wind.dataC)
                {
                    data.Add(c);
                    cauList.AddLast(c);
                }
                dataG.DataContext = data;
            }

            if (main.process.ElementAt(main.selectedIndex).cauList != null)
                {
                    foreach (Cause cau in main.process.ElementAt(main.selectedIndex).cauList)
                    {
                    cauListA.AddLast(cau);
                   
                    }
                }
            List<Cause> causes = new List<Cause>();
            causes = cauListA.OrderBy(p => p.staticCause).ToList();  //Retorna una lista ordenada por Nombre

            foreach (Cause order in causes)
            {
                comboBox.Items.Add(order.staticCause);
            }
        }

        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar una causa.");
            }
            else
            {
                bool exit = false;

                foreach (Cause c in cauList)
                {
                    if (c.staticCause.Equals(comboBox.SelectedItem.ToString()))
                    {
                        exit = true; break;
                    }
                }
                if (!exit)
                {
                    Cause caus = new Cause() { staticCause = comboBox.SelectedItem.ToString() };
                    data.Add(caus);
                    cauList.AddLast(caus);
                    dataG.DataContext = data;
                    clean_Data();

                }
                else
                {
                    MessageBox.Show("Ya existe esta causa.");
                }
            }
        }

        private void button_delete_Click(object sender, RoutedEventArgs e)
        {
            if (dataG.SelectedIndex != -1)
            {
                Cause rec = ((Cause)dataG.SelectedItem);
                cauList.Remove(rec);
                data.Remove(rec);
                dataG.DataContext = data;
            }
            else {
                MessageBox.Show("Debe seleccionar una causa.");
                  }
          }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!inAction)
            {
                Cause cau = ((Cause)dataG.SelectedItem);
             }
            inAction = false;
        }
        public void clean_Data()
        {
         comboBox.Text = "";
         }

        private void button_acept_Click(object sender, RoutedEventArgs e)
        {
             if (cauList != null)
            {
                wind.dataC.Clear();
                foreach (Cause c in data)
                {
                 wind.dataC.Add(c);
                    wind.button_add_var.IsEnabled = false;
                }
                Close();
            }    
        }

        private void button_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            wind.IsEnabled = true;
        }
    }
}