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
using TesisAnaReglas.DbConnect;
using TesisAnaReglas.UserControls;

namespace TesisAnaReglas.UserControls
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private MainWindow main { get; set; }
        public bool inAction = false;
        public static bool isNew = false;

        public Window1(MainWindow main)
        {            
            InitializeComponent();
            this.main = main;
           
            if (!main.isNew)
            {
                if (main.selectedIndex != -1 && main.process.ElementAt(main.selectedIndex) != null)
                {
                    textBox_process_name.Text = main.process.ElementAt(main.selectedIndex).processName;
                    textBox_data_name.Text = main.process.ElementAt(main.selectedIndex).dataBaseName;
                   passwordBox.Password = main.process.ElementAt(main.selectedIndex).pass;
                    main.dataGridProcces.DataContext = main.data;
                }
            }                    
        }
  
        private void button_add(object sender, RoutedEventArgs e)
        {
            
            if (main.selectedIndex != -1 && main.process.ElementAt(main.selectedIndex) != null && !main.isNew)
            {
                main.process.ElementAt(main.selectedIndex).processName= textBox_process_name.Text;
                main.process.ElementAt(main.selectedIndex).dataBaseName= textBox_data_name.Text;
                main.process.ElementAt(main.selectedIndex).pass = passwordBox.Password;
                main.dataGridProcces.DataContext =main.data;
                Close();          
            }
            else {
                if (!textBox_process_name.Text.ToString().Equals("") && !textBox_data_name.Text.ToString().Equals("") && !passwordBox.Password.Equals(""))
                {
                     Process g = new Process(ConnectionHandler.getConnectionHandler().UserName, ConnectionHandler.getConnectionHandler().PassWord, textBox_process_name.Text.ToString(), textBox_data_name.Text.ToString(), passwordBox.Password);

                    main.process.AddLast(g);
                    main.data.Add(g);
                    main.isNew=false;
                    main.user_pass.AddLast(new pas_user(g.processName, true));
                    if (main.dataGridProcces.Items.Count == 0)
                    {
                        main.dataGridProcces.SelectedIndex = 0;
                        main.selectedIndex = main.dataGridProcces.SelectedIndex;
                    }
                    else
                    {
                        main.dataGridProcces.SelectedIndex = main.dataGridProcces.Items.Count - 1;
                        main.selectedIndex = main.dataGridProcces.SelectedIndex;
                    }                   
                    
                    main.dataGridProcces.DataContext = main.data;
                    main.dataGridProcces.CommitEdit();
                    main.LB_arbol_proceso.Content = "Árbol de infercia: " + textBox_process_name.Text;
                    Close();
                }
                else
                {
                    MessageBox.Show("Faltan datos por llenar.");
                }
            }
                
            }

        private void button_cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
       
           
    }
}
