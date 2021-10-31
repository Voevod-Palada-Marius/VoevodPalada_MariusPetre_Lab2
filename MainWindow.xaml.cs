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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;

namespace VoevodPalada_MariusPetre_Lab2
{
   
    public partial class MainWindow : Window
    {
        enum PizzaType
        {
            Margherita,
            Pepperoni,
            Veggie,
            Quattro_Stagioni,
            Canibale
        }

        

        PizzaType selectedPizza;

        KeyValuePair<PizzaType, double>[] PriceList =
        {
                new KeyValuePair<PizzaType, double>(PizzaType.Margherita, 21),
                new KeyValuePair<PizzaType, double>(PizzaType.Pepperoni, 23),
                new KeyValuePair<PizzaType, double>(PizzaType.Veggie, 20),
                new KeyValuePair<PizzaType, double>(PizzaType.Quattro_Stagioni, 27),
                new KeyValuePair<PizzaType, double>(PizzaType.Canibale, 30)
                };

        private PizzaMachine myPizzaMachine;

        private int mMargheritaPizza;
        private int mPepperoniPizza;
        private int mVeggiePizza;
        private int mQuattroStagioniPizza;
        private int mCanibalePizza;

        private void btnRemoveItem_Click(object sender, RoutedEventArgs e)
        {
            lstSale.Items.Remove(lstSale.SelectedItem);
        }

        PizzaType selectedPizza;

        KeyValuePair<PizzaType, double>[] PriceList =
        {
                new KeyValuePair<PizzaType, double>(PizzaType.Margherita, 21),
                new KeyValuePair<PizzaType, double>(PizzaType.Pepperoni, 23),
                new KeyValuePair<PizzaType, double>(PizzaType.Veggie, 20),
                new KeyValuePair<PizzaType, double>(PizzaType.Quattro_Stagioni, 27),
                new KeyValuePair<PizzaType, double>(PizzaType.Canibale, 30)
                };


        private void cmbType_SelectionChanged(object sender, RoutedEventArgs e)
        {
            txtPrice.Text = cmbType.SelectedValue.ToString();
            KeyValuePair<PizzaType, double> selectedEntry = (KeyValuePair<PizzaType, double>)
            cmbType.SelectedItem;
            selectedPizza = selectedEntry.Key;
        }

        private int ValidateQuantity(PizzaType selectedPizza)
        {
            int q = int.Parse(txtQuantity.Text);
            int r = 1;
            switch (selectedPizza)
            {
                case PizzaType.Margherita:
                    if (q > mMargheritaPizza)
                        r = 0;
                    break;
                case PizzaType.Pepperoni:
                    if (q > mPepperoniPizza)
                        r = 0;
                    break;
                case PizzaType.Veggie:
                    if (q > mVeggiePizza)
                        r = 0;
                    break;

                case PizzaType.Canibale:
                    if (q > mCanibalePizza)
                        r = 0;
                    break;

                case PizzaType.Quattro_Stagioni:
                    if (q > mQuattro_Stagioni)
                        r = 0;
                    break;


            }
            return r;
        }

        private void btnCheckOut_Click(object sender, RoutedEventArgs e)
        {
            txtTotal.Text = (double.Parse(txtTotal.Text) + double.Parse(txtQuantity.Text) *
           double.Parse(txtPrice.Text)).ToString();
            foreach (string s in lstSale.Items)
            {
                switch (s.Substring(s.IndexOf(" ") + 1, s.IndexOf(":") - s.IndexOf(" ") - 1))
                {
                    case "Margherita":
                        mMargheritaPizza = mMargheritaPizza - Int32.Parse(s.Substring(0,
                       s.IndexOf(" ")));
                        txtMargheritaPizza.Text = mMargheritaPizza.ToString();
                        break;
                    case "Pepperoni":
                        mPepperoniPizza = mPepperoniPizza - Int32.Parse(s.Substring(0,
                       s.IndexOf(" ")));
                        txtPepperoniPizza.Text = mPepperoniPizza.ToString();
                        break;
                    case "Veggie":
                        mVeggiePizza = mVeggiePizza - Int32.Parse(s.Substring(0,
                       s.IndexOf(" ")));
                        txtVeggiePizza.Text = mVeggiePizza.ToString();
                        break;

                    case "Canibale":
                        mCanibalePizza = mCanibalePizza - Int32.Parse(s.Substring(0,
                       s.IndexOf(" ")));
                        txtCanibalePizza.Text = mCanibalePizza.ToString();
                        break;

                    case "Quattro_Stagioni":
                        mQuattro_StagioniPizza = mQuattro_StagioniPizza - Int32.Parse(s.Substring(0,
                       s.IndexOf(" ")));
                        txtQuattro_StagioniPizza.Text = mQuattro_StagioniPizza.ToString();
                        break;
                }
            }
        }
        private void PizzaItemsShow_Click(object sender, RoutedEventArgs e)
        {
            string mesaj;
            MenuItem SelectedItem = (MenuItem)e.OriginalSource;
            string stringHeader = SelectedItem.Header as string;
            switch (stringHeader)
            {
                case "Stop":
                    this.Title = "Stopped Machine";
                    break;
                case "Inventory":
                    this.Title = "Checking the inventory...";
                    break;
                default:
                    mesaj = SelectedItem.Header.ToString() + " is being cooked!";
                    this.Title = mesaj;
                    break;
            }
        }





