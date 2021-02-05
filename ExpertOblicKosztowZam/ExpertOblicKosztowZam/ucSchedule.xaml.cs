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
       List< List<int>> sequenceList =new List<List<int>>();
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
            sequenceList= Proces.GenerateSequenceIndex(my_ColumnName);
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
           int maxElem = GetMaxElem(sequenceList);
            for (var i = 0; i < maxElem + 2; i++)
            {
                var gridCol1 = new ColumnDefinition() { Width = new GridLength(55) };
                var gridCol2 = new ColumnDefinition() { Width = new GridLength(55) };
                
                DynamicGrid.ColumnDefinitions.Add(gridCol1);
                DynamicGrid.ColumnDefinitions.Add(gridCol2);
               
            }
            // Create Rows
            var gridRow = new RowDefinition() { Height = new GridLength(120) };
            DynamicGrid.RowDefinitions.Add(gridRow);

            for (var i = 0; i < sequenceList.Count()*7+ maxElem; i++)
            {
                var gridRow1 = new RowDefinition() {Height = new GridLength(35) };
                DynamicGrid.RowDefinitions.Add(gridRow1);
            }
            var gridRow2 = new RowDefinition() { Height = new GridLength(120) };
            DynamicGrid.RowDefinitions.Add(gridRow2);
            int count = 0;
            int rowIndex = 0;
            int multiple = 0;
            foreach (var ity in sequenceList)
            {
                count = 0;
                if (multiple > 1)
                    multiple = rowIndex * 5;
                else
                    multiple = rowIndex * 6;
                int firstIndex = FindFirstIndex(my_ColumnName,sequenceList, ity);
                foreach (var index in ity)
                {
                    var unitSchedule = new UnitScheduleComponent();
                    unitSchedule.UnitProdText = my_ColumnName[index].Replace("__in", "<--o").Replace("__out", "-->o");
                    Grid.SetColumnSpan(unitSchedule, 3);
                    Grid.SetRow(unitSchedule, multiple + count + 1);
                    Grid.SetColumn(unitSchedule,( count * 2) +(firstIndex * 2));
                    Grid.SetRowSpan(unitSchedule, 2);
                    DynamicGrid.Children.Add(unitSchedule);

                    count++;
                }
                rowIndex++;
            }

          //  ScottPlot.WpfPlot wpfPlot1 = new ScottPlot.WpfPlot();
          //  var plt = new ScottPlot.Plot(600, 400);

          //  ScottPlot.OHLC[] ohlcs = ScottPlot.DataGen.RandomStockPrices(rand: null, pointCount: 60, deltaMinutes: 10);
          //  wpfPlot1.plt.Title("Ten Minute Candlestick Chart");
          //  wpfPlot1.plt.YLabel("Stock Price (USD)");
          //  wpfPlot1.plt.PlotCandlestick(ohlcs);
          ////  wpfPlot1.plt.Ticks(dateTimeX: true);

           
          //  wpfPlot1.Render();
          //  Grid.SetRow(wpfPlot1, count + 3);
          //  Grid.SetColumnSpan(wpfPlot1, sequenceList.Count());
          //  DynamicGrid.Children.Add(wpfPlot1);
            DynamicScrollViewer.Content = DynamicGrid;
    
            // Display grid into a Window

            this.Content = DynamicScrollViewer;

        }

        private int FindFirstIndex(List<string> my_ColumnName, List<List<int>> sequenceList, List<int> elem)
        {
            
            var first = elem.ElementAt(0);
            var count = 0;
            foreach(var ity in sequenceList.ElementAt(0))
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

        private int GetMaxElem(List<List<int>> sequenceList )
        {
            int max = 0;
           foreach(var ity in sequenceList)
           {
                if (max < ity.Count)
                    max = ity.Count;
           }

            return max;
        }
    }
    }
