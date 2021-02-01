using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ExpertOblicKosztowZam
{
    public class Zamowinia : INotifyPropertyChanged
    { 
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Zamowinia(string _IdZam, int _LiczZam , Double _BazowyWspoczIlos, Double _DenominatorMin,Double _maxValue)
        { 
            IdZam = _IdZam;
            liczZam = _LiczZam;
            BazowyWspoczIlos = _BazowyWspoczIlos;
            DenominatorMin = _DenominatorMin;
            MaxValue = _maxValue;
        }
        public Zamowinia()
            : this("", -1, -1, -1, -1)
        { }

        private Double maxValue;
        public System.Double MaxValue
        {
            get { return maxValue; }
            set { maxValue = value; }
        }
        private Double bazowyWspoczIlos;
        public System.Double BazowyWspoczIlos
        {
            get { return bazowyWspoczIlos; }
            set { 
                bazowyWspoczIlos = value;
            }
        }

        private Double denominatorMin;
        public System.Double DenominatorMin
        {
            get { return denominatorMin; }
            set { denominatorMin = value;

                
                if (denominatorMin > 0 && LiczZam / denominatorMin > 0.1f)
                {
                    Wspolczynik = MaxValue;
                }
                else if (denominatorMin > 0)
                {
                    Wspolczynik = BazowyWspoczIlos + LiczZam / denominatorMin;
                }
            }
        }
        private string idZam;
        public string IdZam
        {
            get { return idZam; }
            set { idZam = value; }
        }
        private int liczZam;
        public int LiczZam
        {
            get { return liczZam; }
            set { liczZam = value; }
        }
        private Double coll1;
        public System.Double Coll1
        {
            get { return coll1; }
            set { coll1 = value; KosztLogicznyObslugi = CalculateTotalCost(); }
        }
        private Double coll2;
        public System.Double Coll2
        {
            get { return coll2; }
            set { coll2 = value; KosztLogicznyObslugi = CalculateTotalCost(); }
        }
        private Double coll3;
        public System.Double Coll3
        {
            get { return coll3; }
            set { coll3 = value; KosztLogicznyObslugi = CalculateTotalCost(); }
        }
        private Double coll4;
        public System.Double Coll4
        {
            get { return coll4; }
            set { coll4 = value; KosztLogicznyObslugi = CalculateTotalCost(); }
        }
        private Double coll5;
        public System.Double Coll5
        {
            get { return coll5; }
            set { coll5 = value; KosztLogicznyObslugi = CalculateTotalCost(); }
        }
        private Double coll6;
        public System.Double Coll6
        {
            get { return coll6; }
            set { coll6 = value; KosztLogicznyObslugi = CalculateTotalCost(); }
        }
        private Double coll7;
        public System.Double Coll7
        {
            get { return coll7; }
            set { coll7 = value; KosztLogicznyObslugi = CalculateTotalCost(); }
        }
        private Double coll8;
        public System.Double Coll8
        {
            get { return coll8; }
            set { coll8 = value; KosztLogicznyObslugi = CalculateTotalCost(); }
        }
        private Double coll9;
        public System.Double Coll9
        {
            get { return coll9; }
            set { coll9 = value; KosztLogicznyObslugi = CalculateTotalCost(); }
        }
        private Double coll10;
        public System.Double Coll10
        {
            get { return coll10; }
            set { coll10 = value; KosztLogicznyObslugi = CalculateTotalCost(); }
        }
        private Double coll11;
        public System.Double Coll11
        {
            get { return coll11; }
            set { coll11 = value; KosztLogicznyObslugi = CalculateTotalCost(); }
        }
        private Double coll12;
        public System.Double Coll12
        {
            get { return coll12; }
            set { coll12 = value; KosztLogicznyObslugi = CalculateTotalCost(); }
        }
        private Double coll13;
        public System.Double Coll13
        {
            get { return coll13; }
            set { coll13 = value; KosztLogicznyObslugi = CalculateTotalCost(); }
        }
        private Double coll14;
        public System.Double Coll14
        {
            get { return coll14; }
            set { coll14 = value; KosztLogicznyObslugi = CalculateTotalCost(); }
        }
        private Double coll15;
        public System.Double Coll15
        {
            get { return coll15; }
            set { coll15 = value; KosztLogicznyObslugi = CalculateTotalCost(); }
        }
        private Double coll16;
        public System.Double Coll16
        {
            get { return coll16; }
            set { coll16 = value; KosztLogicznyObslugi = CalculateTotalCost(); }
        }
        private Double coll17;
        public System.Double Coll17
        {
            get { return coll17; }
            set { coll17 = value; KosztLogicznyObslugi = CalculateTotalCost(); }
        }
        private Double coll18;
        public System.Double Coll18
        {
            get { return coll18; }
            set { coll18 = value; KosztLogicznyObslugi = CalculateTotalCost(); }
        }
        private Double coll19;
        public System.Double Coll19
        {
            get { return coll19; }
            set { coll19 = value; KosztLogicznyObslugi = CalculateTotalCost(); }
        }
        private Double coll20;
        public System.Double Coll20
        {
            get { return coll20; }
            set { coll20 = value; KosztLogicznyObslugi = CalculateTotalCost(); }
        }
        private Double coll21;
        public System.Double Coll21
        {
            get { return coll21; }
            set { coll21 = value; KosztLogicznyObslugi = CalculateTotalCost(); }
        }
        private Double coll22;
        public System.Double Coll22
        {
            get { return coll22; }
            set { coll22 = value; KosztLogicznyObslugi = CalculateTotalCost(); }
        }
        private Double coll23;
        public System.Double Coll23
        {
            get { return coll23; }
            set { coll23 = value; KosztLogicznyObslugi = CalculateTotalCost(); }
        }
        private Double coll24;
        public System.Double Coll24
        {
            get { return coll24; }
            set { coll24 = value; KosztLogicznyObslugi = CalculateTotalCost(); }
        }
        private Double coll25;
        public System.Double Coll25
        {
            get { return coll25; }
            set { coll25 = value; KosztLogicznyObslugi = CalculateTotalCost(); }
        }
        private Double coll26;
        public System.Double Coll26
        {
            get { return coll26; }
            set { coll26 = value; KosztLogicznyObslugi = CalculateTotalCost(); }
        }
        private Double coll27;
        public System.Double Coll27
        {
            get { return coll27; }
            set { coll27 = value; KosztLogicznyObslugi = CalculateTotalCost(); }
        }
        private Double coll28;
        public System.Double Coll28
        {
            get { return coll28; }
            set { coll28 = value; KosztLogicznyObslugi = CalculateTotalCost(); }
        }
        private Double coll29;
        public System.Double Coll29
        {
            get { return coll29; }
            set { coll29 = value; KosztLogicznyObslugi = CalculateTotalCost(); }
        }
        private Double coll30;
        public System.Double Coll30
        {
            get { return coll30; }
            set { coll30 = value; KosztLogicznyObslugi = CalculateTotalCost(); }
        }
        private Double coll31;
        public System.Double Coll31
        {
            get { return coll31; }
            set { coll31 = value; KosztLogicznyObslugi = CalculateTotalCost(); }
        }
        private Double coll32;
        public System.Double Coll32
        {
            get { return coll32; }
            set { coll32 = value; KosztLogicznyObslugi = CalculateTotalCost(); }
        }

        private Double wspolczynik;
        public System.Double Wspolczynik
        {
            get { return wspolczynik; }
            set { wspolczynik = value; KosztLogicznyObslugi = CalculateTotalCost(); }
        }

        private double CalculateTotalCost()
        {
            double total = 0.0f;
            total += coll1 + coll2 + coll3 + coll4 + coll5 + coll6 + coll7 + coll8 + coll9 + coll10;
            total += coll11 + coll12+coll13 + coll14 + coll15+ coll16 + coll17 + coll18 + coll19 + coll20;
            total += coll21 + coll22 + coll23 + coll24 + coll25 + coll26 + coll27 + coll28 + coll29 + coll30+ coll31 + coll32;
            total *= Wspolczynik;
            return total;
        }

        private Double kosztLogicznyObslugi;
        public System.Double KosztLogicznyObslugi
        {
            get { return kosztLogicznyObslugi; }
            set { kosztLogicznyObslugi = value; }
        }
    }
}
