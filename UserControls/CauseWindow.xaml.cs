using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TesisAnaReglas.BussinesLogic;

namespace TesisAnaReglas.UserControls
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public class RuleCause
    {
        private Rule _c;
        private Cause _r;

        public Rule rule
        {
            get { return _c; }
            set { _c = value; }
        }

        public Cause cause
        {
            get { return _r; }
            set { _r = value; }
        }
    }
    public partial class Window2 : Window
    {
        private MainWindow main { get; set; }
        private LinkedList<Cause> causeList = new LinkedList<Cause>();
        public ObservableCollection<Cause> data = new ObservableCollection<Cause>();
        public Cause temp = null;
        public bool inAction = false;
        public Window2(MainWindow main)
        {
            InitializeComponent();
            this.main = main;
            fill_table(main);
        }

        public void fill_table(MainWindow main)
        {
            data.Clear();

            if (main.process.ElementAt(main.selectedIndex) != null)
            {
                if (main.process.ElementAt(main.selectedIndex).cauList != null)
                {
                    foreach (Cause caus in main.process.ElementAt(main.selectedIndex).cauList)
                    {
                        data.Add(caus);
                        causeList.AddLast(caus);
                    }
                    dg.DataContext = data;
                }
            }
        }

        private void add_cause(object sender, RoutedEventArgs e)
        {
            cause c = new cause(main, null, this);
            c.Show();
            this.IsEnabled = false;
        }

        private void remove_cause(object sender, RoutedEventArgs e)
        {
            exist_rule(temp);

            if (dg.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una causa.");
            }
            else
            {
                if (existMultiple.Count > 0 || existSimple.Count > 0)
                {
                    const string message = "Si usted elimina esta causa, tambien la eliminará de las asociaciones donde se encuentre.Si es la única en una regla se eliminará la regla.";
                    const string caption = "Alerta";
                    var result = MessageBox.Show(message, caption, MessageBoxButton.OKCancel);

                    if (result == MessageBoxResult.OK)
                    {
                        delete_rule();
                        inAction = true;
                        Cause cau = ((Cause)dg.SelectedItem);
                        causeList.Remove(cau);
                        data.Remove(cau);
                        main.process.ElementAt(main.selectedIndex).cauList.Remove(cau);
                        dg.DataContext = data;
                    }
                }
                else
                {
                    inAction = true;
                    Cause cau = ((Cause)dg.SelectedItem);
                    causeList.Remove(cau);
                    data.Remove(cau);
                    main.process.ElementAt(main.selectedIndex).cauList.Remove(cau);
                    dg.DataContext = data;
                }
                existMultiple.Clear();
                existSimple.Clear();
            }
        }

        private void modify_cause(object sender, RoutedEventArgs e)
        {

            if (dg.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una causa.");
            }
            else {

                exist_rule(temp);
                if (existSimple.Count > 0 || existMultiple.Count > 0)
                {
                    const string message = "Esta causa ya tiene asociadas sus recomendaciones,si la modifica tambien se modifica en la asociaciones donde se encuentra.";
                    const string caption = "Alerta";
                    var result = MessageBox.Show(message, caption, MessageBoxButton.OKCancel);

                    if (result == MessageBoxResult.OK)
                    {
                        cause v = new cause(main, temp, this);
                        v.Show();
                        this.IsEnabled = false;
                    }
                }
                else
                {
                    cause v = new cause(main, temp, this);
                    v.Show();
                    this.IsEnabled = false;
                }
              
            }
        }

        private int index = -1;
        private void dg_SelectionChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (!inAction)
            {
                Cause cau = ((Cause)dg.SelectedItem);
                temp = cau;
                index = dg.SelectedIndex;
            }
            inAction = false;
        }

        private void button_Acept_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public LinkedList<RuleCause> existMultiple = new LinkedList<RuleCause>();
        public LinkedList<RuleCause> existSimple = new LinkedList<RuleCause>();
        public void exist_rule(Cause c)
        {
            if (c != null)
            {
                if (main.process.ElementAt(main.selectedIndex).rulList != null)
                {
                    foreach (Rule rul in main.process.ElementAt(main.selectedIndex).rulList)
                    {
                        if (rul is CauseRecomendation)
                        {
                            CauseRecomendation cr = ((CauseRecomendation)rul);
                            if (c.staticCause.Equals(cr.cause.staticCause)) {

                                existSimple.AddFirst(new RuleCause() { rule = rul, cause = cr.cause }); break;
                            }
                        }
                        else if (rul is VariableCause)
                        {
                            VariableCause vc = ((VariableCause)rul);
                            for (int i = 0; i < vc.causeList.Count; i++)
                            {
                                if (c.staticCause.Equals(vc.causeList.ElementAt(i).staticCause) && vc.causeList.Count > 1)
                                {
                                    existMultiple.AddFirst(new RuleCause() { rule = rul, cause = vc.causeList.ElementAt(i) }); break;
                                }
                                else if (c.staticCause.Equals(vc.causeList.ElementAt(i).staticCause) && vc.causeList.Count == 1)
                                {
                                    existSimple.AddFirst(new RuleCause() { rule = rul, cause = vc.causeList.ElementAt(i) }); break;
                                }
                            }
                        }
                    }
                }
            }
        }
        public void delete_rule()
        {
            foreach (RuleCause rul in existMultiple)
            {
                ((VariableCause)main.process.ElementAt(main.selectedIndex).rulList.Find(rul.rule).Value).causeList.Remove(rul.cause);
            }
            foreach (RuleCause rul in existSimple)
            {
                main.process.ElementAt(main.selectedIndex).rulList.Remove(rul.rule);
            }
        }
        public void modif_rule()
        {
            foreach (RuleCause rul in existMultiple)
            {
               (((VariableCause)main.process.ElementAt(main.selectedIndex).rulList.Find(rul.rule).Value).causeList.Find(rul.cause).Value)=temp;
            }
            foreach (RuleCause rul in existSimple)
            {
                if (main.process.ElementAt(main.selectedIndex).rulList.Find(rul.rule).Value is CauseRecomendation)
                {
                    ((CauseRecomendation)main.process.ElementAt(main.selectedIndex).rulList.Find(rul.rule).Value).cause = temp;
                }
                else {
                    if (main.process.ElementAt(main.selectedIndex).rulList.Find(rul.rule).Value is VariableCause)
                    {
                        (((VariableCause)main.process.ElementAt(main.selectedIndex).rulList.Find(rul.rule).Value).causeList.Find(rul.cause).Value) = temp;
                    }
                }

            }
       }

      
    }
   
}