        public MainWindow()
        {
                InitializeComponent();

            //creare obiect binding pentru comanda
            CommandBinding cmd1 = new CommandBinding();
            //asociere comanda
            cmd1.Command = ApplicationCommands.Print;
            //asociem un handler
            cmd1.Executed += new ExecutedRoutedEventHandler(CtrlP_CommandHandler);
            //adaugam la colectia CommandBindings
            this.CommandBindings.Add(cmd1);

            CommandBinding cmd2 = new CommandBinding();
            cmd2.Command = CustomCommands.StopCommand.Launch;
            cmd2.Executed += new ExecutedRoutedEventHandler(CtrlS_CommandHandler);
            this.CommandBindings.Add(cmd2);

            private void CtrlP_CommandHandler(object sender, ExecutedRoutedEventArgs e)
            {
                //handler pentru comanda Ctrl+S -> se va executa stopMenuItem_Click
                MessageBox.Show("Ctrl+S was pressed! The pizza machine will stop!");
                this.stopMenuItem_Click(sender, e);

                MessageBox.Show("You have in stock:" + mMargheritaPizza + " Margherita pizza," +
                mPepperoniPizza + " Pepperoni pizza, " + mVeggiePizza + " Veggie Pizza," +
                mQuattroStagioniPizza + " Quattro Stagioni pizza, " + mCanibalePizza + " Canibale pizza"
                );
            }
            //input gesture: Alt + I
            ApplicationCommands.Print.InputGestures.Add(new KeyGesture(Key.I, ModifierKeys.Alt));



            

        }

        private void margPizzaMenuItem_Click(object sender, RoutedEventArgs e)
        {
            margPizzaMenuItem.IsChecked = true;
            pepPizzaMenuItem.IsChecked = false;
            vegPizzaMenuItem.IsChecked = false;
            quatPizzaMenuItem.IsChecked = false;
            canPizzaMenuItem.IsChecked = false;
            myPizzaMachine.MakePizzas(PizzaType.Margherita);
        }
        private void pepPizzaMenuItem_Click(object sender, RoutedEventArgs e)
        {
            margPizzaMenuItem.IsChecked = false;
            pepPizzaMenuItem.IsChecked = true;
            vegPizzaMenuItem.IsChecked = false;
            quatPizzaMenuItem.IsChecked = false;
            canPizzaMenuItem.IsChecked = false;
            myPizzaMachine.MakePizzas(PizzaType.Pepperoni);
        }
        private void vegPizzaMenuItem_Click(object sender, RoutedEventArgs e)
        {
            margPizzaMenuItem.IsChecked = false;
            pepPizzaMenuItem.IsChecked = false;
            vegPizzaMenuItem.IsChecked = true;
            quatPizzaMenuItem.IsChecked = false;
            canPizzaMenuItem.IsChecked = false;
            myPizzaMachine.MakePizzas(PizzaType.Veggie);
        }
        private void quatPizzaMenuItem_Click(object sender, RoutedEventArgs e)
        {
            margPizzaMenuItem.IsChecked = false;
            pepPizzaMenuItem.IsChecked = false;
            vegPizzaMenuItem.IsChecked = false;
            quatPizzaMenuItem.IsChecked = true;
            canPizzaMenuItem.IsChecked = false;
            myPizzaMachine.MakePizzas(PizzaType.Quatro);
        }
        private void canPizzaMenuItem_Click(object sender, RoutedEventArgs e)
        {
            margPizzaMenuItem.IsChecked = false;
            pepPizzaMenuItem.IsChecked = false;
            vegPizzaMenuItem.IsChecked = false;
            quatPizzaMenuItem.IsChecked = false;
            canPizzaMenuItem.IsChecked = true;
            myPizzaMachine.MakePizzas(PizzaType.Canibale);
        }

        private void PizzaCompleteHandler()
        {
            switch (myPizzaMachine.Ingredients)
            {
                case PizzaType.Margherita:
                    mMargheritaPizza++;
                    txtMargheritaPizza.Text = mMargheritaPizza.ToString();
                    break;
                case PizzaType.Pepperoni:
                    mPepperoniPizza++;
                    txtPepperoniPizza.Text = mPepperoniPizza.ToString();
                    break;
                case PizzaType.Veggie:
                    mVeggiePizza++;
                    txtVeggiePizza.Text = mVeggiePizza.ToString();
                    break;
                case PizzaType.Quattro_Stagioni:
                    mQuattro_StagioniPizza++;
                    txtQuattro_StagioniPizza.Text = mQuattro_StagioniPizza.ToString();
                    break;

                   
            }
        }


        private void txtVeggiePizza_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtQuatroPizza_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtCanibalePizza_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnCheckOut_Click(object sender, RoutedEventArgs e)
        {

        }
        private void stopMenuItem_Click(object sender, RoutedEventArgs e)
        {
            myPizzaMachine.Enabled = false;
        }


        private void frmMain_Loaded(object sender, RoutedEventArgs e)
            {
                myPizzaMachine = new PizzaMachine();
            myPizzaMachine.PizzaComplete += new PizzaMachine.PizzaCompleteDelegate(PizzaCompleteHandler);
            cmbType.ItemsSource = PriceList;
            cmbType.DisplayMemberPath = "Key";
            cmbType.SelectedValuePath = "Value";

        }

        private void exitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void txtQuantity_KeyUp(object sender, KeyEventArgs e)
        {
            if (!(e.Key >= Key.D0 && e.Key <= Key.D9))
            {
                MessageBox.Show("Numai cifre se pot introduce!", "Input Error", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }
    }
}
