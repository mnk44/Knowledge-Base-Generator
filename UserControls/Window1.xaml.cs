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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private MainWindow main { get; set; }

        public Window1(MainWindow main)
        {
            InitializeComponent();
            this.main = main;
        }
  
        private void button_Click(object sender, RoutedEventArgs e)
        {
            GeneralData g = new GeneralData(textBox_process_name.Text.ToString(), textBox_data_name.Text.ToString());
            main.appLogic = g;
            this.Close();
        }
    }
}
