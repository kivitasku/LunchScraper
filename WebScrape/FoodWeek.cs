using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Ravintolat

{
    class FoodWeek
    {
        public string paiva
        { get; set; }





        public List<string> ruoat;


        public FoodWeek()
        {
            paiva = "";
            ruoat = new List<string>();
        }

        public FoodWeek(string paiva, HtmlNodeCollection nodet)
        {
            this.paiva = paiva;
            ruoat = new List<string>();
            foreach (HtmlNode node in nodet)
            {
                ruoat.Add(node.InnerText);
            }
            
        }

        public FoodWeek(string paiva, HtmlNode node)
        {
            this.paiva = node.OuterHtml;
            ruoat = new List<string>();
            HtmlNodeCollection dishes = node.SelectNodes($".//p[contains(@class, 'dish')]");
            foreach (HtmlNode dish in dishes)
            {
                Console.WriteLine(dish);
                ruoat.Add(dish.InnerText);
            }

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



    enum Paiva
    {
        Maa,
        Tii,
        Kes,
        Tor,
        Per,
        Lau,
        Sun
    }
}
