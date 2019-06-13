using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace HtmlScrapper.HtmlModules
{
    static class ProductManager
    {
        //"https://www.ceneo.pl/Monitory;0020-30-0-0-"
        //"https://www.ceneo.pl/Tablety_i_czytniki_e-book;0020-30-0-0-"
        private static string urlBegin = "https://www.ceneo.pl/Monitory;0020-30-0-0-";
        private static string urlEnd = ".htm";
        private static int minPageNumber = 0;

        public static List<int> GetProducts()
        {
            var productList = new List<int>();
            try
            {
                HtmlDocument currentPage = GetPage(minPageNumber);

                HtmlNode pagination = currentPage.DocumentNode.SelectSingleNode("//div[@class=\"pagination-top\"]");
                HtmlNode input = pagination.SelectSingleNode(".//input");
                int maxPageNumber = input.GetAttributeValue("data-pageCount", 0) - 1;
                int currentPageNumber = minPageNumber;
                while (currentPageNumber < maxPageNumber)
                {
                    Thread.Sleep(5000);
                    currentPage = GetPage(currentPageNumber);
                    HtmlNode productsContainer = currentPage.DocumentNode.SelectSingleNode("//div[@class=\"category-list-body js_category-list-body js_search-results\"]");
                    HtmlNodeCollection products = productsContainer.SelectNodes("//div");

                    foreach (var p in products)
                    {
                        int productId = p.GetAttributeValue("data-pid", 0);
                        if (productId > 0 && !productList.Contains(productId))
                        {
                            productList.Add(productId);
                        }
                    }
                    currentPageNumber++;
                }
            }
            catch
            {
                return productList;
            }               

            return productList;
        }

        static public HtmlDocument GetPage(int page)
        {
            var web = new HtmlWeb();
            var url = urlBegin + page.ToString() + urlEnd;
            return web.Load(url);
        }
    }
}
