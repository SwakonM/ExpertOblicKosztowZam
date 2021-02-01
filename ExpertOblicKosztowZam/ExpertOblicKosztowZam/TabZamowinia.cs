using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ExpertOblicKosztowZam
{
     public class TabZamowinia 
    {
        private String idZamowinia;
        public System.String IdZamowinia
        {
            get { return idZamowinia; }
            set { idZamowinia = value; }
        }
        private int liczbaJedostekZam;
        public int LiczbaJedostekZam
        {
            get { return liczbaJedostekZam; }
            set { liczbaJedostekZam = value; }
        }
        public Dictionary<string, Double> _Param = new Dictionary<string, Double>();
        public TabZamowinia(String idZamowinia, int liczbaJedostekZam)
        {
            this.IdZamowinia = idZamowinia;
            this.LiczbaJedostekZam = liczbaJedostekZam;
        }
      
    }

}

