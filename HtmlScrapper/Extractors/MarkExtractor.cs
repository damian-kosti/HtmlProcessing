using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace HtmlScrapper.Extractors
{
    class MarkExtractor
    {
        public static string GetMark(HtmlNode opinion)
        {
            var mark = opinion.SelectSingleNode(".//span[@class=\"review-score-count\"]");
            var value = string.Empty;
            if(mark != null)
            {
                value = mark.InnerText.Split('/')[0];
            }

            return value;
        }
    }
}
