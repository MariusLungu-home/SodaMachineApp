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
        private readonly string _MachineInfoFileLocation = "D:\\Dev\\SodaMachineApp\\SodaMachineLibrary\\DataAccess\\Storage\\MachineInfoStorage.json";
        private readonly string _UserInfoFileLocation = "D:\\Dev\\SodaMachineApp\\SodaMachineLibrary\\DataAccess\\Storage\\UserInfoStorage.json";


        public (decimal sodaPrice, decimal cashOnHand, decimal totalIncome) MachineInfo { get; set; }
        public Dictionary<string, decimal> UserCredit { get; set; } = new Dictionary<string, decimal>();

        public void CoinInventory_AddCoins(List<CoinModel> coins)
        {
            try
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
            catch (Exception)
            {
                throw;
            }
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
            // read the JSON file
            var fileAsText = File.ReadAllText(_coinsFileLocation, Encoding.UTF8);
            // convert the JSON to a list of CoinModel
            var coinList = JsonConvert.DeserializeObject<List<CoinModel>>(fileAsText);
            // get the coins to withdraw
            var coinsToWithdraw = coinList.Where(x => x.Amount == coinValue).Take(quantity).ToList();
            // remove the coins from the list
            coinList.RemoveAll(x => x.Amount == coinValue);
            // convert the list back to JSON
            var updatedJson = JsonConvert.SerializeObject(coinList);
            // update the JSON file
            File.WriteAllText(_coinsFileLocation, updatedJson);
            // return the coins to withdraw
            return coinsToWithdraw;
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
            // read the JSON file
            var fileAsText = File.ReadAllText(_sodasFileLocation, Encoding.UTF8);
            // convert the JSON to a list of CoinModel
            var sodaList = JsonConvert.DeserializeObject<List<SodaModel>>(fileAsText);
            // add the new sodas to the list
            sodaList.AddRange(sodas);
            // convert the list back to JSON
            var updatedJson = JsonConvert.SerializeObject(sodaList);
            // update the JSON file
            File.WriteAllText(_sodasFileLocation, updatedJson);
        }

        public bool SodaInventory_CheckIfSodaInStock(SodaModel soda)
        {
            throw new NotImplementedException();
        }

        public List<SodaModel> SodaInventory_GetAll()
        {
            // read the JSON file
            var fileAsText = File.ReadAllText(_sodasFileLocation, Encoding.UTF8);
            // convert the JSON to a list of CoinModel
            var sodaList = JsonConvert.DeserializeObject<List<SodaModel>>(fileAsText);
            // return the list
            return sodaList ?? new List<SodaModel>();
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
