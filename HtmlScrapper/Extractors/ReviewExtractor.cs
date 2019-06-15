using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace HtmlScrapper.Extractors
{
    static class ReviewExtractor
    {
        public static string GetReview(HtmlNode opinion)
        {
            var review = opinion.SelectSingleNode(".//p[@class=\"product-review-body\"]");

            string reviewText = string.Empty;
            if(review != null)
            {
                reviewText = review.InnerText;                
            }

            return reviewText;
        }
    }
}
