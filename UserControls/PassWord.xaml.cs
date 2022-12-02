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
using System.IO;

using TesisAnaReglas.DbConnect;

namespace TesisAnaReglas.UserControls
{
    /// <summary>
    /// Interaction logic for PassWord.xaml
    /// </summary>
    public partial class PassWord : Window
    {
        private MainWindow main { get; set; }
        private Process process { get; set; }
        private StreamReader reader { get; set; }
        public PassWord(MainWindow main,Process p, StreamReader ob)
        {
            if (p!=null & ob!=null) {
                process = p;
                reader = ob;
            }
            this.main = main;
            InitializeComponent();
        }

        private void button_close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            string pass =passwordBox.Password;
            if (pass.Equals(main.pasW))
            {
               
                if (process!=null & reader!=null ) {
                    main.file_process(process, reader);
                }
                main.add.IsEnabled = true;
                main.pasW = null;
                main.user_pass.AddLast(new pas_user(process.processName, true));
                main.Title = "Sistema Generador de Base de Conocimiento en la Industria Alimentaria  (" + process.processName + ")";
                main.LB_arbol_proceso.Content = "Árbol de infercia: " + process.processName;
                main.updateTree();
                Close();
            }
            else
            {
                const string message = "Clave incorrecta, desea abrir el fichero solo de lectura.";
                const string caption = "Alerta";
                var result = MessageBox.Show(message, caption, MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {
                    if (process != null & reader != null)
                    {
                        main.file_process(process, reader);
                    }
                    main.add.IsEnabled = false;
                    main.pasW = null;
                    main.user_pass.AddLast(new pas_user(process.processName, false));
                    main.Title = "Sistema Generador de Base de Conocimiento en la Industria Alimentaria  (" + process.processName + ")";
                    main.LB_arbol_proceso.Content = "Árbol de infercia: " + process.processName;
                    main.updateTree();
                    Close();
                }
            }
        }
    }
}
