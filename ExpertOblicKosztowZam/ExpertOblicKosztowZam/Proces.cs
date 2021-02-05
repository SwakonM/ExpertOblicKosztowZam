using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpertOblicKosztowZam
{


    public class Proces 
    {
        internal static List<List<int>> GenerateSequenceIndex(List<string> my_ColumnName)
        {
       
            List<string> listModules = GetListModules(my_ColumnName);
           var orgList = new Dictionary<int, Tuple<string, string>>();
            var tempList = new Dictionary<int, Tuple<string, string>>();

            int cout = 0;
            foreach (var ity in my_ColumnName)
            {

                if (ity.IndexOf("->") > 0)
                {

                    var temp1 = ity.Substring(0, ity.IndexOf("->"));

                    var temp2 = ity.Substring(ity.IndexOf("->") + 2);
                    orgList.Add(cout, Tuple.Create(temp1.Trim(), temp2.Trim()));
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
            var listModulesList = new List<Dictionary<int, Tuple<string, string>>>();
            var tempNewList = new Dictionary<int, Tuple<string, string>>();
            var ModuleStac = new Stack<Tuple<int,string, string>>();
            var first = tempList.ElementAt(0);
            
            var oldElem = first;
            var lastElem = tempList.ElementAt(tempList.Count - 1);
            var elem = first.Value;
            var index = first.Key;
            tempList.Remove(first.Key);
            tempNewList.Add(first.Key, first.Value);
            bool isBack = false;
            bool isLastElem = false;
            List<int> backIndex = new List<int>();
            while (tempList.Count > 0)
            {
                
                if(isBack )
                {
                    first = GetBack(tempList, elem);
                  
                }
                else
                {
                    first = GetNext(tempList, elem, isLastElem, tempNewList);
                }

                if (elem.Item1 == lastElem.Value.Item1 && elem.Item2 == lastElem.Value.Item2|| elem.Item1 == lastElem.Value.Item2 && elem.Item2 == lastElem.Value.Item1)
                {
                    isLastElem = true;
                }



                if (first.Equals(default(KeyValuePair<int, Tuple<string, string>>)))
                {
                    
                     if (ModuleStac.Count > 0)
                    {
                        while (ModuleStac.Count > 0)
                        {
                            var ity = ModuleStac.Pop();
                            tempList.Remove(ity.Item1);
                        }
                    }
                    var ityIndex = FindIndex(orgList, elem);
                    if (ityIndex >= 0)
                    {
                        ModuleStac.Push(Tuple.Create(ityIndex, elem.Item1, elem.Item2));
                        tempNewList.Remove(ityIndex);
                    }
                      
                    elem = Tuple.Create(elem.Item2, elem.Item1);
                   

                    isBack = true;
                }
                else if(isBack )
                {
                   
                    ModuleStac.Push(Tuple.Create(first.Key, first.Value.Item1, first.Value.Item2));
                    if (ModuleStac.Count > 1)
                    {
                        var templist = new Dictionary<int, Tuple<string, string>>();
                        foreach (var ity in ModuleStac.OrderBy(i => i.Item1))
                        {
                            if (!isLastElem)
                            {
                                templist.Add(ity.Item1, Tuple.Create(ity.Item2, ity.Item3));
                            }
                            else
                                tempNewList[ity.Item1] = Tuple.Create(ity.Item2, ity.Item3);
                        }
                        ModuleStac.Clear();
                        if (!isLastElem)
                            listModulesList.Add(templist);
                       

                    }
                    tempList.Remove(first.Key);
                    elem = first.Value;
                  
                    isBack = false;
                }
                else
                {
                    if(ModuleStac.Count>1)
                    {
                        var templist = new Dictionary<int, Tuple<string, string>>();
                        while(ModuleStac.Count > 0)
                        {
                             var ity =  ModuleStac.Pop();
                            
                            templist.Add(ity.Item1,  Tuple.Create(ity.Item2,ity.Item3));
                           

                        }
                        listModulesList.Add(templist);
                    }
                    else if(ModuleStac.Count >0)
                    {
                        while (ModuleStac.Count > 0)
                        {
                            var ity = ModuleStac.Pop();
                            tempList.Remove(ity.Item1);
                        }
                    }
                    tempNewList.Add(first.Key, first.Value);
                    elem = first.Value;
                    ModuleStac.Push(Tuple.Create(first.Key, elem.Item1, elem.Item2));


                }


                if (tempList.Count == 0)
                {
                    listModulesList.Add(tempNewList);
                }
            }
          
            List< List<int>> sequence = new List<List<int>>();


            foreach (var ListModules in listModulesList)
            {
                var temp = new List<int>();
                foreach (var ity in ListModules)
                {
                    temp.Add(ity.Key);
                }
                sequence.Add(temp);
            }
        
            


            return sequence.OrderByDescending(x => x.Count).ToList(); 
        }

        private static int FindIndex(Dictionary<int, Tuple<string, string>> tempList, Tuple<string, string> elem)
        {
            foreach (var ity in tempList)
            {
                if (ity.Value.Item1 == elem.Item1 && ity.Value.Item2 == elem.Item2)
                    return ity.Key;
            }

            return -1;
        }

        private static KeyValuePair<int, Tuple<string, string>> GetNext(Dictionary<int, Tuple<string, string>> tempList, Tuple<string, string> elem, bool isLastElem,  Dictionary<int, Tuple<string, string>> tempNewList)
        {
            foreach (var ity in tempList)
            {
                if(isLastElem && ity.Value.Item1 == elem.Item2 && ity.Value.Item2 != elem.Item1)
                {
                    return ity;
                }
                else if ( ity.Value.Item1 == elem.Item2 && ity.Value.Item2 != elem.Item1 && NotInList(tempNewList, ity))
                {

                    return ity;


                }

            }
           

            return default(KeyValuePair<int, Tuple<string, string>>);
        }

        private static bool NotInList(Dictionary<int, Tuple<string, string>> tempNewList, KeyValuePair<int, Tuple<string, string>> elem)
        {

            foreach (var ity in tempNewList)
            {
                if (ity.Value.Item1 == elem.Value.Item2 && ity.Value.Item2 == elem.Value.Item1)
                    return false;
            }
            return true;
        }
        private static List<string> GetListModules(List<string> my_ColumnName)
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

        private static KeyValuePair<int, Tuple<string, string>> GetBack(Dictionary<int, Tuple<string, string>> tempList, Tuple<string, string> elem)
        {
            foreach (var ity in tempList)
            {
                if (ity.Value.Item1 == elem.Item1 && ity.Value.Item2 == elem.Item2 )
                {

                    return ity;


                }

            }

            return default(KeyValuePair<int, Tuple<string, string>>);
        }

    }
}