using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace HtmlProcessing.HtmlModules
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
                var currentPage = GetPage(minPageNumber);

                var pagination = currentPage.DocumentNode.SelectSingleNode("//div[@class=\"pagination-top\"]");
                var input = pagination.SelectSingleNode(".//input");
                var maxPageNumber = input.GetAttributeValue("data-pageCount", 0) - 1;

                var currentPageNumber = minPageNumber;
                while (currentPageNumber < maxPageNumber)
                {

                    Thread.Sleep(5000);
                    currentPage = GetPage(currentPageNumber);
                    var productsContainer = currentPage.DocumentNode.SelectSingleNode("//div[@class=\"category-list-body js_category-list-body js_search-results\"]");
                    var products = productsContainer.SelectNodes("//div");

                    foreach (var p in products)
                    {
                        var productId = p.GetAttributeValue("data-pid", 0);
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
