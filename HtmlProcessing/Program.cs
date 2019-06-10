using HtmlProcessing.HtmlModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlProcessing
{
    class Program
    {
        static void Main(string[] args)
        {

            var productList = ProductManager.GetProducts();

            foreach (var product in productList)
            {

            }

        }
    }
}
