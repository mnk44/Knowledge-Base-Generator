using System;
using System.Collections.Generic;
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
    /// Interaction logic for recomendation.xaml
    /// </summary>
    public partial class recomendation : Window
    {
        private MainWindow main { get; set; }
        public Recomendation temp = null;
        public bool inAction = false;
        private RecomendationWindow wind = null;
        public recomendation(MainWindow main, Recomendation rec,RecomendationWindow recW)
        {
            InitializeComponent();
            this.main = main;
            wind = recW;
            if (rec != null)
            {
                if (main.process.ElementAt(main.selectedIndex).recList != null)
                {
                    foreach (Recomendation r in main.process.ElementAt(main.selectedIndex).recList)
                    {
                        if (r.Equals(rec))
                        {
                            temp = r;
                            textBox_descrip.Text = temp.staticRecomendation.ToString();
                        }
                    }
                }
             }
        }

        private void button_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
      
        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            if (temp != null)
            {
                inAction = true;
                Recomendation rec = null;
                foreach (Recomendation reco in main.process.ElementAt(main.selectedIndex).recList)
                {
                    if (reco.Equals(temp))
                    {
                        rec = reco;
                    }
                }
                if (!textBox_descrip.Text.Equals(""))
                {
                    bool exit = false;

                    foreach (Recomendation r in main.process.ElementAt(main.selectedIndex).recList)
                    {
                        if (r.staticRecomendation.Equals(textBox_descrip.Text.ToString()) & !temp.staticRecomendation.Equals(textBox_descrip.Text.ToString()))
                        {
                            exit = true; break;
                        }
                    }
                    if (exit)
                    {
                        MessageBox.Show("Ya existe esta recomendación.");
                    }
                    else
                    {
                        rec.staticRecomendation = textBox_descrip.Text.ToString();
                        int pos = wind.data.IndexOf(rec);
                        wind.data.Insert(pos, rec);
                        wind.data.RemoveAt(pos + 1);
                        wind.datag.DataContext = wind.data;
                        if (wind.existSimple.Count > 0 || wind.existMultiple.Count > 0)
                        {
                            wind.modif_rule();
                            wind.existMultiple.Clear();
                            wind.existSimple.Clear();
                        }
                        Close();
                       }
                }
                else
                {
                    MessageBox.Show("Falta la descripción de la recomendación.");
                }
             }
            else
            {
                inAction = true;
                bool exit = false;

                foreach (Recomendation rec in main.process.ElementAt(main.selectedIndex).recList)
                {
                    if (rec.staticRecomendation.Equals(textBox_descrip.Text.ToString()))
                    {
                        exit = true; break;
                    }
                }
                if (!exit && !textBox_descrip.Text.Equals(""))
                {
                    Recomendation reco = new Recomendation() { staticRecomendation = textBox_descrip.Text.ToString() };
                    temp = reco;
                    main.process.ElementAt(main.selectedIndex).recList.AddLast(reco);
                    this.Close();
                }
                else
                if (exit)
                {
                    MessageBox.Show("Ya existe esta recomendación.");
                }
                else
                if (textBox_descrip.Text.Equals(""))
                {
                    MessageBox.Show("Falta la descripción de la recomendación.");
                }

            }
            wind.fill_table(main);
        }
        private void txtletras_PreviewTextInput(object sender, TextCompositionEventArgs e)

        {
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));

            if (ascci >= 65 && ascci <= 90 || ascci >= 97 && ascci <= 122)

                e.Handled = false;

            else e.Handled = true;

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            wind.IsEnabled = true;
        }
    }
}
