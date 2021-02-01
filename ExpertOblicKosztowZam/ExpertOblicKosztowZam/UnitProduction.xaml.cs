using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;


namespace ExpertOblicKosztowZam
{
    /// <summary>
    /// Interaction logic for UnitProduction.xaml
    /// </summary>
    public partial class UnitProduction : UserControl, INotifyPropertyChanged
    {
        public UnitProduction()
        {
            InitializeComponent();
            UnitProdBtt.DataContext = this;
            tbUnitProdIn.DataContext = this;
            tbUnitProdOut.DataContext = this;

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler UserControlClicked;

        private void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));

        }


     
        public string Name
        {
         set{
                UnitProdBttText = value; 
                if(value!="z")
                {
                    UnitProdInText = value; 
                    UnitProdOutText = value; 
                }

               
            }
        }
         
        private string _UnitProdBttText;

        public string UnitProdBttText
        {
            get { return _UnitProdBttText; }
            set { _UnitProdBttText = value; NotifyPropertyChanged("UnitProdBttText"); }
        }

        private string _UnitProdInText;

        public string UnitProdInText
        {
            get { return _UnitProdInText; }
            set { _UnitProdInText = "o -> "+value; NotifyPropertyChanged("UnitProdInText"); }
        }
        private string _UnitProdOutText;

        public string UnitProdOutText
        {
            get { return _UnitProdOutText; }
            set { _UnitProdOutText = value+" -> o"; NotifyPropertyChanged("UnitProdOutText"); }
        }

        public object EventHandler { get; internal set; }

        private void UnitProdBtt_Click(object sender, RoutedEventArgs e)
        {
            if (UserControlClicked != null)
            {
                UserControlClicked(this, EventArgs.Empty);
            }
        }
    }
}
