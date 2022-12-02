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
using Illusion.Utility;
using TesisAnaReglas.BussinesLogic;
using ListBox = System.Windows.Forms.ListBox;

namespace TesisAnaReglas.UserControls
{
    /// <summary>
    /// Interaction logic for RecomendationWindow.xaml
    /// </summary>
    /// 
   public class RuleRecom
    {
        private Rule _c;
        private Recomendation _r;

    public Rule rule
    {
        get { return _c; }
        set { _c = value; }
    }

    public Recomendation recomendation
    {
        get { return _r; }
        set { _r = value; }
    }
}
     public partial class RecomendationWindow : Window
    {
        private MainWindow main { get; set; }
        private LinkedList<Recomendation> recomenList = new LinkedList<Recomendation>();
        public ObservableCollection<Recomendation> data = new ObservableCollection<Recomendation>();
        public Recomendation temp = null;
        public bool inAction = false;
        public RecomendationWindow(MainWindow main)
        {
            InitializeComponent();
            this.main = main;
            fill_table(main);
        }
        public void fill_table(MainWindow main)
        {
            data.Clear();
            datag.DataContext = data;
            if (main.process.ElementAt(main.selectedIndex) != null)
            {
                if (main.process.ElementAt(main.selectedIndex).recList != null)
                {
                    foreach (Recomendation rec in main.process.ElementAt(main.selectedIndex).recList)
                    {
                        data.Add(rec);
                        recomenList.AddLast(rec);
                    }
                    datag.DataContext = data;
                }
             }
        }
        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            recomendation r = new recomendation(main,null ,this);
            r.Show();
            this.IsEnabled = false;
        }

        private void button_delete_Click(object sender, RoutedEventArgs e)
        {
            exist_rule(temp);

            if (datag.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una recomendación.");

            }
            else
            {
                if (existMultiple.Count>0 || existSimple.Count > 0)
                {
                    const string message = "Si usted elimina esta recomendación, tambien la eliminará de cada regla donde se encuentre. Si es la única en una regla se eliminará la regla";
                    const string caption = "Alerta";
                    var result = MessageBox.Show(message, caption, MessageBoxButton.OKCancel);

                    if (result == MessageBoxResult.OK)
                    {
                        delete_rule();
                        Recomendation rec = ((Recomendation)datag.SelectedItem);
                        recomenList.Remove(rec);
                        data.Remove(rec);
                        main.process.ElementAt(main.selectedIndex).recList.Remove(rec);
                        datag.DataContext = data;
                    }
                }
                else
                {
                    Recomendation rec = ((Recomendation)datag.SelectedItem);
                    recomenList.Remove(rec);
                    data.Remove(rec);
                    main.process.ElementAt(main.selectedIndex).recList.Remove(rec);
                    datag.DataContext = data;
                }
                existSimple.Clear();
                existMultiple.Clear();
            }
        }

        private void button_modify_Click(object sender, RoutedEventArgs e)
        {
           
            if (datag.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una recomendación.");
            }
            else
            {
                exist_rule(temp);
                if (existSimple.Count > 0 || existMultiple.Count > 0)
                {
                    const string message = "Esta recomendación ya está asociada a una causa,si la modifica tambien se modifica en la asociaciones donde se encuentra.";
                    const string caption = "Alerta";
                    var result = MessageBox.Show(message, caption, MessageBoxButton.OKCancel);

                    if (result == MessageBoxResult.OK)
                    {
                        recomendation v = new recomendation(main, temp, this);
                        v.Show();
                        this.IsEnabled = false;
                    }
                }
                else
                {
                    recomendation v = new recomendation(main, temp, this);
                    v.Show();
                    this.IsEnabled = false;
                }
              
            }
           
        }

        private void datag_SelectionChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (!inAction)
            {
                Recomendation rec = ((Recomendation)datag.SelectedItem);
                temp = rec;
             }
            inAction = false;
        }

        private void button_Acept_Click(object sender, RoutedEventArgs e)
        {
             this.Close();
        }

        public LinkedList<RuleRecom> existMultiple = new LinkedList<RuleRecom>();
        public LinkedList<RuleRecom> existSimple = new LinkedList<RuleRecom>();
        private void exist_rule(Recomendation r)
        {
         if (r != null)
            {
                if (main.process.ElementAt(main.selectedIndex).rulList != null)
                {
                    foreach (Rule rul in main.process.ElementAt(main.selectedIndex).rulList)
                    {
                        if (rul is CauseRecomendation)
                        {
                            CauseRecomendation cr = ((CauseRecomendation)rul);
                            for (int i = 0; i < cr.recomendationList.Count; i++)
                            {
                                if (r.staticRecomendation.Equals(cr.recomendationList.ElementAt(i).staticRecomendation) && cr.recomendationList.Count > 1)
                                {
                                    existMultiple.AddFirst(new RuleRecom() {rule = rul,recomendation = cr.recomendationList.ElementAt(i)});break;
                                }else if (r.staticRecomendation.Equals( cr.recomendationList.ElementAt(i).staticRecomendation) && cr.recomendationList.Count == 1)
                                {
                                    existSimple.AddFirst(new RuleRecom() { rule = rul, recomendation = cr.recomendationList.ElementAt(i) });break;
                                }
                            }
                        }
                    }
                }
            }
        } 
        public void delete_rule()
        {
            foreach (RuleRecom var in existMultiple)
            {
              ((CauseRecomendation) main.process.ElementAt(main.selectedIndex).rulList.Find(var.rule).Value).recomendationList.Remove(var.recomendation);
              }
            foreach (RuleRecom var in existSimple)
            {
                main.process.ElementAt(main.selectedIndex).rulList.Remove(var.rule);
            }
        }
        public void modif_rule()
        {
            foreach (RuleRecom rul in existMultiple)
            {
             (((CauseRecomendation)main.process.ElementAt(main.selectedIndex).rulList.Find(rul.rule).Value).recomendationList.Find(rul.recomendation).Value) = temp;
            }
            foreach (RuleRecom rul in existSimple)
            {
             (((CauseRecomendation)main.process.ElementAt(main.selectedIndex).rulList.Find(rul.rule).Value).recomendationList.Find(rul.recomendation).Value) = temp;
            }
        }
    }
   }
