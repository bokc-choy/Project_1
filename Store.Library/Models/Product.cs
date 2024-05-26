using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Models
{
    public class Product
    {
        public string Name { get; set; }
        public string Description { get; set; }

        private string price;
        public string Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value.Replace("$", "");
            }
        }
        public int Id { get; set; }

        public int Quantity { get; set; }

        public Product() { }
        public string? Display
        {
            get
            {
                return ToString();
            }
        }
        public override string ToString()
        {
            return $"[{Id}] {Name} - {Description} - ${Price} - {Quantity} in stock";
        }

        public Product(Product product)
        {
            this.Name = product.Name;
            this.Description = product.Description;
            this.Price = product.Price;
            this.Quantity = product.Quantity;
            this.Id = product.Id;
        }
    }
}
