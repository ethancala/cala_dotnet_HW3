//Ethan Cala Homework 3 for .Net Programming CPSC-23000

// https://github.com/ethancala


/*The program will enable the user to purchase items from your store. Your store's inventory and corresponding prices will come from a text file. The user will specify the
name of the text file at the beginning of the program. The program will read the inventory and then present the user a
list of what they can buy. To purchase something, the user will type the name of the item and then specify how many
they would like. The program will add the item and quantity to its running tab. At the end, the program will show the
user what they purchased and the final price.*/


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
                {
                    while (!reader.EndOfStream)
                    {
                        string item = reader.ReadLine()?.Trim().ToLower();
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
            
            
            

        }
    }
}
