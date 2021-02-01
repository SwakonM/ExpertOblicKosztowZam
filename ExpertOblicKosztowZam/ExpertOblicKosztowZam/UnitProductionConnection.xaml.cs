using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace ExpertOblicKosztowZam
{
    /// <summary>
    /// Interaction logic for UnitProductionConnection.xaml
    /// </summary>
    public partial class UnitProductionConnection : UserControl, INotifyPropertyChanged
    {
        public UnitProductionConnection()
        {
            InitializeComponent();
            tbUnitProdIn.DataContext = this;
            tbUnitProdOut.DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;


        private void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));

        }



        public string Name1
        {
            set { UnitProdInText = value; }
        }

        public string Name2
        {
            set {  UnitProdOutText = value; }
        }

        private string _UnitProdInText;

        public string UnitProdInText
        {
            get { return _UnitProdInText; }
            set { _UnitProdInText =  value; NotifyPropertyChanged("UnitProdInText"); }
        }
        private string _UnitProdOutText;

        public string UnitProdOutText
        {
            get { return _UnitProdOutText; }
            set { _UnitProdOutText = value; NotifyPropertyChanged("UnitProdOutText"); }
        }
    }
}
