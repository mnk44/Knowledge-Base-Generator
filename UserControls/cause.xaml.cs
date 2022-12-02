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

using TesisAnaReglas.UserControls;

namespace TesisAnaReglas.UserControls
{
    /// <summary>
    /// Interaction logic for cause.xaml
    /// </summary>
    public partial class cause : Window
    {
        private MainWindow main { get; set; }
        public Cause temp = null;
        public bool inAction = false;
        private Window2 wind = null;
        public cause(MainWindow main, Cause cause, Window2 causeW )
        {
            InitializeComponent();
            this.main = main;
            wind = causeW;
          
            if (cause != null)
            {
                if (main.process.ElementAt(main.selectedIndex).cauList != null)
                {
                    foreach (Cause c in main.process.ElementAt(main.selectedIndex).cauList)
                    {
                        if (c.Equals(cause))
                        {
                            temp = c;
                            textBox.Text = temp.staticCause.ToString();
                        }
                    }
                }

            }
         }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            if (temp != null)
            {
                inAction = true;
                Cause cau = null;
                foreach (Cause caus in main.process.ElementAt(main.selectedIndex).cauList)
                {
                    if (caus.Equals(temp))
                    {
                        cau = caus;
                    }
                }
                if (!textBox.Text.Equals(""))
                {
                    inAction = true;
                    bool exit = false;

                    foreach (Cause cause in main.process.ElementAt(main.selectedIndex).cauList)
                    {
                        if (cause.staticCause.Equals(textBox.Text.ToString()) & !temp.staticCause.Equals(textBox.Text.ToString()))
                        {
                            exit = true; break;
                        }
                    }
                    if (exit)
                    {
                        MessageBox.Show("Ya existe esta causa.");
                    }
                    else
                    {
                        cau.staticCause = textBox.Text.ToString();
                        int pos = wind.data.IndexOf(cau);
                        wind.data.Insert(pos, cau);
                        wind.data.RemoveAt(pos + 1);
                        wind.dg.DataContext = wind.data;

                        if (wind.existSimple.Count > 0 || wind.existMultiple.Count > 0)
                        {

                            wind.modif_rule();
                            wind.existMultiple.Clear();
                            wind.existSimple.Clear();
                        }
                        Close();
                    }
                  
                }
                else {
                    MessageBox.Show("Falta la descripción de la causa.");
                }
            }
            else
            {
                inAction = true;
                bool exit = false;

                foreach (Cause cause in main.process.ElementAt(main.selectedIndex).cauList)
                {
                    if (cause.staticCause.Equals(textBox.Text.ToString()))
                    {
                        exit = true; break;
                    }
                }
                if (!exit && !textBox.Text.Equals(""))
                {
                    Cause cau = new Cause() { staticCause = textBox.Text.ToString() };
                    temp = cau ;
                    main.process.ElementAt(main.selectedIndex).cauList.AddLast(cau);
                    Close();                    
                }
                else
                if (exit)
                {
                    MessageBox.Show("Ya existe esta causa.");
                }
                else
                if (textBox.Text.Equals(""))
                {
                    MessageBox.Show("Falta la descripción de la causa.");
                }
              }
            wind.fill_table(main);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            wind.IsEnabled = true;
        }
    }

}