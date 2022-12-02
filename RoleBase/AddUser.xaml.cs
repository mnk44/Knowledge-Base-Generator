using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
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
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        public AddUser()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            if (!userNameBox.Text.Equals("") & !passBox.Password.Equals("") & !passConfirmationBox.Password.Equals(""))
            {

                ConnectionHandler.getConnectionHandler().Conectar();
                if (ConnectionHandler.getConnectionHandler().ExistUser(userNameBox.Text))
                {
                    MessageBox.Show("El usuario ya existe.");
                }
                else
                {
                    if (passBox.Password.ToString().Equals(passConfirmationBox.Password.ToString()))
                    {
                        ConnectionHandler.getConnectionHandler().AddUser(userNameBox.Text, passBox.Password.ToString());
                         this.Close();

                    }
                    else
                    {
                        MessageBox.Show("Las contraseñas no coinciden");
                    }
                }
                ConnectionHandler.getConnectionHandler().Desconectar();
            }
            else
            {
                MessageBox.Show("Faltan datos por llenar");
            }
        }
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void txtletras_PreviewTextInput(object sender, TextCompositionEventArgs e)

        {
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));

            if (ascci >= 65 && ascci <= 90 || ascci >= 97 && ascci <= 122)

                e.Handled = false;

            else e.Handled = true;

        }


    }
}

