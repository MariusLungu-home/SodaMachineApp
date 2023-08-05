using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SodaMachineLibrary.Models;
using System.Text;

namespace SodaMachineLibrary.DataAccess
{
    public class JSONDataAccess : IDataAccess
    {
        public IConfiguration _config { get; }

        private readonly string _coinsFileLocation = "D:\\Dev\\SodaMachineApp\\SodaMachineLibrary\\DataAccess\\Storage\\CoinsStorage.json";
        private readonly string _sodasFileLocation = "D:\\Dev\\SodaMachineApp\\SodaMachineLibrary\\DataAccess\\Storage\\SodasStorage.json";

        public (decimal sodaPrice, decimal cashOnHand, decimal totalIncome) MachineInfo { get; set; }
        public List<SodaModel> SodaInventory { get; set; } = new List<SodaModel>();
        public Dictionary<string, decimal> UserCredit { get; set; } = new Dictionary<string, decimal>();

        public void CoinInventory_AddCoins(List<CoinModel> coins)
        {
            // read the JSON file
            var fileAsText = File.ReadAllText(_coinsFileLocation, Encoding.UTF8);
            // convert the JSON to a list of CoinModel
            var coinList = JsonConvert.DeserializeObject<List<CoinModel>>(fileAsText);
            // add the new coins to the list
            coinList.AddRange(coins);
            // convert the list back to JSON
            var updatedJson = JsonConvert.SerializeObject(coinList);
            // update the JSON file
            File.WriteAllText(_coinsFileLocation, updatedJson);
        }

        public List<CoinModel> CoinInventory_GetAll()
        {
            // read the JSON file
            var fileAsText = File.ReadAllText(_coinsFileLocation, Encoding.UTF8);
            // convert the JSON to a list of CoinModel
            var coinList = JsonConvert.DeserializeObject<List<CoinModel>>(fileAsText);
            // return the list
            return coinList ?? new List<CoinModel>();
        }

        public List<CoinModel> CoinInventory_WithdrawCoins(decimal coinValue, int quantity)
        {
            throw new NotImplementedException();
        }

        public decimal MachineInfo_CashOnHand()
        {
            throw new NotImplementedException();
        }

        public decimal MachineInfo_EmptyCash()
        {
            throw new NotImplementedException();
        }

        public decimal MachineInfo_SodaPrice()
        {
            throw new NotImplementedException();
        }

        public decimal MachineInfo_TotalIncome()
        {
            throw new NotImplementedException();
        }

        public void SodaInventory_AddSodas(List<SodaModel> sodas)
        {
            throw new NotImplementedException();
        }

        public bool SodaInventory_CheckIfSodaInStock(SodaModel soda)
        {
            throw new NotImplementedException();
        }

        public List<SodaModel> SodaInventory_GetAll()
        {
            throw new NotImplementedException();
        }

        public SodaModel SodaInventory_GetSoda(SodaModel soda, decimal amount)
        {
            throw new NotImplementedException();
        }

        public List<SodaModel> SodaInventory_GetTypes()
        {
            throw new NotImplementedException();
        }

        public void UserCredit_Clear(string userId)
        {
            throw new NotImplementedException();
        }

        public void UserCredit_Deposit(string userId)
        {
            throw new NotImplementedException();
        }

        public void UserCredit_Insert(string userId, decimal amount)
        {
            throw new NotImplementedException();
        }

        public decimal UserCredit_Total(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
