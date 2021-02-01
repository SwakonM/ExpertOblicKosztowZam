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

namespace ExpertOblicKosztowZam
{
    /// <summary>
    /// Interaction logic for ucSchedule.xaml
    /// </summary>
    public partial class ucSchedule : UserControl, ITabbedMDI
    {

        public List<TabZamowinia> my_ListZamowin;
        List<string> my_ColumnName;
        List<int> sequenceList = new List<int>();
        public ucSchedule(ref List<TabZamowinia> ListZamowin, ref List<string> ColumnName)
        {
            InitializeComponent();
            my_ListZamowin = ListZamowin;
            my_ColumnName = ColumnName;
        }

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
                return "ucSchedule";
            }
        }

        /// <summary>
        /// This is the title that will be shown in the tab.
        /// </summary>
        public string Title
        {
            get { return "Harmonogram procesu"; }
        }
        #endregion

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            sequenceList= GenerateSequenceIndex();
            CreateDynamicWPFGrid(0);
        }

        private void CreateDynamicWPFGrid(int rowId)
        {
          

            // Create the Grid
            var DynamicScrollViewer = new ScrollViewer();
            DynamicScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            DynamicScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
            
            var DynamicGrid = new Grid { ShowGridLines = false };

             //DynamicGrid.Width = 800;
           // DynamicGrid.Height= 800;
            DynamicGrid.HorizontalAlignment = HorizontalAlignment.Stretch;

            DynamicGrid.VerticalAlignment = VerticalAlignment.Stretch;

            //    DynamicGrid.ShowGridLines = true;

            DynamicGrid.Background = new SolidColorBrush(Colors.LightSteelBlue);
            // Create Columns
            for (var i = 0; i < sequenceList.Count()+2; i++)
            {
                var gridCol1 = new ColumnDefinition() { Width = new GridLength(55) };
                var gridCol2 = new ColumnDefinition() { Width = new GridLength(55) };
                
                DynamicGrid.ColumnDefinitions.Add(gridCol1);
                DynamicGrid.ColumnDefinitions.Add(gridCol2);
               
            }
            // Create Rows
            var gridRow = new RowDefinition() { Height = new GridLength(120) };
            DynamicGrid.RowDefinitions.Add(gridRow);

            for (var i = 0; i < sequenceList.Count()+2; i++)
            {
                var gridRow1 = new RowDefinition() {Height = new GridLength(38) };
                DynamicGrid.RowDefinitions.Add(gridRow1);
            }
            var gridRow2 = new RowDefinition() { Height = new GridLength(120) };
            DynamicGrid.RowDefinitions.Add(gridRow2);
            int count = 0;
            foreach (var index in sequenceList)
            {

                var unitSchedule = new UnitScheduleComponent();
                unitSchedule.UnitProdText= my_ColumnName[index].Replace("__in","<--o").Replace("__out","-->o");
                Grid.SetColumnSpan(unitSchedule, 3);
                    Grid.SetRow(unitSchedule, count+1);
                Grid.SetColumn(unitSchedule, count*2);
                Grid.SetRowSpan(unitSchedule, 2);
                DynamicGrid.Children.Add(unitSchedule);

                count++;
            }

            ScottPlot.WpfPlot wpfPlot1 = new ScottPlot.WpfPlot();
            var plt = new ScottPlot.Plot(600, 400);

            ScottPlot.OHLC[] ohlcs = ScottPlot.DataGen.RandomStockPrices(rand: null, pointCount: 60, deltaMinutes: 10);
            wpfPlot1.plt.Title("Ten Minute Candlestick Chart");
            wpfPlot1.plt.YLabel("Stock Price (USD)");
            wpfPlot1.plt.PlotCandlestick(ohlcs);
            wpfPlot1.plt.Ticks(dateTimeX: true);

           
            wpfPlot1.Render();
            Grid.SetRow(wpfPlot1, count + 3);
            Grid.SetColumnSpan(wpfPlot1, sequenceList.Count());
            DynamicGrid.Children.Add(wpfPlot1);
            DynamicScrollViewer.Content = DynamicGrid;
    
            // Display grid into a Window

            this.Content = DynamicScrollViewer;

        }

        private List<int> GenerateSequenceIndex()
        {
            List<string> listModules = GetListModules(my_ColumnName);
            Dictionary<int, Tuple<string, string>> tempList = new Dictionary<int, Tuple<string, string>>();

            int cout = 0;
            foreach (var ity in my_ColumnName)
            {

                if (ity.IndexOf("->") > 0)
                {

                    var temp1 = ity.Substring(0, ity.IndexOf("->"));

                    var temp2 = ity.Substring(ity.IndexOf("->") + 2);
                    tempList.Add(cout, Tuple.Create(temp1.Trim(), temp2.Trim()));
                }
                cout++;
            }
            Dictionary<string, Tuple<int, int>> listInOut = new Dictionary<string, Tuple<int, int>>();
            cout = 0;
            foreach (var tempModule in listModules)
            {
                var indexIn = -1;
                var indexOut = -1;
                cout = 0;
                foreach (var ity in my_ColumnName)
                {

                    if (ity.IndexOf(tempModule.ToString()) > -1 && ity.IndexOf("__in") > -1)
                    {
                        indexIn = cout;
                    }
                    if (ity.IndexOf(tempModule.ToString()) > -1 && ity.IndexOf("__out") > -1)
                    {
                        indexOut = cout;
                    }
                    cout++;
                }
                if (indexIn != -1 || indexOut != -1)
                {
                    listInOut.Add(tempModule.ToString(), Tuple.Create(indexIn, indexOut));
                }
                cout++;
            }
            List<Dictionary<int, Tuple<string, string>>> listModulesList = new List<Dictionary<int, Tuple<string, string>>>();
            var tempNewList = new Dictionary<int, Tuple<string, string>>();
            var first = tempList.ElementAt(0);

            var elem = first.Value;
            var index = first.Key;
            tempList.Remove(first.Key);
            tempNewList.Add(first.Key, first.Value);
            bool isBack = false;

            List<int> backIndex = new List<int>();
            while (tempList.Count > 0)
            {


                first = GetNext(tempList, elem, ref isBack, tempNewList);


                if (first.Equals(default(KeyValuePair<int, Tuple<string, string>>)))
                {
                    listModulesList.Add(tempNewList);
                    tempNewList = new Dictionary<int, Tuple<string, string>>();
                    first = tempList.ElementAt(0);

                    elem = first.Value;
                    index = first.Key;
                }
                else
                {
                    tempNewList.Add(first.Key, first.Value);
                    elem = first.Value;
                    index = first.Key;
                    tempList.Remove(first.Key);
                }


                if (tempList.Count == 0)
                {
                    listModulesList.Add(tempNewList);
                }
            }
            List<int> sequence = new List<int>();
            
            foreach(var ListModules in listModulesList)
            {
                foreach(var ity in ListModules)
                {
                    sequence.Add(ity.Key);
                }
            }
            



            return sequence;
        }


        private KeyValuePair<int, Tuple<string, string>> GetNext(Dictionary<int, Tuple<string, string>> tempList, Tuple<string, string> elem, ref bool isBack, Dictionary<int, Tuple<string, string>> tempNewList)
        {
            foreach (var ity in tempList)
            {
                if (isBack && ity.Value.Item1 == elem.Item2 && ity.Value.Item2 != elem.Item1 && NotInList(tempNewList, ity))
                {

                    isBack = false;
                    return ity;


                }

            }
            foreach (var ity in tempList)
            {
                if (!isBack && ity.Value.Item1 == elem.Item2 && ity.Value.Item2 != elem.Item1)
                {
                    isBack = false;
                    return ity;
                }

            }
            foreach (var ity in tempList)
            {

                if (isBack && ity.Value.Item1 == elem.Item1 && ity.Value.Item2 != elem.Item2)
                {
                    isBack = false;
                    return ity;
                }

            }

            foreach (var ity in tempList)
            {
                if (ity.Value.Item1 == elem.Item2)
                {
                    isBack = true;
                    return ity;

                }

            }
            isBack = false;
            return default(KeyValuePair<int, Tuple<string, string>>);
        }

        private bool NotInList(Dictionary<int, Tuple<string, string>> tempNewList, KeyValuePair<int, Tuple<string, string>> elem)
        {

            foreach (var ity in tempNewList)
            {
                if (ity.Value.Item1 == elem.Value.Item2 && ity.Value.Item2 == elem.Value.Item1)
                    return false;
            }
            return true;
        }

        private List<string> GetListModules(List<string> my_ColumnName)
        {

            List<string> listModules = new List<string>();

            foreach (string ity in my_ColumnName)
            {
                if (ity.IndexOf("->") <= 0)
                    continue;


                var temp = ity.Substring(0, ity.IndexOf("->"));
                bool IsInList = listModules.Any(temp.Contains);
                if (!IsInList)
                {
                    listModules.Add(temp);
                }
            }
            return listModules;
        }
    }
    }
