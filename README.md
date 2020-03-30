# Data-Parallelism
This Repo illustrate the parallel processing of records with blocking collection in C#. [Data Parallelism](https://docs.microsoft.com/en-us/dotnet/standard/parallel-programming/data-parallelism-task-parallel-library)
# Scenario:
Recently, working on a windows service which process the collection of records and populate them in DB. The records are processed in synchronously since there are some shared variables involved and it is taking **~2hours to complete the job**. 
The processing time is **brought down to 5min by using Data Parallelism**.

``` C#
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
            ```

