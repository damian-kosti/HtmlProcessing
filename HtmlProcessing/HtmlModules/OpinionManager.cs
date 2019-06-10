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
        private static int minOpinionPage = 0;

        public static List<HtmlNode> GetOpinions(int productId)
        {
            var opinionList = new List<HtmlNode>();
            try
            {
                var currentOpinionPage = GetPage(productId, minOpinionPage);
                var reviewTab = currentOpinionPage.DocumentNode.SelectSingleNode("//li[@class=\"page-tab reviews active\"]");
                var tabText = reviewTab.SelectSingleNode(".//span[@class=\"page-tab__title\"]").InnerText;



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
