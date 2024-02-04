using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ravintolat
{
    class RavintolaDay
    {
        public string Nimi { get; set; }
        List<string> ruoat = new List<string>();

        public void setNimi(string nimi)
        {
            this.Nimi = nimi;

        }


        public string getNimi()
        {
            return this.Nimi;
        }

        public void appendRuoka(string ruoka)
        {
            ruoat.Add(ruoka);
        }




        public List<string> getRuoat()
        {
            return ruoat;
        }

    }
}
