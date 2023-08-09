using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using SodaMachineLibrary;
using SodaMachineLibrary.DataAccess;
using SodaMachineLibrary.Logic;

namespace SodaMachineConsoleUI;

internal class Program
{
    private static ServiceProvider _serviceProvider;

    static void Main(string[] args)
    {
        RegisterServices();
        string userSelection = string.Empty;
        
        Console.WriteLine("Welcome to our Soda Machine");

        do
        {
            userSelection = ShowMenu();

            switch (userSelection)
            {
                case "1":// Show soda price
                    break;
                case "2":// List soda options
                    break;
                case "3":// Show amount deposited
                    break;
                case "4":// Deposit money
                    break;
                case "5":// Cancel transaction
                    break;
                case "6":// Buy soda
                    break;

                case "9":// Close machine
                    // Don't do anything, we'll let the while loop handle it
                    break;

                default:
                    // Don't do anything, we'll let the while loop handle it
                    break;

            }

            Console.Clear();

        } while (userSelection != "9");

        Console.WriteLine("Thanks, have a nice day.");
        Console.WriteLine("Press return to quit..");

        Console.ReadLine();
    }

    private static string ShowMenu()
    {
        Console.WriteLine("Please make a selection from the following options:");
        
        Console.WriteLine("1: Show soda price");
        Console.WriteLine("2: List soda options");
        Console.WriteLine("3: Show amount deposited");
        Console.WriteLine("4: Deposit money");
        Console.WriteLine("5: Cancel transaction");
        Console.WriteLine("6: Buy soda");

        Console.WriteLine("9: Close machine");
    
        return Console.ReadLine();
    }

    private static void RegisterServices() 
    {
        var collection = new ServiceCollection();
        
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true, true)
            .Build();

        collection.AddSingleton<IConfiguration>(config);
        collection.AddTransient<IDataAccess, TextFileDataAccess>();
        collection.AddTransient<ISodaMachineLogic, SodaMachineLogic>();

        _serviceProvider = collection.BuildServiceProvider();
    }
}