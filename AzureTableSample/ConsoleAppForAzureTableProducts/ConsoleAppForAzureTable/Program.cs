using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppForAzureTable
{
   
    class Product : TableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public double Price { get; set; }

        public bool Status { get; set; }
        public DateTime Date { get; set; }

        public Product(int id, string name, DateTime date, double price, bool status)
        {
            Name = name;
            Date = date;
            Price = price;
            Status = status;
            PartitionKey = "Products";
            RowKey = id.ToString();
        }

        public Product()
        {

        }
    }

class Program
    {

        static void Main(string[] args)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                               CloudConfigurationManager.GetSetting("StorageConnection"));
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("Products");
            table.CreateIfNotExists();



            CreateProduct(table, new Product(1,"Prod A", DateTime.Now,20.5,true));
            CreateProduct(table, new Product(2, "Prod B", DateTime.Now.AddMinutes(10), 7, false));
            CreateProduct(table, new Product(3, "Prod C", DateTime.Now.AddDays(-1), 3.9, true));
            CreateProduct(table, new Product(4, "Prod D", DateTime.Now.AddMonths(-2), 10, false));
          

            UpdaterProduct(table, "Products", "2", "Test");
         
            GetProduct(table, "Products", "2");
            GetAllProducts(table);
            Console.ReadKey();
        }


        static void CreateProduct(CloudTable table, Product product)
        {
            TableOperation insert = TableOperation.Insert(product);

            table.Execute(insert);
        }

        static void GetAllProducts(CloudTable table)
        {
            TableQuery<Product> query = new TableQuery<Product>()
                    .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Products"));

            Console.WriteLine("GetAllProducts begin");
            foreach (Product product in table.ExecuteQuery(query))
            {
                Console.WriteLine(product.Id);
                Console.WriteLine(product.Name);
                Console.WriteLine(product.Price);
            }
            Console.WriteLine("GetAllProducts ends");
        }

        static void GetProduct(CloudTable table, string partitionKey, string rowKey)
        {
            TableOperation retrieve = TableOperation.Retrieve<Product>(partitionKey, rowKey);

            TableResult result = table.Execute(retrieve);

            Console.WriteLine(((Product)result.Result).Name);
        }

        static void UpdaterProduct(CloudTable table, string partitionKey, string rowKey, string newName)
        {
            TableOperation retrieve = TableOperation.Retrieve<Product>(partitionKey, rowKey);

            TableResult result = table.Execute(retrieve);

            Product product = (Product)result.Result;

            product.ETag = "*";
            product.Name = newName;

            if (result != null)
            {
                TableOperation update = TableOperation.Replace(product);

                table.Execute(update);
            }

        }

    }
}
