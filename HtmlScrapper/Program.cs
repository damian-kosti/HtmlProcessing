using HtmlScrapper.HtmlModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlScrapper
{
    class Program
    {
        static void Main(string[] args)
        {

            List<int> productIdList = ProductManager.GetProducts();
            
            foreach (var product in productIdList)
            {
                var opinions = OpinionManager.GetOpinions(product);
                foreach(var opinion in opinions)
                {
                    Console.WriteLine(opinion.InnerText);
                }
            }
            
        }
    }
}
