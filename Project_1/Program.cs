using Store.Models;
using Store.Library.Services;

namespace Project_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var products = StoreServiceProxy.Current;
            var cart = new List<Product>();
            while (true)
            {
                Console.WriteLine("Please enter a seleciton\nInventory Management: 1\nShop: 2\nExit Program: 3\n");
                var choiceA = Console.ReadLine();
                if (int.TryParse(choiceA, out int intChoiceA))
                {
                    switch (intChoiceA)
                    {
                        case 1://Inventory Management
                            bool inInventory = true;
                            while (inInventory)
                            {
                                Console.WriteLine("\nInventory Manager\nCreate Item: 1\nRead Item: 2\nUpdate Item: 3\nDelete Item: 4\nExit Inventory: 5\n");
                                var choiceB = Console.ReadLine();
                                if (int.TryParse(choiceB, out int intChoiceB))
                                {
                                    switch (intChoiceB)
                                    {
                                        case 1://Create Item
                                            Console.WriteLine("Enter new object name: ");
                                            string name = Console.ReadLine() ?? "No name";
                                            Console.WriteLine("Enter new object description: ");
                                            string description = Console.ReadLine() ?? "No description";
                                            Console.WriteLine("Enter new object price: ");
                                            string price = Console.ReadLine() ?? "No price";
                                            Console.WriteLine("Enter quantity: ");
                                            string quantity = Console.ReadLine() ?? "0";
                                            if(int.TryParse(quantity, out int intQuantity))
                                            {

                                            }

                                            products.AddOrUpdate(
                                                new Product
                                                {
                                                    Name = name,
                                                    Description = description,
                                                    Price = price,
                                                    Quantity = intQuantity
                                                });

                                            break;

                                        case 2://Read Items
                                            products?.Products?.ToList()?.ForEach(Console.WriteLine);
                                            break;

                                        case 3://Update Items
                                            Console.WriteLine("Enter Item ID to update: ");
                                            string targetUpdate = Console.ReadLine();
                                            if (int.TryParse(targetUpdate, out int intTargetUpdate))
                                            {
                                                Console.WriteLine("Enter updated object name: ");
                                                string nameUpd = Console.ReadLine() ?? "No name";
                                                Console.WriteLine("Enter updated object description: ");
                                                string descriptionUpd = Console.ReadLine() ?? "No description";
                                                Console.WriteLine("Enter updated object price: ");
                                                string priceUpd = Console.ReadLine() ?? "No price";
                                                Console.WriteLine("Enter updated quantity: ");
                                                string quantityUpd = Console.ReadLine() ?? "0";
                                                if (int.TryParse(quantityUpd, out int intQuantityUpd))
                                                {

                                                }
                                                products.Update(intTargetUpdate, nameUpd, descriptionUpd, priceUpd, intQuantityUpd);
                                            }
                                                break;

                                        case 4://Delete Items
                                            Console.WriteLine("Enter Item ID to delete: ");
                                            string targetDelete = Console.ReadLine();
                                            if(int.TryParse(targetDelete, out int intTargetDelete))
                                            {
                                                products.Delete(intTargetDelete);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Item ID not found");
                                            }
                                            break;

                                        case 5:
                                            inInventory = false;
                                            break;
                                    }
                                }
                            }
                            break;

                        case 2:
                            bool inShop = true;
                            while (inShop)
                            {
                                Console.WriteLine("\nShop:");
                                products?.Products?.ToList()?.ForEach(Console.WriteLine);
                                Console.WriteLine("Cart:");
                                foreach(var product in cart)
                                {
                                    Console.WriteLine($"{product.Name} - ${product.Price}");
                                }
                                Console.WriteLine("\nEnter a selection\nAdd Item to Cart: 1\nRemove Item from Cart: 2\nCheck out: 3\n");
                                var choiceC = Console.ReadLine();
                                if (int.TryParse(choiceC, out int intChoiceC))
                                {
                                    switch (intChoiceC)
                                    {
                                        case 1: //add to cart
                                            Console.WriteLine("Enter Item ID to add to cart\n");
                                            string cartAdd = Console.ReadLine();
                                            if (int.TryParse(cartAdd, out int intCartAdd))
                                            {
                                                if (products.GetProductById(intCartAdd).Quantity > 0)
                                                {
                                                    cart.Add(products?.GetProductById(intCartAdd));
                                                    products.GetProductById(intCartAdd).Quantity--;
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Item not available\n");
                                                }
                                                }
                                                break;

                                        case 2: //remove from cart
                                            Console.WriteLine("Enter Item ID to remove from cart\n");
                                            string cartRemove = Console.ReadLine();
                                            if (int.TryParse(cartRemove, out int intCartRemove))
                                            {
                                                cart.Remove(products?.GetProductById(intCartRemove));
                                                products.GetProductById(intCartRemove).Quantity++;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Item not found\n");
                                            }
                                            break;

                                        case 3://check out
                                            int subtotal = 0;
                                            foreach(var product in cart)
                                            {
                                                if(int.TryParse(product.Price, out int intPrice))
                                                {
                                                    subtotal += intPrice;
                                                }
                                            }
                                            Console.WriteLine($"{"\n"}Subtotal: {subtotal}\nTaxes: {subtotal*0.07}\nTotal: {subtotal*1.07}{"\n"}");
                                            inShop = false;
                                            break;
                                    }
                                }
                            }
                            break;

                        case 3:
                            Environment.Exit(-1);
                            break;
                    }
                }
            }
            //
        }
    }
}
