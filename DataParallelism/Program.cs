using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DataParallelism
{
    class Program
    {
        static void Main(string[] args)
        {
            Product[] products = new Product[8000];
            List<string> productAttributes = new List<string>();
            List<Product> processedProduct = new List<Product>();
            //Sequential process.
            foreach (var item in products)
            {
                processedProduct.Add(ProcessProduct(item, productAttributes));
                // processing each product takes 1sec so total time = 2.2hours
            }

            // parallel processing
            BlockingCollection<Product> products1 = new BlockingCollection<Product>();
            BlockingCollection<string> productAttributes1 = new BlockingCollection<string>(new ConcurrentQueue<string>(productAttributes));
            Parallel.ForEach(products, (product)=> {
                products1.Add(ParallelProcessProduct(product, productAttributes1));
            });


        }
        public static Product ProcessProduct(Product product, List<string> productAttributes)
        {
            Thread.Sleep(1000); // The product attributes are used to calculate the product price.
            return product;     // Assume price calculation requires external service call, mocked by sleep.
        }

        public static Product ParallelProcessProduct(Product product, BlockingCollection<string> productAttributes)
        {
            Thread.Sleep(1000); // The product attributes are used to calculate the product price.
            return product;
        }
    }


    public struct Product
    {
        public string Code { get; set; }
        public string ProdctDescription { get; set; }
        public Decimal Price { get; set; }

    }
}
