using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ravintolat
{
    class RavintolaWeek
    {
        string Nimi;
        List<FoodWeek> ruoat;

        public RavintolaWeek()
        {
            ruoat = new List<FoodWeek>();
        }

        public void setNimi(string nimi)
        {
            this.Nimi = nimi;

        }

        public void appendFoodWeek(FoodWeek ruoka)
        {
            ruoat.Add(ruoka);
        }


        public string getNimi()
        {
            return this.Nimi;
        }

        public List<FoodWeek> getRuoat()
        {
            return ruoat;
        }

    }
}
