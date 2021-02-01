using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace ExpertOblicKosztowZam
{

    public struct ModuleInfo
    {
        public string Module1;
        public string Module2;
        public int Index1;
        public int Index2;
        public bool IsSubModule;

      
    }

    /// <summary>
    /// Interaction logic for ucSequenceModel.xaml
    /// </summary>
    public partial class ucSequenceModel : UserControl, ITabbedMDI
    {
        private List<TabProces> my_ListProses = new List<TabProces>();
        public List<TabZamowinia> my_ListZamowin;
        List<string> my_ColumnName;
        private bool isInit = false;

        public List<TabMod> my_list = new List<TabMod>();
        public ucSequenceModel(ref List<TabZamowinia> ListZamowin, ref List<string> ColumnName)
        {
            InitializeComponent();
            my_ListZamowin = ListZamowin;
            my_ColumnName = ColumnName;

            my_ListProses = GetListProces(my_ColumnName);
        }

        private List<TabProces> GetListProces(List<string> my_ColumnName)
        {
            List<TabProces> ListProses = new List<TabProces>();

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
            foreach (var ity in listModules)
            {
                var indexIn = -1;
                var indexOut = -1;
                cout = 0;
                foreach (var ityElem in my_ColumnName)
                {

                    if (ityElem.IndexOf(ity.ToString()) > -1 && ityElem.IndexOf("__in") > -1)
                    {
                        indexIn = cout;
                    }
                    if (ityElem.IndexOf(ity.ToString()) > -1 && ityElem.IndexOf("__out") > -1)
                    {
                        indexOut = cout;
                    }
                    cout++;
                }
                if (indexIn != -1 || indexOut != -1)
                {
                    listInOut.Add(ity.ToString(), Tuple.Create(indexIn, indexOut));
                }

            }

            var dicModuleList = tempList;


            var ModuleStac = new Stack<Tuple<string, string>>();
            var listModuleInfo = new List<ModuleInfo>();
            var index1 = -1;
            var index2 = -1;
            var isSubmod = false;

            var module = dicModuleList.ElementAt(0);
            var moduleEnd = dicModuleList.ElementAt(dicModuleList.Count - 1);


            GetModuleInfo(tempList, module.Value, ref index1, ref index2);
            listModuleInfo.Add(new ModuleInfo() { Module1 = module.Value.Item1, Module2 = module.Value.Item2, Index1 = index1, Index2 = index2, IsSubModule = isSubmod });
            ModuleStac.Push(Tuple.Create(module.Value.Item1, module.Value.Item2));
            RemoveBegin(ref dicModuleList, module);
            var indexKey = 0;
            var intFix = 0;
            var isNotEndElem = true;
            while (ModuleStac.Count() > 0 && intFix < dicModuleList.Count * dicModuleList.Count && isNotEndElem)
            {

                module = GetNextModule(dicModuleList, ModuleStac.Peek(), listModuleInfo);
                if (module.Equals(default(KeyValuePair<int, Tuple<string, string>>)))
                {
                    var tempMod = ModuleStac.Pop();
                    RemoveNext(ref dicModuleList, tempMod);

                    indexKey = GetModuleIndex(listModuleInfo, tempMod);
                    if (indexKey > 0)
                    {
                        var temp = listModuleInfo[indexKey];
                        temp.IsSubModule = true;
                        listModuleInfo[indexKey] = temp;
                    }




                }
                else
                {

                    GetModuleInfo(tempList, module.Value, ref index1, ref index2);
                    listModuleInfo.Add(new ModuleInfo() { Module1 = module.Value.Item1, Module2 = module.Value.Item2, Index1 = index1, Index2 = index2, IsSubModule = isSubmod });
                    ModuleStac.Push(Tuple.Create(module.Value.Item1, module.Value.Item2));
                    RemoveBegin(ref dicModuleList, module);
                    if (module.Value.Item1 == moduleEnd.Value.Item1 && module.Value.Item2 == moduleEnd.Value.Item2 || module.Value.Item1 == moduleEnd.Value.Item2 && module.Value.Item2 == moduleEnd.Value.Item1)
                    {
                        isNotEndElem = false;
                    }
                }
                intFix++;

            }

            foreach (var ity in listModuleInfo)
            {
                var indexIn = listInOut[ity.Module2].Item1;
                var indexOut = listInOut[ity.Module2].Item2;

                ListProses.Add(new TabProces(ity.Module1, ity.Module2, ity.Index1, ity.Index2, indexIn, indexOut, ity.IsSubModule));

            }


            return ListProses;
        }


        private int GetModuleIndex(List<ModuleInfo> listModuleInfo, Tuple<string, string> moduleInfo)
        {

            var cout = 0;
            foreach (var ity in listModuleInfo)
            {
                if (ity.Module1 == moduleInfo.Item1 && ity.Module2 == moduleInfo.Item2)
                {


                    return cout;


                }
                cout++;
            }
            return -1;
        }

        private KeyValuePair<int, Tuple<string, string>> GetNextModule(Dictionary<int, Tuple<string, string>> dicModuleList, ModuleInfo moduleInfo, List<ModuleInfo> listModuleInfo)
        {
            foreach (var ity in dicModuleList)
            {
                if (ity.Value.Item1 == moduleInfo.Module2 && ity.Value.Item2 != moduleInfo.Module1 && NotInListModule(listModuleInfo, ity))
                {


                    return ity;


                }

            }


            return default(KeyValuePair<int, Tuple<string, string>>);
        }
        private KeyValuePair<int, Tuple<string, string>> GetNextModule(Dictionary<int, Tuple<string, string>> dicModuleList, Tuple<string, string> tuple, List<ModuleInfo> listModuleInfo)
        {
            foreach (var ity in dicModuleList)
            {
                if (ity.Value.Item1 == tuple.Item2 && ity.Value.Item2 != tuple.Item1 && NotInListModule(listModuleInfo, ity))
                {


                    return ity;


                }

            }


            return default(KeyValuePair<int, Tuple<string, string>>);
        }

        private void RemoveNext(ref Dictionary<int, Tuple<string, string>> dicModuleList, Tuple<string, string> tuple)
        {
            foreach (var ity in dicModuleList)
            {
                if (ity.Value.Item1 == tuple.Item1 && ity.Value.Item2 == tuple.Item2)
                {

                    dicModuleList.Remove(ity.Key);
                    return;

                }
            }
        }

        private void RemoveNext(ref Dictionary<int, Tuple<string, string>> dicModuleList, ModuleInfo moduleInfo)
        {
            foreach (var ity in dicModuleList)
            {
                if (ity.Value.Item1 == moduleInfo.Module1 && ity.Value.Item2 == moduleInfo.Module2)
                {

                    dicModuleList.Remove(ity.Key);
                    return;

                }
            }
        }
        private void RemoveBegin(ref Dictionary<int, Tuple<string, string>> dicModuleList, KeyValuePair<int, Tuple<string, string>> tempModule)
        {
            foreach (var ity in dicModuleList)
            {
                if (ity.Value.Item1 == tempModule.Value.Item2 && ity.Value.Item2 == tempModule.Value.Item1)
                {

                    dicModuleList.Remove(ity.Key);
                    return;

                }
            }
        }



        private bool NotInListModule(List<ModuleInfo> listModuleInfo, KeyValuePair<int, Tuple<string, string>> elem)
        {
            foreach (var ity in listModuleInfo)
            {
                if (ity.Module1 == elem.Value.Item2 && ity.Module2 == elem.Value.Item1 || ity.Module2 == elem.Value.Item1 && ity.Module1 == elem.Value.Item2)
                    return false;
            }
            return true;
        }

        private bool NotExistModule(Dictionary<int, Tuple<string, string>> dicModuleList, KeyValuePair<int, Tuple<string, string>> elem)
        {
            foreach (var ity in dicModuleList)
            {
                if (ity.Value.Item1 == elem.Value.Item1 && ity.Value.Item2 == elem.Value.Item2 || ity.Value.Item2 == elem.Value.Item1 && ity.Value.Item1 == elem.Value.Item2)
                    return false;
            }
            return true;
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

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            CreateDynamicWPFGrid();


        }
        private void CreateDynamicWPFGrid()
        {

            // Create the Grid
            var DynamicScrollViewer = new ScrollViewer();
            DynamicScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            DynamicScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            var DynamicGrid = new Grid { ShowGridLines = false };

            //  DynamicGrid.Width = 800;
            // DynamicGrid.Height= 450;
            DynamicGrid.HorizontalAlignment = HorizontalAlignment.Left;

            DynamicGrid.VerticalAlignment = VerticalAlignment.Top;

            //    DynamicGrid.ShowGridLines = true;

            DynamicGrid.Background = new SolidColorBrush(Colors.LightSteelBlue);
            // Create Columns
            for (var i = 0; i < 15; i++)
            {
                var gridCol1 = new ColumnDefinition() { Width = new GridLength(120) };

                DynamicGrid.ColumnDefinitions.Add(gridCol1);
            }
            // Create Rows

            for (var i = 0; i < 10; i++)
            {
                var gridRow1 = new RowDefinition();


                DynamicGrid.RowDefinitions.Add(gridRow1);
            }



            var temp1 = my_ListProses[0];
            int count = 0;
            int countSubmod = 0;
            for (int iTy = -1; iTy < my_ListProses.Count(); iTy++)
            {
               
                if (iTy>=0)
                 temp1 = my_ListProses[iTy];
                int id = count;
                if (temp1.ISubmodule)
                {

                    var connect1 = new UnitProductionConnection();
                    connect1.Name1 = temp1.ModuleA + " -> " + temp1.ModuleB;
                    connect1.Name2 = temp1.ModuleB + " <- " + temp1.ModuleA;
                    if (countSubmod > 0) 
                    {
                        Grid.SetRow(connect1, 3);
                    }
                    else
                    {
                        Grid.SetRow(connect1, 5);
                    }

                    Grid.SetColumn(connect1, (id-1) * 2);
                    DynamicGrid.Children.Add(connect1);

                    var UnitProd1 = new UnitProduction();
                    UnitProd1.Name = temp1.ModuleB;
                    UnitProd1.UserControlClicked += UnitProd1_UserControlClicked;
                    Grid.SetColumn(UnitProd1, (id - 1) * 2);
                    if (countSubmod>0)
                    {
                        Grid.SetRow(UnitProd1, 2);
                    }
                    else
                    {
                        Grid.SetRow(UnitProd1, 6);
                    }
                    DynamicGrid.Children.Add(UnitProd1);
                    countSubmod++;
                }
                else
                {
                    if (id > 0)
                    {
                        var connect1 = new UnitProductionConnection();
                        connect1.Name1 = temp1.ModuleA + " -> " + temp1.ModuleB;
                        connect1.Name2 = temp1.ModuleB + " <- " + temp1.ModuleA;
                        Grid.SetRow(connect1, 4);
                        Grid.SetColumn(connect1, id * 2 - 1);
                        DynamicGrid.Children.Add(connect1);
                    }
                    var UnitProd1 = new UnitProduction();
                    UnitProd1.UserControlClicked += UnitProd1_UserControlClicked;
                    if (id > 0)
                    {
                        UnitProd1.Name = temp1.ModuleB;
                    }
                    else
                    {
                        UnitProd1.Name = temp1.ModuleA;
                    }
                    Grid.SetColumn(UnitProd1, id * 2);
                    Grid.SetRow(UnitProd1, 4);
                    DynamicGrid.Children.Add(UnitProd1);
                  


                    count++;

                }




            }



            DynamicScrollViewer.Content = DynamicGrid;
            // Display grid into a Window

            this.Content = DynamicScrollViewer;
        }

        private void UnitProd1_UserControlClicked(object sender, EventArgs e)
        {
            UnitProduction sender1 = (UnitProduction)sender;
            var Name = sender1.UnitProdBttText;
         TabProces proc   = FindProces( Name);

        }

        private TabProces FindProces( string name)
        {
           foreach(var ity in my_ListProses)
            {
                if (ity.ModuleA == name)
                    return ity;
            }
            return new TabProces();
        }

        private void GetModuleInfo(Dictionary<int, Tuple<string, string>> tempList, Tuple<string, string> elem,ref int index1,ref int index2)
        {
           foreach(var ity in tempList)
           {
                if (ity.Value.Item1 == elem.Item1 && ity.Value.Item2 == elem.Item2)
                {
                    index1 = ity.Key;
                }                    
                if (ity.Value.Item2 == elem.Item1 && ity.Value.Item1 == elem.Item2)
                {
                    index2 = ity.Key;
                }
                   
           }
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
                return "ucSequenceModel";
            }
        }

        /// <summary>
        /// This is the title that will be shown in the tab.
        /// </summary>
        public string Title
        {
            get { return "Struktura procesu "; }
        }
        #endregion


    }
}
