using Xunit;
using SodaMachineLibrary.DataAccess;
using SodaMachineLibrary.Logic;
using SodaMachineLibrary.Models;
using SodaMachineLibrary.Tests.Mocks;
using System.Xml.Serialization;

namespace SodaMachineLibrary.Tests;

public class SodaMachineLogicTests
{
    [Fact]
    public void AddCoinToInventory_ShouldWork()
    {
        MockDataAccess dataAccess = new();
        SodaMachineLogic logic = new SodaMachineLogic(dataAccess);

        List<CoinModel> coins = new List<CoinModel>
        {
            new CoinModel { Name = "Quarter", Value = 0.25M },
            new CoinModel { Name = "Quarter", Value = 0.25M },
            new CoinModel { Name = "Dime", Value = 0.1M },
        };

        logic.AddToCoinInventory(coins);

        int expected = 6;
        int actual = dataAccess.CoinInventory.Where(x => x.Name == "Quarter").Count();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void AddToSodaInventory_ShouldWork()
    {
        MockDataAccess dataAccess = new();
        SodaMachineLogic logic = new SodaMachineLogic(dataAccess);

        List<SodaModel> sodas = new List<SodaModel>
        {
            new SodaModel { Name = "Cola", Price = 1.00M },
            new SodaModel { Name = "Cola", Price = 1.00M }
        };

        logic.AddToSodaInventory(sodas);
        int expected = 10;
        int actual = dataAccess.SodaInventory.Count();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void EmptyMoneyFromMachine_ShouldWork()
    {
        MockDataAccess dataAccess = new();
        SodaMachineLogic logic = new SodaMachineLogic(dataAccess);

        var (_, expected, _) = dataAccess.MachineInfo;
        decimal actual = logic.EmptyMoneyFromMachine();

        Assert.Equal(expected, actual);
        expected = 0;

        (_, actual, _) = dataAccess.MachineInfo;
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetCoinInventory_ShouldWork()
    {
        MockDataAccess dataAccess = new();
        SodaMachineLogic logic = new SodaMachineLogic(dataAccess);

        List<CoinModel> expected = dataAccess.CoinInventory;
        List<CoinModel> actual = logic.GetCoinInventory();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetCurrentIncome_ShouldWork()
    {
        MockDataAccess dataAccess = new();
        SodaMachineLogic logic = new SodaMachineLogic(dataAccess);

        var (_, expected, _) = dataAccess.MachineInfo;
        decimal actual = logic.GetCurrentIncome();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetMoneyInsertedTotal_ShouldWork()
    {
        MockDataAccess dataAccess = new();
        SodaMachineLogic logic = new SodaMachineLogic(dataAccess);

        decimal expected = 1M;
        dataAccess.UserCredit.Add("test2", expected);
        decimal actual = logic.GetMoneyInsertedTotal("test2");

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetSodaInventory_ShouldWork()
    {
        MockDataAccess dataAccess = new MockDataAccess();
        SodaMachineLogic logic = new SodaMachineLogic(dataAccess);

        var actual = logic.GetSodaInventory().Count;
        var expected = dataAccess.SodaInventory.Count;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetSodaPrice_ShouldWork()
    {
        MockDataAccess dataAccess = new();
        SodaMachineLogic logic = new SodaMachineLogic(dataAccess);

        var (price, _, _) = dataAccess.MachineInfo;

        decimal expected = dataAccess.MachineInfo.sodaPrice;
        decimal actual = logic.GetSodaPrice();


        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetTotalIncome_ShouldWork()
    {
        MockDataAccess dataAccess = new();
        SodaMachineLogic logic = new SodaMachineLogic(dataAccess);

        decimal expected = dataAccess.MachineInfo.totalIncome;
        decimal actual = logic.GetTotalIncome();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void IssueFullRefund_ShouldWork()
    {
        MockDataAccess dataAccess = new();
        SodaMachineLogic logic = new SodaMachineLogic(dataAccess);

        string user = "test";
        dataAccess.UserCredit[user] = 0.65M;

        logic.IssueFullRefund(user);

        Assert.True(dataAccess.UserCredit[user] == 0);
    }

    [Fact]
    public void ListTypesOfSoda_ShouldWork()
    {
        MockDataAccess dataAccess = new();
        SodaMachineLogic logic = new SodaMachineLogic(dataAccess);

        // gets a list of distinct soda names
        var expected = 3;// dataAccess.SodaInventory.Distinct().Count;
        var actual = logic.ListTypesOfSoda().Count;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MoneyInsert_SingleUserShouldHaveCorrectAmount()
    {
        MockDataAccess dataAccess = new();
        SodaMachineLogic logic = new SodaMachineLogic(dataAccess);

        string user1 = "user1";
        decimal money = 0.25M;

        logic.MoneyInserted(user1, money);
        var expected = money;
        var actual = dataAccess.UserCredit[user1];

        Assert.Equal(expected, actual);

        logic.MoneyInserted(user1, money);
        expected += money;
        actual = dataAccess.UserCredit[user1];

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MoneyInserted_User2ShouldHaveCorrectAmount()
    {
        MockDataAccess dataAccess = new();
        SodaMachineLogic logic = new SodaMachineLogic(dataAccess);

        string user1 = "user1";
        string user2 = "user2";

        decimal user1money = 0.25M;
        decimal user2money = 0.10M;

        logic.MoneyInserted(user1, user1money);
        logic.MoneyInserted(user2, user2money);
        logic.MoneyInserted(user1, user1money);

        var expected = user2money;
        var actual = dataAccess.UserCredit[user2];

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void RequestSoda_ShouldReturnSodaWithNoChange()
    {
        MockDataAccess dataAccess = new();
        SodaMachineLogic logic = new SodaMachineLogic(dataAccess);

        string user = "test";
        SodaModel expectedSoda = new SodaModel { Name = "Cola", SlotOccupied = "1" };
        var machineInitialState = dataAccess.MachineInfo;

        dataAccess.UserCredit[user] = 1M;

        var results = logic.RequestSoda(expectedSoda, user);

        Assert.Equal(expectedSoda.Name, results.soda.Name);
        Assert.Equal(expectedSoda.SlotOccupied, results.soda.SlotOccupied);

        Assert.Equal(0, dataAccess.UserCredit[user]);

        Assert.Equal(machineInitialState.cashOnHand + 0.75M, dataAccess.MachineInfo.cashOnHand);
        Assert.Equal(machineInitialState.totalIncome + 0.75M, dataAccess.MachineInfo.totalIncome);

        Assert.True(string.IsNullOrWhiteSpace(results.errorMessage));

        Assert.True(results.change.Count() == 0);
    }

    [Fact]
    public void RequestSoda_ShouldSayNotEnoughMoney()
    {
        MockDataAccess dataAccess = new();
        SodaMachineLogic logic = new SodaMachineLogic(dataAccess);

        string user = "test";
        SodaModel expectedSoda = new SodaModel { Name = "Cola", SlotOccupied = "1" };
        var machineInitialState = dataAccess.MachineInfo;

        dataAccess.UserCredit[user] = 0.5M;

        var results = logic.RequestSoda(expectedSoda, user);

        Assert.Null(results.soda);

        Assert.Equal(0.5M, dataAccess.UserCredit[user]);

        Assert.Equal(machineInitialState.cashOnHand, dataAccess.MachineInfo.cashOnHand);
        Assert.Equal(machineInitialState.totalIncome, dataAccess.MachineInfo.totalIncome);

        Assert.False(string.IsNullOrWhiteSpace(results.errorMessage));

        Assert.True(results.change.Count() == 0);
    }

    [Fact]
    public void RequestSoda_ShouldSayOutOfStock()
    {
        MockDataAccess dataAccess = new();
        SodaMachineLogic logic = new SodaMachineLogic(dataAccess);

        string user = "test";
        SodaModel expectedSoda = new SodaModel { Name = "Fanta", SlotOccupied = "4" };
        var machineInitialState = dataAccess.MachineInfo;

        dataAccess.UserCredit[user] = 0.75M;

        var results = logic.RequestSoda(expectedSoda, user);

        Assert.Null(results.soda);

        Assert.Equal(0.75M, dataAccess.UserCredit[user]);

        Assert.Equal(machineInitialState.cashOnHand, dataAccess.MachineInfo.cashOnHand);
        Assert.Equal(machineInitialState.totalIncome, dataAccess.MachineInfo.totalIncome);

        Assert.False(string.IsNullOrWhiteSpace(results.errorMessage));

        Assert.True(results.change.Count() == 0);
    }

    [Fact]
    public void RequestSoda_ShouldReturnSodaWithChange()
    {
        MockDataAccess dataAccess = new();
        SodaMachineLogic logic = new SodaMachineLogic(dataAccess);

        string user = "test";
        SodaModel expectedSoda = new SodaModel { Name = "Cola", SlotOccupied = "1" };
        var machineInitialState = dataAccess.MachineInfo;

        dataAccess.UserCredit[user] = 1M;

        var results = logic.RequestSoda(expectedSoda, user);

        Assert.Equal(expectedSoda.Name, results.soda.Name);
        Assert.Equal(expectedSoda.SlotOccupied, results.soda.SlotOccupied);

        Assert.Equal(0, dataAccess.UserCredit[user]);

        Assert.Equal(machineInitialState.cashOnHand + 1M, dataAccess.MachineInfo.cashOnHand);
        Assert.Equal(machineInitialState.totalIncome + 1M, dataAccess.MachineInfo.totalIncome);

        Assert.True(string.IsNullOrWhiteSpace(results.errorMessage));

        Assert.True(results.change.Count() == 1);
    }

    [Fact]
    public void RequestSoda_ShouldReturnSodaWithAlternateChange()
    {
        MockDataAccess dataAccess = new();
        SodaMachineLogic logic = new SodaMachineLogic(dataAccess);

        string user = "test";
        SodaModel expectedSoda = new SodaModel { Name = "Cola", SlotOccupied = "1" };
        var machineInitialState = dataAccess.MachineInfo;

        dataAccess.UserCredit[user] = 1M;
        dataAccess.CoinInventory.RemoveAll(x => x.Value == 0.25M);

        var results = logic.RequestSoda(expectedSoda, user);

        Assert.Equal(expectedSoda.Name, results.soda.Name);
        Assert.Equal(expectedSoda.SlotOccupied, results.soda.SlotOccupied);

        Assert.Equal(0, dataAccess.UserCredit[user]);

        Assert.Equal(machineInitialState.cashOnHand + 1M, dataAccess.MachineInfo.cashOnHand);
        Assert.Equal(machineInitialState.totalIncome + 1M, dataAccess.MachineInfo.totalIncome);

        Assert.True(string.IsNullOrWhiteSpace(results.errorMessage));

        // Dime + Dime + Nickel = 0.20 + 0.05 = 0.25
        Assert.True(results.change.Count() == 3);
    }

}