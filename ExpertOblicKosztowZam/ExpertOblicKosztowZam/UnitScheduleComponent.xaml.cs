using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace ExpertOblicKosztowZam
{
    /// <summary>
    /// Interaction logic for ScheduleComponent.xaml
    /// </summary>
    public partial class UnitScheduleComponent : UserControl, INotifyPropertyChanged
    {
        public UnitScheduleComponent()
        {
            InitializeComponent();

            UnitProdBtt.DataContext = this;
            tbUnitProdTime.DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;


        private void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));

        }

        public string UnitProdText
        {
            set {

                UnitProdBttText = value;
                UnitProdTimeText = "t_(" + value + ")";
            }
        }

      

        private string _UnitProdBttText;

        public string UnitProdBttText
        {
            get { return _UnitProdBttText; }
            set { _UnitProdBttText = value; NotifyPropertyChanged("UnitProdBttText"); }
        }
        private string _UnitProdTimeText;

        public string UnitProdTimeText
        {
            get { return _UnitProdTimeText; }
            set { _UnitProdTimeText = value; NotifyPropertyChanged("UnitProdTimeText"); }
        }


    }
}
