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
    /// Interaction logic for VariableRule2.xaml
    /// </summary>
    /// 
  
    public partial class VariableRule2 : Window
    {
        private MainWindow main = null;
        private LinkedList<Variable> varList = new LinkedList<Variable>();
        private VariableRule wind = null;
        private Variable variable = null;
        private Variable variable2 = null;
        
        StateC s;
  
        public VariableRule2(MainWindow main, VariableRule varW,string name,Variable vari)
        {
            InitializeComponent();
            this.main = main;
            wind = varW;
            label_name.Content = name;

            if (vari != null)
            {

                variable2 = vari;

                if (variable2.type != null)
                {
                    VariableType v = variable2.type;
                    if (variable2.typeS.Equals("Válvula"))
                    {
                        radioButton_open.Visibility = Visibility.Visible;
                        radioButton_normal.Visibility = Visibility.Visible;
                        radioButton_Close.Visibility = Visibility.Visible;                       

                        StateV val = ((Valvula)v).estat;
                        v = new Valvula(val, v.type);
                        if (val.Equals(StateV.Abierta))
                        {
                            radioButton_open.IsChecked = true;
                        }
                        if (val.Equals(StateV.Normal))
                        {
                            radioButton_normal.IsChecked = true;
                        }
                        if (val.Equals(StateV.Cerrada))
                        {
                            radioButton_Close.IsChecked = true;
                        }                      
                    }
                    else if (variable2.typeS.Equals("Continua"))
                    {
                            radioButton_high.Visibility = Visibility.Visible;
                            radioButton_normalC.Visibility = Visibility.Visible;
                            radioButton_under.Visibility = Visibility.Visible;
                            StateC con = ((Continua)v).estat;
                            v = new Continua(con, v.type);

                            if (con.Equals(StateC.Alto))
                            {
                            radioButton_high.IsChecked = true;
                              //variable2.dayana = "Alto";
                            }
                            else if (con.Equals(StateC.Normal))
                            {
                            radioButton_normalC.IsChecked = true;
                               //variable2.dayana = "Normal";
                            }
                            else if (con.Equals(StateC.Bajo))
                            {
                            radioButton_under.IsChecked = true;
                            //variable2.dayana = "Bajo";
                        }
                        
                        }
                    else if (variable2.typeS.Equals("Discreta"))
                    {
                        radioButton_pos.Visibility = Visibility.Visible;
                        radioButton_neg.Visibility = Visibility.Visible;
                        StateD dis = ((Discreta)v).estat;
                         v = new Discreta(dis, v.type);
                        if (dis.Equals(StateD.Positivo))
                        {
                            radioButton_pos.IsChecked = true;
                        }
                        else if (dis.Equals(StateD.Negativo))
                        {
                            radioButton_neg.IsChecked = true;
                        }
                    }
                        variable2.type = v;
                    }

                }
                else
                {
                    if (main.process.ElementAt(main.selectedIndex).varList != null)
                    {
                        foreach (Variable var in main.process.ElementAt(main.selectedIndex).varList)
                        {
                            if (var.name.Equals(name))
                            {
                                variable = var;
                                //variable2 = var;
                            }
                        }
                        set_visibility();
                    }
                }
            
          
        }

        private void button_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void set_visibility()
        {
            if (variable.typeS.Equals("Válvula"))
            {
                radioButton_open.Visibility = Visibility.Visible;
                radioButton_normal.Visibility = Visibility.Visible;
                radioButton_Close.Visibility = Visibility.Visible;
                
            }
            else if (variable.typeS.Equals("Continua"))
            {
                radioButton_high.Visibility = Visibility.Visible;
                radioButton_normalC.Visibility = Visibility.Visible;
                radioButton_under.Visibility = Visibility.Visible;
              }
            else if (variable.typeS.Equals("Discreta"))
            {
                radioButton_pos.Visibility = Visibility.Visible;
                radioButton_neg.Visibility = Visibility.Visible;
            }
        }
        
     
        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            //StateC s = StateC.Alto;
            StateD d = StateD.Positivo;
            StateV v = StateV.Abierta;
           
           //Variable variable3=new Variable();
        string type = "";
            if (variable != null)
            {
                type = variable.typeS.ToString();
            }
            else if (variable2 != null)
            {
               
                type = variable2.typeS.ToString();
            }
            VariableType vt = null;
           
            if (radioButton_open.Visibility == Visibility.Visible)
            {
                if (radioButton_open.IsChecked == true || radioButton_Close.IsChecked == true || radioButton_normal.IsChecked == true)
                {
                    if (radioButton_open.IsChecked == true)
                    {
                        v = StateV.Abierta;
                        variable.dayana = "Abierta";
                    }
                    if (radioButton_normal.IsChecked == true)
                    {
                        v = StateV.Normal;
                        variable.dayana = "Normal";
                    }   
                    if (radioButton_Close.IsChecked == true)
                    {
                        v = StateV.Cerrada;
                        variable.dayana = "Cerrada";
                    }                  

                    vt = new Valvula(v, type);
                }
                else
                    MessageBox.Show("Debe seleccionar un estado.");
            }
            else if (radioButton_high.Visibility == Visibility.Visible)
            {
                
                if (radioButton_high.IsChecked == true || radioButton_under.IsChecked == true || radioButton_normalC.IsChecked == true)
                {
                    if (radioButton_high.IsChecked == true)
                    {
                       
                        s = StateC.Alto;
                       // main.elena("Alto");
                        variable.dayana = "ALTO";
                    }
                    else if (radioButton_normalC.IsChecked == true)
                    {
                       
                        s = StateC.Normal;
                       // main.elena("Normal");
                        variable.dayana = "NORMAL";
                    }
                    else
                    {
                        s = StateC.Bajo;
                       // main.elena("Bajo");
                        //variable2.CR;
                         variable.dayana = "BAJO";
                            
                    }
                    vt = new Continua(s, type);
                }
                else
                    MessageBox.Show("Debe seleccionar un estado.");
            }
            else if (radioButton_neg.Visibility == Visibility.Visible)
            {
                if (radioButton_neg.IsChecked == true || radioButton_pos.IsChecked == true)
                {
                    if (radioButton_neg.IsChecked == true)
                    {
                        d = StateD.Negativo;
                        variable.dayana = "Negativo";
                    }
                    else
                    {
                        d = StateD.Positivo;
                        variable.dayana = "Positivo";
                    }
                    vt = new Discreta(d, type);
                }

                else
                    MessageBox.Show("Debe seleccionar un estado.");
            }
            if (vt != null)
            {
                
                if (variable2 != null)
                {
                    variable2.type = vt;
                    int pos = wind.dataV.IndexOf(variable2);
                    wind.dataV.Insert(pos, variable2);
                    wind.dataV.RemoveAt(pos + 1);
                    wind.dataGrid.DataContext = wind.dataV;
                    this.Close();
                }
                else
                {
                    Variable var = new Variable(variable.name, vt);
                    wind.dataV.Add(var);
                    Close();
                }
                //revisar
                wind.fill_table(main);
            }
           
            


        }

        private void Window_Closed(object sender, EventArgs e)
        {
            wind.IsEnabled = true;
        }
    }

}

