using Store.Models;
using System.Collections.ObjectModel;

namespace Store.Library.Services
{
    public class StoreServiceProxy
    {
        private StoreServiceProxy()
        {
            products = new List<Product>
            {
                new Product
                {
                    Name = "Bread",
                    Description = "Good for sandwiches",
                    Price = "5",
                    Quantity = 10,
                    Id = 0
                },
                new Product
                {
                    Name = "Toast",
                    Description = "Better than bread",
                    Price = "7",
                    Quantity = 6,
                    Id = 1
                }
            };
        }

        private static StoreServiceProxy? instance;
        private static object instanceLock = new object();
        public static StoreServiceProxy Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new StoreServiceProxy();
                    }
                }
                return instance;
            }
        }

        private List<Product>? products;
        public ReadOnlyCollection<Product>? Products
        {
            get
            {
                return products?.AsReadOnly();
            }
        }

        public int LastId
        {
            get
            {
                if(products?.Any()?? false)
                {
                    return products?.Select(c => c.Id)?.Max()?? 0;
                }
                return 0;
            }
        }

        public Product? AddOrUpdate(Product product)
        {
            if(products == null)
            {
                return null;
            }

            var isAdd = false;

            if(product.Id == 0)
            {
                product.Id = LastId + 1;
                isAdd = true;
            }

            if (isAdd)
            {
                products.Add(product);
            }

            return product;
        }

        public Product? GetProductById(int id)
        {
            return products?.Find(i => i.Id == id);
        }

        public void Update(int id, string name, string description, string price, int quantity)
        {
            if(products == null)
            {
                return;
            }
            var productToUpdate = products.FirstOrDefault(c => c.Id == id);

            if (productToUpdate != null)
            {
                productToUpdate.Name = name;
                productToUpdate.Description = description;
                productToUpdate.Price = price;
                productToUpdate.Quantity = quantity;
            }
        }

        public void Delete(int id)
        {
            if(products == null)
            {
                return;
            }
            var productToDelete = products.FirstOrDefault(c => c.Id == id);

            if(productToDelete != null)
            {
                products.Remove(productToDelete);
            }
        }
    }
}