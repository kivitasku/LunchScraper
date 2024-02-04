using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Ravintolat
{
    class Searcher
    {
        public List<RavintolaDay> raflatDay;
        public List<RavintolaWeek> raflatWeek;
        List<string> sivut;
        List<string> menuItemCategoryt;
        public List<string> hreffit;
        public List<string> raflojenNimiaTemp;

        public Searcher()
        {
            raflatDay = new List<RavintolaDay>();
            raflatWeek = new List<RavintolaWeek>();
            sivut = new List<string>();
            menuItemCategoryt = new List<string>();
            hreffit = new List<string>();
            InitializeSearcher();
            gatherHrefs();
        }


        void gatherHrefs()
        {
            foreach (string sivu in sivut)
            {
                var htmlDocument = GetDocu(sivu);

                foreach (string menu in menuItemCategoryt)
                {
                    HtmlNodeCollection divNodesit = htmlDocument.DocumentNode.SelectNodes(menu);

                    if (divNodesit != null)
                    {
                        foreach (HtmlNode divNode in divNodesit)
                        {
                            if (divNode.InnerHtml.Contains("dish"))
                            {
                                
                                
                                hreffit.Add("https://www.lounaat.info" + divNode.SelectSingleNode(".//h3").InnerHtml.Substring(9, (divNode.SelectSingleNode(".//h3").InnerHtml.IndexOf("\">") - 9)));

                                
                            }
                        }
                    }



                }

            }
        }



        public void gatherWeekMeals(string hakusana)
        {
            foreach (string linkki in hreffit)
            {
                HtmlAgilityPack.HtmlDocument html = GetDocu(linkki);
                RavintolaWeek ravintola = new RavintolaWeek();
                ravintola.setNimi(html.DocumentNode.SelectSingleNode(".//h2").InnerText.Trim());
                HtmlNodeCollection sivunItemit = html.DocumentNode.SelectNodes($"//div[@class='item']");
                foreach (HtmlNode itemi in sivunItemit)
                {
                    
                    

                    if (itemi.InnerHtml.ToLower().Contains(hakusana.ToLower()))
                    {
                        
                        Console.WriteLine(itemi.OuterHtml);

                        FoodWeek paivanruoat = new FoodWeek();
                        paivanruoat.paiva = itemi.InnerHtml.Substring(itemi.InnerHtml.IndexOf("<h3>") + 4, (itemi.InnerHtml.IndexOf("</h3>") - itemi.InnerHtml.IndexOf("<h3>")) - 4);


                        
                        

                        
                        foreach (HtmlNode menuitem in itemi.SelectNodes($".//p[contains(@class, 'dish')]"))
                        {
                            paivanruoat.appendRuoka(menuitem.InnerText);
                        }




                        //ravintola.appendFoodWeek(new FoodWeek(itemi.InnerHtml.Substring(itemi.InnerHtml.IndexOf("<h3>") + 4, (itemi.InnerHtml.IndexOf("</h3>") - itemi.InnerHtml.IndexOf("<h3>")) - 4), itemi.SelectNodes($"//p[contains(@class, 'dish')]")));
                        //ravintola.appendFoodWeek(new FoodWeek(itemi.InnerHtml.Substring(itemi.InnerHtml.IndexOf("<h3>") + 4, (itemi.InnerHtml.IndexOf("</h3>") - itemi.InnerHtml.IndexOf("<h3>")) - 4), itemi));

                        ravintola.appendFoodWeek(paivanruoat);

                    }






                }

                if (ravintola.getRuoat().Count != 0)
                {
                    raflatWeek.Add(ravintola);
                } 




            }

        }




        void InitializeSearcher()
        {


            RavintolaDay testi = new RavintolaDay();
            testi.setNimi("RaflanNimiTesti");
            raflatDay.Add(testi);

            for (int i = 0; i < 15; i++)
            {
                sivut.Add($"https://www.lounaat.info/search/filter?view=sijainti&query=Turku&page={i}");
            }

            Console.WriteLine("started");

            for (int i = 0; i < 7; i++)
            {
                menuItemCategoryt.Add($"//div[contains(@class, 'menu item category-{i}')]");
            }




        }

        public void gatherDayMeals()
        {
            foreach (string sivu in sivut)
            {
                var htmlDocument = GetDocu(sivu);

                foreach (string menu in menuItemCategoryt)
                {
                    HtmlNodeCollection divNodesit = htmlDocument.DocumentNode.SelectNodes(menu);

                    if (divNodesit != null)
                    {
                        foreach (HtmlNode divNode in divNodesit)
                        {
                            if (divNode.InnerHtml.Contains("dish"))
                            {
                                RavintolaDay rafla = new RavintolaDay();
                                HtmlNodeCollection dishP = divNode.SelectNodes(".//p[contains(@class, 'dish')]");
                                rafla.setNimi(divNode.SelectSingleNode(".//h3").InnerText);
                                if (dishP != null)
                                {
                                    foreach (HtmlNode dish in dishP)
                                    {
                                        rafla.appendRuoka(dish.InnerText);
                                    }



                                }
                                raflatDay.Add(rafla);
                            }
                        }
                    }



                }

            }
        }


        public void gatherDayWord(string hakusana)
        {
            foreach (string sivu in sivut)
            {
                var htmlDocument = GetDocu(sivu);

                foreach (string menu in menuItemCategoryt)
                {
                    HtmlNodeCollection divNodesit = htmlDocument.DocumentNode.SelectNodes(menu);

                    if (divNodesit != null)
                    {
                        foreach (HtmlNode divNode in divNodesit)
                        {
                            if (divNode.InnerHtml.Contains("dish") && divNode.OuterHtml.ToLower().Contains(hakusana))
                            {
                                RavintolaDay rafla = new RavintolaDay();
                                HtmlNodeCollection dishP = divNode.SelectNodes(".//p[contains(@class, 'dish')]");

                                rafla.setNimi(divNode.SelectSingleNode(".//h3").InnerText);
                                if (dishP != null)
                                {
                                    foreach (HtmlNode dish in dishP)
                                    {
                                        rafla.appendRuoka(dish.InnerText);
                                    }



                                }
                                raflatDay.Add(rafla);
                            }
                        }
                    }



                }

            }
        }


        HtmlAgilityPack.HtmlDocument GetDocu(string url)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = web.Load(url);
            return doc;
        }

    }
}
