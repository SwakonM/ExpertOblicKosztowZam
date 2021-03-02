using Microsoft.Win32;

using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for ucSetting.xaml
    /// </summary>

    public partial class ucSetting : UserControl,ITabbedMDI
    {

        public List<TabZamowinia> my_ListZamowin;
        
        List<string> my_ColumnName;
        private List<Zamowinia> my_zamowinias = new List<Zamowinia>();
        private bool isInit = false;
    

        public ucSetting(ref List<TabZamowinia> ListZamowin,ref List<string>  ColumnName)
        {
            InitializeComponent();
            my_ListZamowin = ListZamowin;
            my_ColumnName = ColumnName;
            isInit = true;
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
                return "ucSetting";
            }
        }

        /// <summary>
        /// This is the title that will be shown in the tab.
        /// </summary>
        public string Title
        {
            get { return "Zamówienia"; }
        }
        #endregion
        private static DataTemplate getDataTemplate(int count)
        {
            DataTemplate template = new DataTemplate();
            FrameworkElementFactory factory = new FrameworkElementFactory(typeof(TextBlock));
            factory.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Right);
            factory.SetBinding(TextBlock.TextProperty, new Binding(string.Format("[{0}]", count)));
            template.VisualTree = factory;

            return template;
        }
        private void MouseDownOnCell(object sender, MouseButtonEventArgs e)
        {

            var I = sender;
        }

        private void InitGrid()
        {
            var coll01 = new DataGridTextColumn();

            coll01.Binding = new Binding("IdZam");
            coll01.Header = my_ColumnName[0].Replace('@', ',');

            DatagridMovie.Columns.Add(coll01);

            var coll02 = new DataGridTextColumn();
            coll02.Binding = new Binding("LiczZam");
            coll02.Header = my_ColumnName[1];
            DatagridMovie.Columns.Add(coll02);

            var coll03 = new DataGridTextColumn();
            coll03.Binding = new Binding("Wspolczynik");
            coll03.Header = "γ_m,n";
            DatagridMovie.Columns.Add(coll03);
            int index = 1;
            foreach (var collName in my_ColumnName)
            {
                if (index > 2)
                {
                    var coll = new DataGridTextColumn();

                    coll.Header = collName;
                    coll.Binding = new Binding("Coll" + (index - 2));


                    DatagridMovie.Columns.Add(coll);

                }
                index++;
            }

            var coll04 = new DataGridTextColumn();
            coll04.Header = "C_m,n";
            coll04.Binding = new Binding("KosztLogicznyObslugi");
            DatagridMovie.Columns.Add(coll04);
            DatagridMovie.AddHandler(DataGrid.MouseLeftButtonDownEvent, new MouseButtonEventHandler(MouseDownOnCell), true);
        }

        private void FillGrid(bool isUpdate)
        {
            if (!isInit)
                return;
            
            my_zamowinias.Clear();

            if (isUpdate)            
                InitGrid();



            

            foreach (var zam in my_ListZamowin)
            {
                var temp = new Zamowinia(zam.IdZamowinia.Replace('@', ','), zam.LiczbaJedostekZam, Double.Parse(this.BazowyWspoczIlos.Text), Double.Parse(DenominatorMin.Text), Double.Parse(MaxWartosc.Text));
                temp.Coll1 = zam._Param.ElementAt(0).Value;
                temp.Coll2 = zam._Param.ElementAt(1).Value;
                temp.Coll3 = zam._Param.ElementAt(2).Value;
                temp.Coll4 = zam._Param.ElementAt(3).Value;
                temp.Coll5 = zam._Param.ElementAt(4).Value;
                temp.Coll6 = zam._Param.ElementAt(5).Value;
                temp.Coll7 = zam._Param.ElementAt(6).Value;
                temp.Coll8 = zam._Param.ElementAt(7).Value;
                temp.Coll9 = zam._Param.ElementAt(8).Value;
                temp.Coll10 = zam._Param.ElementAt(9).Value;
                temp.Coll11 = zam._Param.ElementAt(10).Value;
                temp.Coll12 = zam._Param.ElementAt(11).Value;
                temp.Coll13 = zam._Param.ElementAt(12).Value;
                temp.Coll14 = zam._Param.ElementAt(13).Value;
                temp.Coll15 = zam._Param.ElementAt(14).Value;
                temp.Coll16 = zam._Param.ElementAt(15).Value;
                temp.Coll17 = zam._Param.ElementAt(16).Value;
                temp.Coll18 = zam._Param.ElementAt(17).Value;
                temp.Coll19 = zam._Param.ElementAt(18).Value;
                temp.Coll20 = zam._Param.ElementAt(19).Value;
                temp.Coll21 = zam._Param.ElementAt(20).Value;
                temp.Coll22 = zam._Param.ElementAt(21).Value;
                temp.Coll23 = zam._Param.ElementAt(22).Value;
                temp.Coll24 = zam._Param.ElementAt(23).Value;
                temp.Coll25 = zam._Param.ElementAt(24).Value;
                temp.Coll26 = zam._Param.ElementAt(25).Value;
                temp.Coll27 = zam._Param.ElementAt(26).Value;
                temp.Coll28 = zam._Param.ElementAt(27).Value;
                temp.Coll29 = zam._Param.ElementAt(28).Value;
                temp.Coll30 = zam._Param.ElementAt(29).Value;
                temp.Coll31 = zam._Param.ElementAt(30).Value;
                temp.Coll32 = zam._Param.ElementAt(31).Value;


                my_zamowinias.Add(temp);
            
       
                
            }


           
         
          
        
           

            //  var column1 = new DataColumn("IdZam", typeof(string));
            //    column1.ColumnName = my_ColumnName[0].Replace('@', ',');
            //   var column2 = new DataColumn("IdZam", typeof(string));


           

           // CollectionViewSource itemCollectionViewSource;
          //  itemCollectionViewSource = (CollectionViewSource)(FindResource("ItemCollectionViewSource"));
           
            DatagridMovie.ItemsSource = null;
            DatagridMovie.ItemsSource = my_zamowinias;
            DatagridMovie.UpdateLayout();
            DatagridMovie.Items.Refresh();
            //itemCollectionViewSource.Source = my_zamowinias;
         


        }
        private void Row_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Ensure row was clicked and not empty space
            var row = ItemsControl.ContainerFromElement((DataGrid)sender,
                                                e.OriginalSource as DependencyObject) as DataGridRow;

            if (row == null) return;

    
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FillGrid(true);
            //if (my_zamowinias.Count() > 0)
            //{
            //    List<double> ldataX = new List<double>();
            //    List<double> ldataY = new List<double>();
            //    int cout = 1;
            //    foreach (var ity in my_zamowinias)
            //    {
            //        ldataX.Add(ity.LiczZam);
            //        ldataY.Add(ity.KosztLogicznyObslugi);
            //    }
            //    // plot the data
            //    wpfPlot1.Reset();
            //   // wpfPlot1.plt.PlotSignal(ldataY.ToArray());
            //    wpfPlot1.plt.PlotSignal(ldataX.ToArray());

            //    // additional styling
            //    wpfPlot1.plt.Title($"Line Plot ({my_zamowinias.Count():N0} points each)");
            //    wpfPlot1.plt.XLabel("Horizontal Axis Label");
            //    wpfPlot1.plt.YLabel("Vertical Axis Label");
            //    wpfPlot1.Render();
            //    wpfPlot1.Render();
            //}
            //lvUsers.ItemsSource = my_ListZamowin;

        }

        private void BazowyWspoczIlos_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
           if(textBox.Text.Length>0)
            FillGrid(false);
        }
        
        private void DenominatorMin_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox.Text.Length > 0)
                FillGrid(false);
        }
    }
    
}
