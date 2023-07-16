using Xunit;
using SodaMachineLibrary.DataAccess;
using SodaMachineLibrary.Logic;
using SodaMachineLibrary.Models;
using SodaMachineLibrary.Tests.Mocks;

namespace SodaMachineLibrary.Tests
{
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
                new CoinModel { Name = "Dime", Value = 0.10M },
                new CoinModel { Name = "Nickel", Value = 0.05M },
                new CoinModel { Name = "Penny", Value = 0.01M }
            };

            logic.AddCoinToInventory(coins);

            int expected = 5;
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

            logic.AddToInventory(sodas);
            int expected = 7;
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
    }
}