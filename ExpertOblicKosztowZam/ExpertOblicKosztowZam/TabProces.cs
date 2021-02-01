using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ExpertOblicKosztowZam
{
    class TabProces : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public TabProces(string moduleA, string moduleB, int indexA, int indexB, int indexIn, int indexOut, bool isSubmodule)
        {
            ModuleA = moduleA;
            ModuleB = moduleB;
            IndexA = indexA;
            IndexB = indexB;
            IndexIn = indexIn;
            IndexOut = indexOut;
            ISubmodule = isSubmodule;
        }

        public TabProces()
            : this("", "", -1, -1, -1, -1, false)
        { }

        private string _moduleA; 
        private string _moduleB;
        private int _indexA; 
        private int _IndexB;
       
        private int _indexIn;
        public int IndexIn
        {
            get { return _indexIn; }
            set { _indexIn = value; }
        }
        private int _indexOut;
        public int IndexOut
        {
            get { return _indexOut; }
            set { _indexOut = value; }
        }
        private bool _iSubmodule;
        public bool ISubmodule
        {
            get { return _iSubmodule; }
            set { _iSubmodule = value; }
        }
     

        private int _outId;
        public int OutId
        {
            get { return _outId; }
            set { _outId = value; }
        }
        public string ModuleA
        {
            get { return _moduleA; }
            set { _moduleA = value; }
        }
   
        public int IndexA
        {
            get { return _indexA; }
            set { _indexA = value; }
        }
     
        public int IndexB
        {
            get { return _IndexB; }
            set { _IndexB = value; }
        }
        public string ModuleB
        {
            get { return _moduleB; }
            set { _moduleB = value; }
        }
        

    }
}
