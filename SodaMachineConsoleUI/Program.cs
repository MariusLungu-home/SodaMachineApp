using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using SodaMachineLibrary.DataAccess;
using SodaMachineLibrary.Logic;
using SodaMachineLibrary.Models;

namespace SodaMachineConsoleUI;

public class Program
{
    private static IServiceProvider? _serviceProvider;
    private static ISodaMachineLogic? _sodaMachinelogic;
    private static string? userId;
    static void Main(string[] args)
    {
        RegisterServices();

        _sodaMachinelogic = _serviceProvider.GetService<ISodaMachineLogic>();
        userId = new Guid().ToString();

        string? userSelection = string.Empty;
        Console.WriteLine("Welcome to our Soda Machine");

        do
        {
            userSelection = ShowMenu();

            switch (userSelection)
            {
                case "1":
                    ShowSodaPrice();
                    break;
                case "2":
                    ListSodaOptions();
                    break;
                case "3":
                    ShowAmountDeposited();
                    break;
                case "4":
                    DepositMoney();
                    break;
                case "5":
                    CancelTransaction();
                    break;
                case "6":
                    RequestSoda();
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
        var sodaPrice = _sodaMachinelogic!.GetSodaPrice();
        Console.Clear();
        Console.WriteLine($"The soda price is {sodaPrice}$");
        PressAnyKeyToContinue();
    }

    private static void ShowAmountDeposited()
    {
        decimal? amountDeposited = _sodaMachinelogic!.GetMoneyInsertedTotal(userId!);
        Console.WriteLine($"The amount deposited is: {amountDeposited}$");
        
        PressAnyKeyToContinue();
    }

    private static void DepositMoney()
    {
        // get what to deposit
        Console.WriteLine("How much would you like to add to the machine: ");
        string? amountText = Console.ReadLine();

        bool? isValidAmount = decimal.TryParse(amountText, out decimal amountAdded);

        // deposit that amount
        _sodaMachinelogic!.MoneyInserted(userId!, amountAdded);

        PressAnyKeyToContinue();
    }
    
    private static void CancelTransaction()
    {
        var amountDeposited = _sodaMachinelogic!.GetMoneyInsertedTotal(userId!);
        _sodaMachinelogic!.IssueFullRefund(userId!);
        Console.WriteLine($"The transaction has been cancelled, please collect your credit: {amountDeposited}$");
        PressAnyKeyToContinue();
    }

    private static void RequestSoda()
    {
        // identify which soda the user wants
        List<SodaModel> sodas = _sodaMachinelogic!.ListTypesOfSoda();
        var i = 1;
        
        Console.WriteLine("Select soda from the machine:");
        sodas.ForEach(x => Console.WriteLine($"{i++ } - {x.Name}"));

        string? sodaIdentifier = Console.ReadLine();
        bool? isValidSodaIdentifier = int.TryParse(sodaIdentifier, out int sodaIndex);
        SodaModel? soda = new();

        try
        {
            soda = sodas[sodaIndex - 1];
        }
        catch (Exception)
        {
            PressAnyKeyToContinue();

            throw;
        }

        // request that soda
        var results = _sodaMachinelogic.RequestSoda(sodas[sodaIndex - 1], userId!);

        // handle the response
        if (results.errorMessage.Length > 0)
        {
            Console.WriteLine(results.errorMessage);
        }
        else 
        {
            Console.Clear();
            Console.WriteLine($"Please pick up your {results.soda.Name}");

            if (results.change.Count> 0)
            {
                Console.WriteLine($"Here is your change:");
                results.change.ForEach(x => Console.WriteLine($"{x.Name}"));
            }
            else
            { 
                Console.WriteLine("You used the exact amount to buy a soda, no need to refund.");
            }
        }

        PressAnyKeyToContinue();
    }

    private static void ListSodaOptions()
    {
        Console.Clear();
        Console.WriteLine("The soda options are:");
        var sodaOptions = _sodaMachinelogic!.ListTypesOfSoda();
        sodaOptions.ForEach(x => Console.WriteLine(x.Name));
        PressAnyKeyToContinue();
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

        return Console.ReadLine()!;
    }

    private static void RegisterServices() 
    {
        var collection = new ServiceCollection();
        
        IConfiguration? config = new ConfigurationBuilder()
            .AddJsonFile("appSettings.json", true, true)
            .Build();

        collection.AddSingleton<IConfiguration>(config);
        collection.AddTransient<IDataAccess, TextFileDataAccess>();
        collection.AddTransient<ISodaMachineLogic, SodaMachineLogic>();

        _serviceProvider = collection.BuildServiceProvider();
    }
}