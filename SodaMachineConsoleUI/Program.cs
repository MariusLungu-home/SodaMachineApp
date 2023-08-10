using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using SodaMachineLibrary;
using SodaMachineLibrary.DataAccess;
using SodaMachineLibrary.Logic;
using SodaMachineLibrary.Models;
using System.Xml.Linq;

namespace SodaMachineConsoleUI;

public class Program
{
    private static ServiceProvider _serviceProvider;

    static void Main(string[] args)
    {
        RegisterServices();
        string userSelection = string.Empty;
        Console.WriteLine("Welcome to our Soda Machine");
        string userId = Guid.NewGuid().ToString();
        SodaModel sodaModel = new();

        do
        {
            userSelection = ShowMenu();

            switch (userSelection)
            {
                case "1":
                    ShowSodaPrice();
                    break;
                case "2":
                    ListSodaOptions(false);
                    break;
                case "3":
                    ShowAmountDeposited(userId);
                    break;
                case "4":
                    Console.WriteLine("How much money do you want to deposit?");
                    var amount = Console.ReadLine();
                    DepositMoney(userId, amount);
                    break;
                case "5":
                    CancelTransaction(userId);
                    break;
                case "6":
                    Console.WriteLine("Select soda from the machine:");
                    sodaModel = ListSodaOptions(true).Item2;
                    BuySoda(sodaModel, userId);
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

    private static void PressAnyKeyToContinue() 
    {
        Console.WriteLine("\n\n\nPress return to continue...");
        Console.ReadLine();
    }

    private static void ShowSodaPrice()
    {
        var sodaPrice = _serviceProvider.GetService<ISodaMachineLogic>().GetSodaPrice();
        Console.Clear();
        Console.WriteLine($"The soda price is {sodaPrice}$");
        PressAnyKeyToContinue();
    }

    private static void ShowAmountDeposited(string userId)
    {
        // get user id
        var amountDeposited = _serviceProvider.GetService<ISodaMachineLogic>().GetMoneyInsertedTotal(userId);
        Console.WriteLine($"The amount deposited is: {amountDeposited}$");
        
        PressAnyKeyToContinue();
    }

    private static void DepositMoney(string userId, string amount)
    {
        var amountDeposited = _serviceProvider.GetService<ISodaMachineLogic>().MoneyInserted(userId, decimal.Parse(amount));
        Console.WriteLine($"The amount deposited is: {amountDeposited}$");
        PressAnyKeyToContinue();
    }
    
    private static void CancelTransaction(string userId)
    {
        _serviceProvider.GetService<ISodaMachineLogic>().IssueFullRefund(userId);
        Console.WriteLine($"The transaction has been cancelled, please collect your credit.");
        PressAnyKeyToContinue();
    }

    private static void BuySoda(SodaModel sodaModel, string userId)
    { 
        (SodaModel soda, List<CoinModel> coins, string errorMessage)soda = _serviceProvider.GetService<ISodaMachineLogic>().RequestSoda(sodaModel, userId);
        if (soda.errorMessage != string.Empty)
        {
            Console.WriteLine($"The transaction has been cancelled, please collect your credit.");
        }
        else
        {
            Console.WriteLine($"You have bought a {soda.soda.Name}.");
            Console.WriteLine($"Please collect your change: {soda.coins.Count} coins: {String.Join(", ", soda.coins.Select(col => col.Name))}");
        }

        PressAnyKeyToContinue();
    }

    private static (string, SodaModel) ListSodaOptions(bool isBuying)
    {
        Console.Clear();
        Console.WriteLine("The soda options are:");
        var sodaOptions = _serviceProvider.GetService<ISodaMachineLogic>().GetSodaInventory();
        for (int i = 0; i < sodaOptions.Count; i++)
        {
            Console.WriteLine($"{i}:  {sodaOptions[i].Name}, slot:{sodaOptions[i].SlotOccupied}");
        }
        if (isBuying)
        {
            Console.WriteLine("Please select the number of the soda you want to buy:");
            int userIndex = int.Parse(Console.ReadLine());
            SodaModel sodaModel = sodaOptions.ElementAt<SodaModel>(userIndex);
            
            return (string.Empty, sodaModel);
        }
        else 
        {
            PressAnyKeyToContinue();

            return (string.Empty, new SodaModel());
        }
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

        Console.WriteLine("\n\nYour option:");

        return Console.ReadLine();
    }

    private static void RegisterServices() 
    {
        var collection = new ServiceCollection();
        
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appSettings.json", true, true)
            .Build();

        collection.AddSingleton<IConfiguration>(config);
        collection.AddTransient<IDataAccess, TextFileDataAccess>();
        collection.AddTransient<ISodaMachineLogic, SodaMachineLogic>();

        _serviceProvider = collection.BuildServiceProvider();
    }
}