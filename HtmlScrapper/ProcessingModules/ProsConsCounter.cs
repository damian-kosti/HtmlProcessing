using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;


namespace HtmlScrapper.ProcessingModules
{
    static class ProsConsCounter
    {

        public static int CountPros(HtmlNode opinion)
        {
            int counter = 0;
            var pros = opinion.SelectSingleNode(".//div[@class=\"pros-cell\"]");
            if (pros != null)
            {
                var prosList = pros.SelectNodes(".//li");
                if(prosList != null)
                {
                    counter = prosList.Count;
                }
            }

            return counter;
        }

        public static int CountCons(HtmlNode opinion)
        {
            int counter = 0;
            var cons = opinion.SelectSingleNode(".//div[@class=\"cons-cell\"]");

            if (cons != null)
            {
                var consList = cons.SelectNodes(".//li");
                if (consList != null)
                {
                    counter = consList.Count;
                }
            }

            return counter;
        }
    }
}
