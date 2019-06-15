using HtmlScrapper.HtmlModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlScrapper.Extractors;
using HtmlScrapper.ProcessingModules;

namespace HtmlScrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            var traits = new Dictionary<string, int>();
            var path = Utilities.GetFilePath();

            List<int> productIdList = ProductManager.GetProducts();
            foreach (var product in productIdList)
            {
                var opinions = OpinionManager.GetOpinions(product);
                foreach(var opinion in opinions)
                {
                    var mark = MarkExtractor.GetMark(opinion);
                    var review = ReviewExtractor.GetReview(opinion);

                    traits.Add("positive", OpinionProcessor.CalculateStrength(review, Adjectives.GetPositiveAdjectives));
                    traits.Add("negative", OpinionProcessor.CalculateStrength(review, Adjectives.GetNegativeAdjectives));
                    traits.Add("length", review.Length);
                    traits.Add("pros", ProsConsCounter.CountPros(opinion));
                    traits.Add("cons", ProsConsCounter.CountCons(opinion));

                    Utilities.WriteToFile(traits, mark, path);
                    traits.Clear();
                }
            }
        }
    }
}
