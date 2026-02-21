namespace ECommerce_SellerMgmt
{
    internal class Program
    {

        static string[] productNames = new string[10];
        static double[] productPrices = new double[10];
        static int[] productStocks = new int[10];
        static int productCount = 0;


        static List<string> shoppingCart = new List<string>();

        static void Main(string[] args)
        {

            productNames[0] = "T-Shirt"; productPrices[0] = 15.00; productStocks[0] = 50;
            productNames[1] = "Hoodie"; productPrices[1] = 35.00; productStocks[1] = 20;
            productCount = 2;

            while (true)
            {
                Console.WriteLine("\n--- MINI ONLINE SHOP ---");
                Console.WriteLine("1. Add Product to Inventory");
                Console.WriteLine("2. View Shop Products");
                Console.WriteLine("3. Add Product to Cart");
                Console.WriteLine("4. View Shopping Cart");
                Console.WriteLine("5. Exit");
                Console.Write("Select: ");
                string choice = Console.ReadLine();

                if (choice == "1") AddInventory();
                else if (choice == "2") ViewProducts();
                else if (choice == "3") AddToCart();
                else if (choice == "4") ViewCart();
                else if (choice == "5") break;
            }
        }

        static void AddInventory()
        {
            Console.Write("Enter Product Name: ");
            productNames[productCount] = Console.ReadLine();
            Console.Write("Enter Price: ");
            productPrices[productCount] = double.Parse(Console.ReadLine());
            Console.Write("Enter Quantity: ");
            productStocks[productCount] = int.Parse(Console.ReadLine());

            productCount++;
            Console.WriteLine("Inventory Updated!");
        }

        static void ViewProducts()
        {
            Console.WriteLine("\nID\tName\t\tPrice\tStock");
            for (int i = 0; i < productCount; i++)
            {
                Console.WriteLine(i + "\t" + productNames[i] + "\t\t$" + productPrices[i] + "\t" + productStocks[i]);
            }
        }

        static void AddToCart()
        {
            Console.Write("Enter Product ID to buy: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("How many items?: ");
            int qty = int.Parse(Console.ReadLine());

            if (id < productCount && productStocks[id] >= qty)
            {

                productStocks[id] -= qty;


                double total = productPrices[id] * qty;
                shoppingCart.Add(productNames[id] + " (Qty: " + qty + ") - Total: $" + total);

                Console.WriteLine("Added to cart!");
            }
            else
            {
                Console.WriteLine("Error: Invalid ID or Not enough stock.");
            }
        }

        static void ViewCart()
        {
            Console.WriteLine("\n--- YOUR SHOPPING CART ---");
            if (shoppingCart.Count == 0) Console.WriteLine("Cart is empty.");

            for (int i = 0; i < shoppingCart.Count; i++)
            {
                Console.WriteLine(shoppingCart[i]);
            }
        }
    }
}
