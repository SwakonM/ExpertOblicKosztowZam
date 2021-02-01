using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertOblicKosztowZam
{
    public class TabMod
    {
        public String Nazwa;
        public Int32 id_mod;
        public Int32 id_sub;
        public Int32 id_next;
        public Int32 id_before;   
        public Double t_in;
        public Double c_in;
        public Double t_out;
        public Double c_out;
        public Double t_forward;
        public Double c_forward;
        public Double t_back;
        public Double c_back;

        public TabMod(string nazwa, int id_mod, int id_sub )
        {
            Nazwa = nazwa;
            this.id_mod = id_mod;
            this.id_sub = id_sub;
         
        }
    }
}
