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
using Npgsql;
using TesisAnaReglas.DbConnect;
using MessageBox = System.Windows.MessageBox;

namespace TesisAnaReglas.RoleBase
{
    /// <summary>
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        public ChangePassword()
        {
            InitializeComponent();
            label_Copy2.Content = ConnectionHandler.getConnectionHandler().UserName;
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if ( !passwordBox.Password.Equals("") & !passwordBox_Copy.Password.Equals(""))
            {

                ConnectionHandler.getConnectionHandler().Conectar();
                
                
                
                    if (passwordBox.Password.ToString().Equals(passwordBox_Copy.Password.ToString()))
                    {
                        ConnectionHandler.getConnectionHandler().UpdateUser(ConnectionHandler.getConnectionHandler().UserName, passwordBox.Password.ToString());
                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show("Las contraseñas no coinciden");
                    }
                
                ConnectionHandler.getConnectionHandler().Desconectar();
            }
            else
            {
                MessageBox.Show("Faltan datos por llenar");
            }
        }
    }
}
