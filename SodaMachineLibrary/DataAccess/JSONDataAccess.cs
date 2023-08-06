using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SodaMachineLibrary.Models;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;

namespace SodaMachineLibrary.DataAccess
{
    public class JSONDataAccess : IDataAccess
    {
        public IConfiguration _config { get; }

        private readonly string _coinsFileLocation = "D:\\Dev\\SodaMachineApp\\SodaMachineLibrary\\DataAccess\\Storage\\CoinsStorage.json";
        private readonly string _sodasFileLocation = "D:\\Dev\\SodaMachineApp\\SodaMachineLibrary\\DataAccess\\Storage\\SodasStorage.json";
        private readonly string _machineInfoFileLocation = "D:\\Dev\\SodaMachineApp\\SodaMachineLibrary\\DataAccess\\Storage\\MachineInfoStorage.json";
        private readonly string _userInfoFileLocation = "D:\\Dev\\SodaMachineApp\\SodaMachineLibrary\\DataAccess\\Storage\\UserInfoStorage.json";

        public JSONDataAccess()
        {
            UserCredit = GetUserCredit();    
        }

        private Dictionary<string,decimal> GetUserCredit()
        {
            // read the JSON file
            var fileAsText = File.ReadAllText(_userInfoFileLocation, Encoding.UTF8);
            // convert the JSON to a list of CoinModel
            var users = JsonConvert.DeserializeObject<Dictionary<string, decimal>>(fileAsText);
            // return the list
            return users;
        }

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
            //read the JSON file
            var fileAsText = File.ReadAllText(_machineInfoFileLocation, Encoding.UTF8);
            // convert the JSON to MachineInfo
            var machineInfo = JsonConvert.DeserializeObject<MachineInfoModel>(fileAsText);
            // return the cash on hand
            return machineInfo.CashOnHand;
        }

        public decimal MachineInfo_EmptyCash()
        {
            //read the JSON file
            var fileAsText = File.ReadAllText(_machineInfoFileLocation, Encoding.UTF8);
            // convert the JSON to MachineInfo
            var machineInfo = JsonConvert.DeserializeObject<MachineInfoModel>(fileAsText);
            // set the cash on hand to 0
            machineInfo.SodaPrice = 0;
            machineInfo.CashOnHand = 0;
            machineInfo.TotalIncome = 0;
            var updatedJson = JsonConvert.SerializeObject(machineInfo);
            // update the JSON file
            File.WriteAllText(_machineInfoFileLocation, updatedJson);
            
            return machineInfo.CashOnHand;
        }

        public decimal MachineInfo_SodaPrice()
        {
            //read the JSON file
            var fileAsText = File.ReadAllText(_machineInfoFileLocation, Encoding.UTF8);
            // convert the JSON to MachineInfo
            var machineInfo = JsonConvert.DeserializeObject<MachineInfoModel>(fileAsText);
            // return the cash on hand
            return machineInfo.SodaPrice;
        }

        public decimal MachineInfo_TotalIncome()
        {
            //read the JSON file
            var fileAsText = File.ReadAllText(_machineInfoFileLocation, Encoding.UTF8);
            // convert the JSON to MachineInfo
            var machineInfo = JsonConvert.DeserializeObject<MachineInfoModel>(fileAsText);
            // return the cash on hand
            return machineInfo.TotalIncome;
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
            List<SodaModel> sodas = SodaInventory_GetAll().GroupBy(x => x.Name).Select(y=>y.First()).ToList();
            return sodas;
        }

        public void UserCredit_Clear(string userId)
        {
            // read the JSON file
            var fileAsText = File.ReadAllText(_userInfoFileLocation, Encoding.UTF8);
            // convert the JSON to a list of CoinModel
            UserCredit = JsonConvert.DeserializeObject<Dictionary<string, decimal>>(fileAsText);
            // return the list
            UserCredit[userId] = 0;
            // convert the list back to JSON
            var updatedJson = JsonConvert.SerializeObject(UserCredit);
            // update the JSON file
            File.WriteAllText(_userInfoFileLocation, updatedJson);
        }

        public void UserCredit_Deposit(string userId)
        {
            // read the machine JSON file
            var fileAsText = File.ReadAllText(_machineInfoFileLocation, Encoding.UTF8);
            // convert the JSON to a list of CoinModel
            var machineInfo = JsonConvert.DeserializeObject<Dictionary<string, decimal>>(fileAsText);

            // read the JSON file
        }

        public void UserCredit_Insert(string userId, decimal amount)
        {
            // read the JSON file
            var fileAsText = File.ReadAllText(_userInfoFileLocation, Encoding.UTF8);
            // convert the JSON to a list of CoinModel
            UserCredit = JsonConvert.DeserializeObject<Dictionary<string, decimal>>(fileAsText);
            // return the list
            UserCredit[userId] += amount;
            // convert the list back to JSON
            var updatedJson = JsonConvert.SerializeObject(UserCredit);
            // update the JSON file
            File.WriteAllText(_userInfoFileLocation, updatedJson);
        }

        public decimal UserCredit_Total(string userId)
        {
            var userCredit = UserCredit.Where(x => x.Key == userId).FirstOrDefault();
            return userCredit.Value;
        }
    }
}
