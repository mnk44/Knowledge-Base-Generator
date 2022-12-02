using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using TesisAnaReglas.DbConnect;

namespace TesisAnaReglas.RoleBase
{
    /// <summary>
    /// Interaction logic for LogWindow.xaml
    /// </summary>
    public partial class LogWindow : Window
    {
        public LogWindow()
        {
            InitializeComponent();
        }

        private void newUser_Click(object sender, RoutedEventArgs e)
        {
            AddUser add = new AddUser();
            add.Show();
        }
    
        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
            /*if (userNameBox.Text.Equals("") || passBox.Password.Equals(""))
            {
                MessageBox.Show("Faltan datos por llenar.");
            }
            else
            {
                ConnectionHandler.getConnectionHandler().Conectar();
                ConnectionHandler.getConnectionHandler().Log("*", "user", "\"username\" = '" + userNameBox.Text + "' and \"password\" = '" + MD5Encrypt.getMD5Encrypt().MD5(passBox.Password.ToString()) + "'", MD5Encrypt.getMD5Encrypt().MD5(passBox.Password.ToString()), userNameBox.Text);
                if (ConnectionHandler.getConnectionHandler().isLoging())
                {
                    MainWindow main = new MainWindow();
                    main.Show();
                    this.Close();
                }
                else 
                {
                    MessageBox.Show("Usuario o contraseña errónea.");
                }
            }*/
        }
        private void txtletras_PreviewTextInput(object sender, TextCompositionEventArgs e)

        {
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));

            if (ascci >= 65 && ascci <= 90 || ascci >= 97 && ascci <= 122)

                e.Handled = false;

            else e.Handled = true;

        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            //Close();
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
                
    }
}
