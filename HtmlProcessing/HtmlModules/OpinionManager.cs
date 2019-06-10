using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HtmlProcessing.HtmlModules
{
    static class OpinionManager
    {

        private static string urlBegin = "https://www.ceneo.pl/";
        private static string urlEnd = "/opinie-";
        private static int minPageNumber = 0;

        public static List<HtmlNode> GetOpinions(int productId)
        {
            var opinionList = new List<HtmlNode>();
            try
            {
                HtmlDocument currentPage = GetPage(productId, minPageNumber);
                HtmlNode reviewTab = currentPage.DocumentNode.SelectSingleNode("//li[@class=\"page-tab reviews active\"]");
                string innerText = reviewTab.SelectSingleNode(".//span[@class=\"page-tab__title\"]").InnerText;

                int numberBegin = innerText.IndexOf('(') + 1;
                int numberEnd = innerText.IndexOf(')');
                int maxPageNumber = Int32.Parse(innerText.Substring(numberBegin, numberEnd - numberBegin));
                int currentPageNumber = minPageNumber;

                while (currentPageNumber <= maxPageNumber)
                {
                    Thread.Sleep(5000);
                    currentPage = GetPage(productId, currentPageNumber);
                    HtmlNode opinionContainer = currentPage.DocumentNode.SelectSingleNode("//ol[@class=\"product-reviews js_product-reviews js_reviews-hook js_product-reviews-container\"]");
                    HtmlNodeCollection opinions = opinionContainer.SelectNodes("li");
                    foreach(var op in opinions)
                    {
                        opinionList.Add(op);
                    }

                    currentPageNumber++;
                }
            }
            catch
            {
                return opinionList;
            }
            return opinionList;
        }


        public static HtmlDocument GetPage(int productId, int page)
        {
            var web = new HtmlWeb();
            var url = urlBegin + productId + urlEnd + page.ToString();
            return web.Load(url);
        }
    }
}
