using Microsoft.Win32;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
  
    public partial class MainWindow 
    {
        public List<TabZamowinia> my_ListZamowin = new List<TabZamowinia>();
        public List<string> ColumnName = new List<string>();
        private Dictionary<string, string> _mdiChildren = new Dictionary<string, string>();
        public Boolean IsInit = false;

        public MainWindow()
        {
            InitializeComponent();
        }


    
      
        /// <summary>
        /// Add tab item to the tab
        /// </summary>
        /// <param name="mdiChild">This is the user control</param>
        private void AddTab(ITabbedMDI mdiChild)
        {
            //Check if the user control is already opened
            if (_mdiChildren.ContainsKey(mdiChild.UniqueTabName))
            {
                //user control is already opened in tab. 
                //So set focus to the tab item where the control hosted
                foreach (object item in tcMdi.Items)
                {
                    TabItem ti = (TabItem)item;
                    if (ti.Name == mdiChild.UniqueTabName)
                    {
                        ti.Focus();
                        break;
                    }
                }
            }
            else
            {
                //the control is not open in the tab item
                tcMdi.Visibility = Visibility.Visible;
                tcMdi.Width = this.ActualWidth;
                tcMdi.Height = this.ActualHeight;

                ((ITabbedMDI)mdiChild).CloseInitiated += new delClosed(CloseTab);

                //create a new tab item
                TabItem ti = new TabItem();
                //set the tab item's name to mdi child's unique name
                ti.Name = ((ITabbedMDI)mdiChild).UniqueTabName;
                //set the tab item's title to mdi child's title
                ti.Header = ((ITabbedMDI)mdiChild).Title;
                //set the content property of the tab item to mdi child
                ti.Content = mdiChild;
                ti.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                ti.VerticalContentAlignment = VerticalAlignment.Top;
                //add the tab item to tab control
                tcMdi.Items.Add(ti);
                //set this tab as selected
                tcMdi.SelectedItem = ti;
                //add the mdi child's unique name in the open children's name list
                _mdiChildren.Add(((ITabbedMDI)mdiChild).UniqueTabName, ((ITabbedMDI)mdiChild).Title);

            }
        }
        /// <summary>
        /// Close a tab item
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="e"></param>
        private void CloseTab(ITabbedMDI tab, EventArgs e)
        {
            TabItem ti = null;
            foreach (TabItem item in tcMdi.Items)
            {
                if (tab.UniqueTabName == ((ITabbedMDI)item.Content).UniqueTabName)
                {
                    ti = item;
                    break;
                }
            }
            if (ti != null)
            {
                _mdiChildren.Remove(((ITabbedMDI)ti.Content).UniqueTabName);
                tcMdi.Items.Remove(ti);
            }
        }
        /// <summary>
        /// Adjust the tab height and weight during load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu1_Loaded(object sender, RoutedEventArgs e)
        {
            tcMdi.Width = this.ActualWidth;
            tcMdi.Height = this.ActualHeight - 10;
        }
        private void RibbonWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void tcMdi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void InitDate()
        {
            
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV (*.csv)| *.csv";
            if (openFileDialog.ShowDialog() == true)
            {

                IsInit = false;

                my_ListZamowin.Clear();
                ColumnName.Clear();
                string path = System.IO.Path.GetFullPath(openFileDialog.FileName);

                string[] lines = System.IO.File.ReadAllLines(path);
                int count = 0;
                
                int clollNum = 0;
                int collId = 0;
                foreach (string line in lines)
                {
                    clollNum = 0;
                    string[] columns = line.Split(',');
                    collId = 0;





                    if (count == 0)
                    {
                        foreach (string collumn in columns)
                        {

                            ColumnName.Add(collumn);


                            clollNum++;
                        }
                    }
                    else
                    {
                        var _zamow = new TabZamowinia(columns[0], Int32.Parse(columns[1]));

                        foreach (string collumn in columns)
                        {
                            if (clollNum > 1)
                            {
                                _zamow._Param.Add(ColumnName[collId + 2], Double.Parse(collumn));
                                collId++;
                            }


                            clollNum++;
                        }
                        if (count > 0)
                        {
                            my_ListZamowin.Add(_zamow);
                        }

                    }





                    count++;

                }
                IsInit = true;
            }
           
        }

        private void btRead_Click(object sender, RoutedEventArgs e)
        {
            InitDate();

        }

        private void btCalculate_Click(object sender, RoutedEventArgs e)
        {

            if (IsInit)
            {
                ucSequenceModel mdiChild = new ucSequenceModel(ref my_ListZamowin, ref ColumnName);
                AddTab(mdiChild);
            }
            else
            {
                InitDate();
                if (IsInit)
                {
                    ucSequenceModel mdiChild = new ucSequenceModel(ref my_ListZamowin, ref ColumnName);
                    AddTab(mdiChild);

                }
            }

        }

        private void btExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btOpenGrid_Click(object sender, RoutedEventArgs e)
        {
            if(IsInit)
            {
                ucSetting mdiChild = new ucSetting(ref my_ListZamowin,ref ColumnName );
                AddTab(mdiChild);
            }
            else
            {
                InitDate();
                if (IsInit)
                {
                    ucSetting mdiChild = new ucSetting(ref my_ListZamowin, ref ColumnName);
                    AddTab(mdiChild);

                }
            }
             
        }

        private void btSchedule_Click(object sender, RoutedEventArgs e)
        {
            if (IsInit)
            {
                ucSchedule mdiChild = new ucSchedule(ref my_ListZamowin, ref ColumnName);
                AddTab(mdiChild);
            }
            else
            {
                InitDate();
            }

        }
    }
}
