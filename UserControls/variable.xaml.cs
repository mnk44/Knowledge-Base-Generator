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
    /// Interaction logic for variable.xaml
    /// </summary>
    public partial class variable : Window
    {
        private MainWindow main { get; set; }
        public Variable temp = null;
        private VariablesWindow wind = null;
        public bool inAction = false;
        public variable(MainWindow main, Variable var, VariablesWindow varW)
        {
            InitializeComponent();
            this.main = main;
            wind = varW;

            comboBox_type.Items.Add("Continua");
            comboBox_type.Items.Add("Válvula");
            comboBox_type.Items.Add("Discreta");
            if (var != null)
            {
                if (main.process.ElementAt(main.selectedIndex).varList != null)
                {
                    foreach (Variable v in main.process.ElementAt(main.selectedIndex).varList)
                    {
                        if (v.Equals(var))
                        {
                            temp = v;
                            textBox_name.Text = temp.name.ToString();
                            VariableType vt = temp.type;
                            if (vt.type.Equals("Continua"))
                            {
                                float min = ((Continua)vt).min;
                                float max = ((Continua)vt).max;

                                textBox_min.Text = min.ToString();
                                textBox_max.Text = max.ToString();
                            }

                            if (var.type.type.Equals("Válvula"))
                            {
                                comboBox_type.SelectedIndex = 1;
                            }
                            else if (var.type.type.Equals("Discreta"))
                            {
                                comboBox_type.SelectedIndex = 2;
                            }
                            else if (var.type.type.Equals("Continua"))
                            {
                                comboBox_type.SelectedIndex = 0;
                            }
                        }
                    }
                }
            }
            if (wind.existSimple.Count > 0 || wind.existMultiple.Count > 0)
            {
                comboBox_type.IsEnabled = false;
            }
        }
        private void set_visibility(string val)
        {
            if (val.Equals("Válvula") )
            {
                groupBox.Visibility = Visibility.Hidden;
            }
            else if (val.Equals("Continua"))
            {
              
                groupBox.Visibility = Visibility.Visible;
            }
            else if (val.Equals("Discreta"))
            {
           
                groupBox.Visibility = Visibility.Hidden;
             }
        }
        private string selItem = "";
        private void comboBox_type_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            selItem = comboBox_type.SelectedItem.ToString();

            if (selItem.Equals("Válvula"))
            {
                set_visibility("Válvula");
            }
            else if (selItem.Equals("Continua"))
            {
                set_visibility("Continua");
            }
            else
            {
                set_visibility("Discreta");
            }
        }
        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        private void button_Click(object sender, RoutedEventArgs e)
        {

            // caso para modicar la variable
            if (temp != null)
            {
                bool exis = false;

                            
                    Variable var = null;
                    foreach (Variable variable in main.process.ElementAt(main.selectedIndex).varList)
                    {
                        if (variable.Equals(temp))
                        {
                            var = variable;

                        }
                    }

                    if (!textBox_name.Text.Equals("") & comboBox_type.SelectedItem != null)
                    {

                    foreach (Variable v in main.process.ElementAt(main.selectedIndex).varList)
                    {
                        if (v.name.Equals(textBox_name.Text.ToString()) & !temp.name.Equals(textBox_name.Text.ToString()))
                        {
                            exis = true; break;
                        }
                    }
                    if (exis)
                    {
                        MessageBox.Show("Ya existe esta variable.");
                    }
                    else
                    {
                        if (comboBox_type.Text.ToString().Equals("Válvula"))
                        {
                            var.type.type = "Válvula";
                            var.name = textBox_name.Text.ToString().Trim();
                            int pos = wind.data.IndexOf(var);
                            wind.data.Insert(pos, var);
                            wind.data.RemoveAt(pos + 1);
                            wind.dgTest.DataContext = wind.data;
                            if (wind.existSimple.Count > 0 || wind.existMultiple.Count > 0)
                            {
                                wind.modif_rule();
                                wind.existMultiple.Clear();
                                wind.existSimple.Clear();
                            }
                            Close();
                        }
                        else if (comboBox_type.Text.ToString().Equals("Continua"))
                        {
                            if (!textBox_min.Text.Equals("") & !textBox_max.Text.Equals(""))
                            {

                                string min = textBox_min.Text.ToString();
                                string max = textBox_max.Text.ToString();
                                if (float.Parse(min) < float.Parse(max))
                                {
                                    var.type.type = "Continua";
                                    var.type = new Continua(var.type.type = "Continua", float.Parse(min), float.Parse(max));
                                    var.name = textBox_name.Text.ToString().Trim();

                                    int pos = wind.data.IndexOf(var);
                                    wind.data.Insert(pos, var);
                                    wind.data.RemoveAt(pos + 1);
                                    wind.dgTest.DataContext = wind.data;
                                    if (wind.existSimple.Count > 0 || wind.existMultiple.Count > 0)
                                    {
                                        wind.modif_rule();
                                        wind.existMultiple.Clear();
                                        wind.existSimple.Clear();
                                    }
                                    Close();
                                }
                                else
                                {
                                    MessageBox.Show("El límite minimo no puede ser superior al límite mayor.");
                                }

                            }

                            else
                            {
                                if (!textBox_min.Text.Equals("") || !textBox_max.Text.Equals(""))
                                {
                                    string min = textBox_min.Text.ToString();
                                    string max = textBox_max.Text.ToString();

                                    if (min.Equals(""))
                                    {
                                        min = null;
                                    }
                                    if (max.Equals(""))
                                    {
                                        max = null;
                                    }
                                    var.type.type = "Continua";

                                    var.type = new Continua(var.type.type = "Continua", float.Parse(min),
                                        float.Parse(max));
                                    var.name = textBox_name.Text.ToString().Trim();

                                    int pos = wind.data.IndexOf(var);
                                    wind.data.Insert(pos, var);
                                    wind.data.RemoveAt(pos + 1);
                                    wind.dgTest.DataContext = wind.data;
                                    if (wind.existSimple.Count > 0 || wind.existMultiple.Count > 0)
                                    {
                                        wind.modif_rule();
                                        wind.existMultiple.Clear();
                                        wind.existSimple.Clear();
                                    }
                                    Close();

                                }
                                else
                                {
                                    MessageBox.Show("Faltan datos por llenar.");
                                }
                                
                            }
                        }
                        else if (comboBox_type.Text.ToString().Equals("Discreta"))
                        {
                            var.type.type = "Discreta";
                            var.name = textBox_name.Text.ToString().Trim();
                            int pos = wind.data.IndexOf(var);
                            wind.data.Insert(pos, var);
                            wind.data.RemoveAt(pos + 1);
                            wind.dgTest.DataContext = wind.data;
                            if (wind.existSimple.Count > 0 || wind.existMultiple.Count > 0)
                            {
                                wind.modif_rule();
                                wind.existMultiple.Clear();
                                wind.existSimple.Clear();
                            }
                            Close();

                        }
                    }
                    }
                    else
                    {
                        MessageBox.Show("Faltan datos por llenar.");
                    }
                
        }
            //caso para insertar una nueva variable
            else {
                bool exist = false;

                foreach (Variable var in main.process.ElementAt(main.selectedIndex).varList)
                {
                    if (var.name.Equals(textBox_name.Text.ToString()))
                    {
                        exist = true; break;
                    }
                }
                VariableType v;
                if (comboBox_type.Text.ToString().Equals("Válvula"))
                {
                    v = new VariableType();
                    v.type = "Válvula";
                }
                else if (comboBox_type.Text.ToString().Equals("Continua"))
                {
                    v = new VariableType();
                    v.type = "Continua";
                }
                else
                {
                    v = new VariableType();
                    v.type = "Discreta";
                }

                if (exist)
                {
                    MessageBox.Show("Ya existe esta variable.");
                }
                else if (!comboBox_type.Text.Equals(""))
                {
                    if (comboBox_type.Text.ToString().Equals("Discreta") || comboBox_type.Text.ToString().Equals("Válvula"))
                    {
                        if (!textBox_name.Text.Equals(""))
                        {
                            Variable var = new Variable() { name = textBox_name.Text.ToString(), type = v, typeS = v.type };
                            temp = var;
                            main.process.ElementAt(main.selectedIndex).varList.AddLast(var);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Falta la etiqueta por llenar.");
                        }
                    }
                    else if (comboBox_type.Text.ToString().Equals("Continua"))
                    {
                        if (!textBox_name.Text.Equals("") /*& !textBox_min.Text.Equals("") & !textBox_max.Text.Equals("")*/)
                        {
                            string mi = textBox_min.Text.ToString();
                            string ma = textBox_max.Text.ToString();
                            float min = 0;
                            float max = 0;
                            /*if (!float.TryParse((string)mi, out min) || !float.TryParse((string)ma, out max))
                            {
                                MessageBox.Show("La entrada de los límites es incorrecta.");
                            }else
                            {*/

                            if (mi.Equals("") || ma.Equals(""))
                            {
                                if (mi.Equals(""))
                                {
                                    min = 0;                                    
                                    max = float.Parse(ma);
                                }
                                if (ma.Equals(""))
                                {
                                    max = 210317;
                                    min = float.Parse(mi);
                                    
                                }
                                string typ = comboBox_type.Text.ToString();
                                v = new Continua(typ, min, max);
                                Variable var = new Variable() { name = textBox_name.Text.ToString(), type = v, typeS = v.type };
                                var.valorMin = min;
                                var.valorMax = max;
                                temp = var;
                                main.process.ElementAt(main.selectedIndex).varList.AddLast(var);
                                Close();
                            }

                            else { 
                            min = float.Parse(mi);
                            max = float.Parse(ma);
                             
                            if(min < max)
                            {
                                string typ = comboBox_type.Text.ToString();
                                v = new Continua(typ, min, max);
                                Variable var = new Variable() { name = textBox_name.Text.ToString(), type = v, typeS = v.type };
                                var.valorMin = min;
                                var.valorMax = max;
                                temp = var;
                                main.process.ElementAt(main.selectedIndex).varList.AddLast(var);
                                Close();
                            }
                            else
                            {
                                MessageBox.Show("El límite minimo no puede ser superior al límite mayor.");
                            }

                            }
                        }
                        else
                        {
                            MessageBox.Show("Faltan datos por llenar.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Faltan datos por llenar.");
                }

            }
            wind.fill_table(main);
          }

      
        private void txtnum_PreviewTextInput(object sender, TextCompositionEventArgs e)

        {
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));

            if (ascci >= 48 && ascci <= 57 || ascci > 45 && ascci < 47) e.Handled = false;

            else e.Handled = true;
          
        }
      
        private void Variable_OnClosed(object sender, EventArgs e)
        {
            wind.IsEnabled = true;
            
        }
    }
}
