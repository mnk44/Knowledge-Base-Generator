using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using TesisAnaReglas.BussinesLogic;
using System.Windows.Controls;


namespace TesisAnaReglas.UserControls
{
     
    
    public partial class CauseRecWindow : Window
    {
        private MainWindow main { get; set; }
        Cause staticC = null;
        public bool inAction = false;
        private CauseRecomendation causeRec = null;
        LinkedList<Recomendation> recomendationList = new LinkedList<Recomendation>();
        LinkedList<Recomendation> recomendationListA = new LinkedList<Recomendation>();
        public ObservableCollection<Recomendation> data = new ObservableCollection<Recomendation>();
        private CauseRuleWindow wind = null;
        public CauseRecWindow(MainWindow main,String cause, CauseRuleWindow crWind)
        {
            InitializeComponent();
            this.main = main;
            wind = crWind;
            label_causa.Content = cause;
           

                if (main.process.ElementAt(main.selectedIndex) != null)
                {

                    foreach (Rule c in main.process.ElementAt(main.selectedIndex).rulList)
                    {
                        if (c is CauseRecomendation)
                        {
                            causeRec = (CauseRecomendation)c;
                            if (causeRec.cause.staticCause.Equals(cause))
                            {
                                staticC = causeRec.cause;
                                recomendationList = causeRec.recomendationList;
                                break;
                            }
                            else
                            {
                                causeRec = null;
                            }
                        }
                    }
                

                if (recomendationList != null)
                {
                    foreach (Recomendation Rec in recomendationList)
                    {
                        data.Add(Rec);
                    }
                    dataGrid.DataContext = data;
                }
            }
            if (main.process.ElementAt(main.selectedIndex).recList != null)
            {
                foreach (Recomendation rec in main.process.ElementAt(main.selectedIndex).recList)
                {
                    recomendationListA.AddLast(rec);
                }
             }
            List<Recomendation> list = new List<Recomendation>();
            list = recomendationListA.OrderBy(p => p.staticRecomendation).ToList();  //Retorna una lista ordenada por Nombre

            foreach (Recomendation order in list)
            {
                comboBox_recomen.Items.Add(order.staticRecomendation);
            }
        }

        private void button_add_Click(object sender, RoutedEventArgs e)
        {
          
                inAction = true;
                bool exit = false;
                if (comboBox_recomen.SelectedItem == null)
                {
                    MessageBox.Show("Debe seleccionar una recomendación");
                }
                else
                {
                    foreach (Recomendation rec in data)
                    {
                        if (rec.staticRecomendation.Equals(comboBox_recomen.SelectedItem.ToString()))
                        {
                            exit = true; break;
                        }
                    }
                    if (!exit)
                    {
                        Recomendation rec = new Recomendation() { staticRecomendation = comboBox_recomen.SelectedItem.ToString() };
                        data.Add(rec);
                        recomendationList.AddLast(rec);
                        clean_Data();
                        dataGrid.DataContext = data;
                    }
                    else

                        MessageBox.Show("Ya existe esta recomendación");
                }
            }
        

        private void button_acep_Click(object sender, RoutedEventArgs e)
        {
            String rule = null;
            if (recomendationList.Count != 0)
            {
                if (causeRec != null)
            {
                causeRec.recomendationList = recomendationList;
                rule = add_rule(causeRec);
                causeRec.ruleR = rule;
                Close();
                             
            }
            else
            {             
                    CauseRecomendation c = new CauseRecomendation();
                    c.cause = new Cause(label_causa.Content.ToString());
                    c.recomendationList = recomendationList;
                    c.ruleN = label_causa.Content.ToString();
                    rule = add_rule(c);
                    CauseRecomendation cr = new CauseRecomendation(new Cause(label_causa.Content.ToString()), recomendationList, rule, label_causa.Content.ToString());
                    main.process.ElementAt(main.selectedIndex).rulList.AddLast(cr);
                   // MessageBox.Show(rule);
                    Close();
                }
               
            }
            else
            {
                MessageBox.Show("Debe insertar recomendaciones");
            }
            wind.fill_table(main);
        }

        private void button_delete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedIndex!=-1) {
          
            Recomendation rec = ((Recomendation)dataGrid.SelectedItem);
            recomendationList.Remove(rec);
            data.Remove(rec);
            dataGrid.DataContext = data;

            }
            else
                MessageBox.Show("Debe seleccionar una recomendación");
        }
        public void clean_Data()
        {
            comboBox_recomen.Text = "";
     }
        public String add_rule(CauseRecomendation cr) {

            String rule = "rule \"" + cr.ruleN + "\"";

            rule += "/when/  Causa( causa matches \"" + cr.ruleN + "\" )" + "/then/" + "/ GeneralTree<String> tree = new GeneralTree<String>();/" + "BinaryTreeNode<String> root = new BinaryTreeNode<String>(new String(\"" + cr.ruleN + "\")); /" + "/tree.insertNode(root, null);";

            if (cr.recomendationList.Count > 0)
            {
                for (int i = 0; i < cr.recomendationList.Count; i++)
                {
                    string r = cr.recomendationList.ElementAt(i).staticRecomendation;

                    string temp = main.sim(r);

                    rule += "/ BinaryTreeNode<String>" + "node" + temp + "= new BinaryTreeNode<String>(new String(" + "\"" + r + "\"" + "));";
                    rule += "/ tree.insertNode(" + "node" + temp + ", root);";
                }
                rule += "/ProcesarDatos.cauAnalizadas.add(new Causa(\"\",\"" + cr.ruleN + "\"" + ",tree));";
                rule += "/javax.swing.JOptionPane.showMessageDialog(new JFrame(),\"Las posibles recomendaciones son:";

                for (int j = 0; j < cr.recomendationList.Count; j++)
                {
                    String r = cr.recomendationList.ElementAt(j).staticRecomendation;
                    rule +=r;
                    if (j != cr.recomendationList.Count - 1)
                    {
                        rule += " , ";
                    }
                }
            }

            rule += "\");/end/";

            return rule;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
         this.Close();
         }

        private void Window_Closed(object sender, EventArgs e)
        {
            wind.IsEnabled = true;
        }
    }
}
