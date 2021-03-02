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
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace ExpertOblicKosztowZam
{

    

    /// <summary>
    /// Interaction logic for ucOrders.xaml
    /// </summary>
    public partial class ucOrders : UserControl, ITabbedMDI, INotifyPropertyChanged
    {
        private double _from;
        private double _to;
        private  ChartValues<GanttPoint> _values;

        public List<TabZamowinia> my_ListZamowin;
        List<string> my_ColumnName;
        List<List<int>> sequenceList = new List<List<int>>();

        #region ITabbedMDI Members
        /// <summary>
        /// This event will be fired when user will click close button
        /// </summary>
        public event delClosed CloseInitiated;

        /// <summary>
        /// This is unique name of the tab
        /// </summary>
        public string UniqueTabName
        {
            get
            {
                return "ucOrders";
            }
        }

        /// <summary>
        /// This is the title that will be shown in the tab.
        /// </summary>
        public string Title
        {
            get { return "Harmonogram zamówień"; }
        }
#endregion
        public ucOrders(ref List<TabZamowinia> ListZamowin, ref List<string> ColumnName)
        {
            InitializeComponent();
            my_ListZamowin = ListZamowin;
            my_ColumnName = ColumnName;
            sequenceList = Proces.GenerateSequenceIndex(my_ColumnName);
            this.OrderList.SelectionChanged += new SelectionChangedEventHandler(OnOrderListBoxChanged);
            ConnectionViewModel vm = new ConnectionViewModel(ref my_ListZamowin);
          
            OrderList.DataContext = vm;
            OrderList.SelectedIndex = -1;

        }

        private void OnOrderListBoxChanged(object sender, SelectionChangedEventArgs e)
        {
            
            GenerateChart(this.OrderList.SelectedIndex);
        }

        private void GenerateChart( int v)
        {
           _values = new ChartValues<GanttPoint>();
            var labels = new List<string>();
            _values.Clear();
            OrderSchedule.DataContext = null;
            if (v < 0)
            {
               
                DataContext = this;
                return;

            }

           
            var zam = my_ListZamowin.ElementAt(v);

            var now = DateTime.Now;
            DateTime hms = new DateTime(now.Year, now.Month, now.Day, 8, 0, 0);
       
            List<Double> timeList = new List<Double>();
            Double tempTime = 0; ;
            foreach (var ity in sequenceList.ElementAt(0))
            {
                timeList.Add(tempTime);
                tempTime += zam._Param[my_ColumnName.ElementAt(ity)];
            }

            foreach (var tempList in sequenceList)
            {
                int firstIndex = FindFirstIndex(my_ColumnName, sequenceList, tempList);
                Double starTime = timeList.ElementAt(firstIndex);
                Double endTime = starTime;
                foreach (var ity in tempList)
                {
                    endTime += zam._Param[my_ColumnName.ElementAt(ity)];
                    _values.Add(new GanttPoint(hms.AddMinutes(starTime).Ticks, hms.AddMinutes(endTime).Ticks));
                    starTime += zam._Param[my_ColumnName.ElementAt(ity)];
                    labels.Add("Task: " + my_ColumnName.ElementAt(ity));

                }
            }
           

            Series = new SeriesCollection
             {
                 new RowSeries
                 {
                     Values = _values,
                     DataLabels = true
                 }
             };
            Formatter = value => new DateTime((long)value).ToString("H:mm");

           
            //for (var i = 0; i < _values.Count; i++)
            //    labels.Add("Task " + i);
            Labels = labels.ToArray();

            ResetZoomOnClick(null, null);

            OrderSchedule.DataContext = this;
        }

        private int FindFirstIndex(List<string> my_ColumnName, List<List<int>> sequenceList, List<int> elem)
        {

            var first = elem.ElementAt(0);
            var count = 0;
            foreach (var ity in sequenceList.ElementAt(0))
            {

                var temp1 = my_ColumnName[ity].Substring(0, my_ColumnName[ity].IndexOf("->"));
                var temp2 = my_ColumnName[first].Substring(0, my_ColumnName[first].IndexOf("->"));
                if (temp1 == temp2)
                {
                    return count;
                }
                count++;
            }
            return 0;
        }
        public SeriesCollection Series { get; set; }
        public Func<double, string> Formatter { get; set; }
        public string[] Labels { get; set; }

        public double From
        {
            get { return _from; }
            set
            {
                _from = value;
                OnPropertyChanged("From");
            }
        }

        public double To
        {
            get { return _to; }
            set
            {
                _to = value;
                OnPropertyChanged("To");
            }
        }

        private void ResetZoomOnClick(object sender, RoutedEventArgs e)
        {
            From = _values.First().StartPoint;
            To = _values.Last().EndPoint;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }



    }

    public class OrderBookEntry
    {
        public string Name { get; set; }
        public OrderBookEntry(string name)
        {
            Name = name;
        }
    }
    public class ConnectionViewModel : INotifyPropertyChanged
    {
        public List<string> m_ListZam;
        private string _name;

        public ConnectionViewModel(ref List<TabZamowinia> ListZamowin)
        {
            m_ListZam= ListZamowin.Select(c => c.IdZamowinia).ToList();
            List<OrderBookEntry> list = new  List<OrderBookEntry>();
            foreach(var ity in m_ListZam)
            {
              
                list.Add(new OrderBookEntry(ity));

            }
            
            _orderbookEntries = new CollectionView(list.ToList());
        }

        

        private readonly CollectionView _orderbookEntries;
        private string _orderbookEntry;

        public CollectionView OrderbookEntries
        {
            get { return _orderbookEntries; }
        }

        public string OrderbookEntry
        {
            get { return _orderbookEntry; }
            set
            {
                if (_orderbookEntry == value) return;
                _orderbookEntry = value;
                OnPropertyChanged("OrderbookEntry");
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value) return;
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }

   
}
