//Ethan Cala Homework 3 for .Net Programming CPSC-23000

// https://github.com/ethancala


/*The program will enable the user to purchase items from your store. Your store's inventory and corresponding prices will come from a text file. The user will specify the
name of the text file at the beginning of the program. The program will read the inventory and then present the user a
list of what they can buy. To purchase something, the user will type the name of the item and then specify how many
they would like. The program will add the item and quantity to its running tab. At the end, the program will show the
user what they purchased and the final price.*/

//stack overflow, and chatGPT were used to figure out certain lines of code with NOTE, but no code was entirely generated:
//how to use Dictionaries, how to read from files using stream reader, and how to format data.

//imports
using System;
using System.Collections.Generic;
using System.IO;



namespace cala_dotnet_HW3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //create total amount 
            double totalAmount = 0;
            
            //create a dictionary to store inventory and cart information.
            
            //string a double for item and price of item
            Dictionary<string, double> inventory = new Dictionary<string, double>();

            //string and int for item and number of items
            Dictionary<string, int> cart = new Dictionary<string, int>();


            //welcome message and banner
            Console.WriteLine("****************************");
            Console.WriteLine("STOREFRONT V1.0");
            Console.WriteLine("****************************");
            
            //get the file path and store as a string
            Console.WriteLine("Please enter the path of grocery items file: \n");
            string filePath = Console.ReadLine();
            
            //ensure the file exists
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Error: File not found");
                return;
            }
            
            //try catch to read the file
            try
            {
                //use stream reader 
                using (StreamReader reader = new StreamReader(filePath))
                { //NOTE: I had to look up how to do this
                    while (!reader.EndOfStream)
                    {
                        string item = reader.ReadLine()?.Trim().ToLower();
                        //NOTE: looked this format up
                        if (double.TryParse(reader.ReadLine(), out double price) && !string.IsNullOrEmpty(item))
                        {
                            inventory[item] = price;
                        }
                    }
                }

            }
            //catch exception if issue with file reading
            catch (Exception exception)
            {
                Console.WriteLine($"Error reading the file: {exception} ");
                    return;
            }
            
            //now that we read the file, we can print out the inventory data to prompt the user to shop
            Console.WriteLine("\nWhat would you like to buy?");
            
            //for each loop where we provide each keyvalue pair
            //NOTE: I had to look up how to do this!
            foreach (var item in inventory)
            {
                Console.WriteLine($"{item.Key}:  ${item.Value}");
            }
            
            //loop until users breaks with quit
            while (true)
            {
                //prompt user for input
                Console.Write("Enter the name of the Item, or 'quit' to end:  ");
                string itemPurchased = Console.ReadLine().Trim().ToLower();

                //quit if user user types quit
                if (itemPurchased == "quit")
                {
                    break;
                }

                //check if the item is found
                if (!inventory.ContainsKey(itemPurchased))
                {
                    //if not try again
                    Console.WriteLine("Error: Item not found. Please try again.");
                    continue;
                }
                
                //if item found, ask how many
                Console.WriteLine("How many would you like: ");
                
                //NOTE same format looked up for validation
                if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
                {
                    Console.WriteLine("Invalid quantity, please try again.");
                    continue;
                }
                
                //if quantity is valid, add value to cart dictionary
                //if already in cart
                if (cart.ContainsKey(itemPurchased))
                {
                    //add more
                    cart[itemPurchased] += quantity;
                }
                else
                {
                    //else add it for the first time
                    cart[itemPurchased] = quantity;
                }
                
                //lastly, just calculate total and provide receipt
                Console.WriteLine("Here is what you bought: ");
                

                //iterate through each key-value pair
                foreach (var item in cart)
                {
                    //get cost and add to total while displaying the amount
                    //NOTE formatting was looked up here
                    double cost = item.Value * inventory[item.Key];
                    Console.WriteLine($"{item.Key} x{item.Value} = ${cost:F2}");
                    totalAmount += cost;
                }
                
                
            }
            //print total and thank the user!
            Console.WriteLine($"Your total for today: ${totalAmount:F2}");
            Console.WriteLine("Thank you for shopping with us!");
        }
    }
}
