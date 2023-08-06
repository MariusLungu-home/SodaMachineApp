using System;
using Xunit;
using SodaMachineLibrary.DataAccess;
using SodaMachineLibrary.Logic;
using SodaMachineLibrary.Models;
using SodaMachineLibrary.Tests.Mocks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace SodaMachineLibrary.Tests
{
    public class SodaMachineLogicTests_JSON
    {
        [Fact]
        public void AddToCoinInventory_ShouldWork()
        {
            JSONDataAccess da = new JSONDataAccess();
            SodaMachineLogic logic = new SodaMachineLogic(da);
            List<CoinModel> coins = new List<CoinModel>
            {
                new CoinModel{ Name = "quarter", Amount = 0.25M},
                new CoinModel{ Name = "quarter", Amount = 0.25M},
                new CoinModel{ Name = "dime", Amount = 0.1M},
            };

            logic.AddToCoinInventory(coins);

            int expected = 22;
            int actual = da.CoinInventory_GetAll().Where(x => x.Name == "quarter").Count();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddToSodaInventory_ShouldWork()
        {
            JSONDataAccess da = new JSONDataAccess();
            SodaMachineLogic logic = new SodaMachineLogic(da);

            List<SodaModel> sodas = new List<SodaModel>
            {
                new SodaModel{ Name = "Coke", SlotOccupied = "1"},
                new SodaModel{ Name = "Coke", SlotOccupied = "1"},
                new SodaModel{ Name = "RedBull", SlotOccupied = "5"}
            };

            int expected = da.SodaInventory_GetAll().Where(x => x.Name == "Coke").Count();
            
            logic.AddToSodaInventory(sodas);

            int actual = da.SodaInventory_GetAll().Where(x => x.Name == "Coke").Count();
            

            Assert.Equal(expected + 2, actual);
        }

        [Fact]
        public void EmptyMoneyFromMachine_ShouldWork()
        {
            JSONDataAccess da = new JSONDataAccess();
            SodaMachineLogic logic = new SodaMachineLogic(da);

            var (_, expected, _) = da.MachineInfo;
            decimal actual = 25.25M;
            
            Assert.Equal(expected, actual);
            
            logic.EmptyMoneyFromMachine();

            expected = 0;
            (_, actual, _) = da.MachineInfo;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetCoinInventory_ShouldWork()
        {
            JSONDataAccess da = new JSONDataAccess();
            SodaMachineLogic logic = new SodaMachineLogic(da);

            var coins = logic.GetCoinInventory();

            int expected = da.CoinInventory_GetAll().Where(x => x.Name == "Quarter").Count();
            int actual = coins.Where(x => x.Name == "Quarter").Count();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetCurrentIncome_ShouldWork()
        {
            JSONDataAccess da = new JSONDataAccess();
            SodaMachineLogic logic = new SodaMachineLogic(da);

            var (_, expected, _) = da.MachineInfo;
            decimal actual = logic.GetCurrentIncome();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetMoneyInsertedTotal_ShouldWork()
        {
            JSONDataAccess da = new JSONDataAccess();
            SodaMachineLogic logic = new SodaMachineLogic(da);

            decimal expected = 0.65M;
            da.UserCredit.Add("test", expected);

            decimal actual = logic.GetMoneyInsertedTotal("test");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetSodaInventory_ShouldWork()
        {
            JSONDataAccess da = new JSONDataAccess();
            SodaMachineLogic logic = new SodaMachineLogic(da);

            int actual = logic.GetSodaInventory().Count();
            int expected = 12;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetSodaPrice_ShouldWork()
        {
            JSONDataAccess da = new JSONDataAccess();
            SodaMachineLogic logic = new SodaMachineLogic(da);

            decimal expected = da.MachineInfo.sodaPrice;
            decimal actual = logic.GetSodaPrice();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetTotalIncome_ShouldWork()
        {
            JSONDataAccess da = new JSONDataAccess();
            SodaMachineLogic logic = new SodaMachineLogic(da);

            decimal expected = da.MachineInfo.totalIncome;
            decimal actual = logic.GetTotalIncome();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void IssueFullRefund_ShouldWork()
        {
            JSONDataAccess da = new JSONDataAccess();
            SodaMachineLogic logic = new SodaMachineLogic(da);
            string user = "test";

            da.UserCredit[user] = 0.65M;

            logic.IssueFullRefund(user);

            Assert.True(da.UserCredit[user] == 0);
        }

        [Fact]
        public void ListTypesOfSoda_ShouldWork()
        {
            JSONDataAccess da = new JSONDataAccess();
            SodaMachineLogic logic = new SodaMachineLogic(da);

            int expected = 4;
            int actual = logic.ListTypesOfSoda().Count();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MoneyInsert_SingleUserShouldHaveCorrectAmount()
        {
            JSONDataAccess da = new JSONDataAccess();
            SodaMachineLogic logic = new SodaMachineLogic(da);
            string user = "user1";
            decimal money = 1M;

            logic.MoneyInserted(user, money);

            decimal expected = 17M;
            decimal actual = da.UserCredit[user];

            Assert.Equal(expected, actual);

            logic.MoneyInserted(user, money);

            expected += money;
            actual = da.UserCredit[user];

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MoneyInsert_User2ShouldHaveCorrectAmount()
        {
            JSONDataAccess da = new JSONDataAccess();
            SodaMachineLogic logic = new SodaMachineLogic(da);
            string user1 = "user1";
            string user2 = "user2";
            decimal money = 0.25M;
            decimal user2Money = 0.10M;

            logic.MoneyInserted(user1, money);
            logic.MoneyInserted(user2, user2Money);
            logic.MoneyInserted(user1, money);

            decimal expected = 5.3M;
            decimal actual = da.UserCredit[user2];

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RequestSoda_ShouldReturnSodaWithNoChange()
        {
            JSONDataAccess da = new JSONDataAccess();
            SodaMachineLogic logic = new SodaMachineLogic(da);

            string user = "test";
            SodaModel expectedSoda = new SodaModel { Name = "Coke", SlotOccupied = "1" };
            var initialState = da.MachineInfo;

            da.UserCredit[user] = 0.75M;

            var results = logic.RequestSoda(expectedSoda, user);

            Assert.Equal(expectedSoda.Name, results.soda.Name);
            Assert.Equal(expectedSoda.SlotOccupied, results.soda.SlotOccupied);

            Assert.Equal(0, da.UserCredit[user]);

            Assert.Equal(initialState.cashOnHand + 0.75M, da.MachineInfo.cashOnHand);
            Assert.Equal(initialState.totalIncome + 0.75M, da.MachineInfo.totalIncome);

            Assert.True(string.IsNullOrWhiteSpace(results.errorMessage));

            Assert.True(results.change.Count() == 0);
        }

        [Fact]
        public void RequestSoda_ShouldSayNotEnoughMoney()
        {
            JSONDataAccess da = new JSONDataAccess();
            SodaMachineLogic logic = new SodaMachineLogic(da);

            string user = "test";
            SodaModel expectedSoda = new SodaModel { Name = "Coke", SlotOccupied = "1" };
            var initialState = da.MachineInfo;

            da.UserCredit[user] = 0.5M;

            var results = logic.RequestSoda(expectedSoda, user);

            Assert.Null(results.soda);

            Assert.Equal(0.5M, da.UserCredit[user]);

            Assert.Equal(initialState.cashOnHand, da.MachineInfo.cashOnHand);
            Assert.Equal(initialState.totalIncome, da.MachineInfo.totalIncome);

            Assert.False(string.IsNullOrWhiteSpace(results.errorMessage));

            Assert.True(results.change.Count() == 0);
        }

        [Fact]
        public void RequestSoda_ShouldSayOutOfStock()
        {
            JSONDataAccess da = new JSONDataAccess();
            SodaMachineLogic logic = new SodaMachineLogic(da);

            string user = "test";
            SodaModel expectedSoda = new SodaModel { Name = "Fanta", SlotOccupied = "4" };
            var initialState = da.MachineInfo;

            da.UserCredit[user] = 0.75M;

            var results = logic.RequestSoda(expectedSoda, user);

            Assert.Null(results.soda);

            Assert.Equal(0.75M, da.UserCredit[user]);

            Assert.Equal(initialState.cashOnHand, da.MachineInfo.cashOnHand);
            Assert.Equal(initialState.totalIncome, da.MachineInfo.totalIncome);

            Assert.False(string.IsNullOrWhiteSpace(results.errorMessage));

            Assert.True(results.change.Count() == 0);
        }

        [Fact]
        public void RequestSoda_ShouldSayNotEnoughChange()
        {
            JSONDataAccess da = new JSONDataAccess();
            SodaMachineLogic logic = new SodaMachineLogic(da);

            string user = "test";
            SodaModel expectedSoda = new SodaModel { Name = "Coke", SlotOccupied = "1" };
            var initialState = da.MachineInfo;

            da.UserCredit[user] = 1.75M;
            da.CoinInventory_GetAll().Clear();

            var results = logic.RequestSoda(expectedSoda, user);

            Assert.Null(results.soda);

            Assert.Equal(1.75M, da.UserCredit[user]);

            Assert.Equal(initialState.cashOnHand, da.MachineInfo.cashOnHand);
            Assert.Equal(initialState.totalIncome, da.MachineInfo.totalIncome);

            Assert.False(string.IsNullOrWhiteSpace(results.errorMessage));

            Assert.True(results.change.Count() == 0);
        }

        [Fact]
        public void RequestSoda_ShouldReturnSodaWithChange()
        {
            JSONDataAccess da = new JSONDataAccess();
            SodaMachineLogic logic = new SodaMachineLogic(da);

            string user = "test";
            SodaModel expectedSoda = new SodaModel { Name = "Coke", SlotOccupied = "1" };
            var initialState = da.MachineInfo;

            da.UserCredit[user] = 1M;

            var results = logic.RequestSoda(expectedSoda, user);

            Assert.Equal(expectedSoda.Name, results.soda.Name);
            Assert.Equal(expectedSoda.SlotOccupied, results.soda.SlotOccupied);

            Assert.Equal(0, da.UserCredit[user]);

            Assert.Equal(initialState.cashOnHand + 1M, da.MachineInfo.cashOnHand);
            Assert.Equal(initialState.totalIncome + 1M, da.MachineInfo.totalIncome);

            Assert.True(string.IsNullOrWhiteSpace(results.errorMessage));

            Assert.True(results.change.Count() == 1);
        }

        [Fact]
        public void RequestSoda_ShouldReturnSodaWithAlternateChange()
        {
            JSONDataAccess da = new JSONDataAccess();
            SodaMachineLogic logic = new SodaMachineLogic(da);

            string user = "test";
            SodaModel expectedSoda = new SodaModel { Name = "Coke", SlotOccupied = "1" };
            var initialState = da.MachineInfo;

            da.UserCredit[user] = 1M;
            var allCoins = da.CoinInventory_GetAll();
            da.CoinInventory_WithdrawCoins(0.25M, allCoins.Count);

            var results = logic.RequestSoda(expectedSoda, user);

            Assert.Equal(expectedSoda.Name, results.soda.Name);
            Assert.Equal(expectedSoda.SlotOccupied, results.soda.SlotOccupied);

            Assert.Equal(0, da.UserCredit[user]);

            Assert.Equal(initialState.cashOnHand + 1M, da.MachineInfo.cashOnHand);
            Assert.Equal(initialState.totalIncome + 1M, da.MachineInfo.totalIncome);

            Assert.True(string.IsNullOrWhiteSpace(results.errorMessage));

            // Dime + Dime + Nickle = Quarter
            Assert.True(results.change.Count() == 3);
        }
    }
}
