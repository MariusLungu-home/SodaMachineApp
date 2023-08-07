using Microsoft.Extensions.Configuration;
using SodaMachineLibrary.Models;
using System.IO;

namespace SodaMachineLibrary.DataAccess
{
    public class TextFileDataAccess : IDataAccess
    {
        private readonly IConfiguration _config;
        private string coinTextFile;
        private string sodaTextFile;
        private string machineInfoTextFile;
        private string userCreditTextFile;

        public TextFileDataAccess(IConfiguration config)
        {
            _config = config;
            coinTextFile = _config.GetValue<string>("TextFileStorage:Coins");
            sodaTextFile = _config.GetValue<string>("TextFileStorage:Soda");
            machineInfoTextFile = _config.GetValue<string>("TextFileStorage:MachineInfo");
            userCreditTextFile = _config.GetValue<string>("TextFileStorage:UserCredit");
        }

        private List<CoinModel> RetrieveCoins()
        {
            List<CoinModel> output = new List<CoinModel>();
            var lines = File.ReadAllLines(coinTextFile);
            try
            {
                foreach (var line in lines)
                {
                    var data = line.Split(',');
                    output.Add(new CoinModel
                    {
                        Name = data[0],
                        Amount = decimal.Parse(data[1])
                    });
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                throw new Exception("Missing data in the text file.", ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("The data in the Coin text file has been corrupted", ex);
            }
            catch
            {
                throw;
            }

            return output;
        }

        private void SaveCoins(List<CoinModel> coins)
        {
            File.WriteAllLines(coinTextFile, coins.Select(c => $"{c.Name},{c.Amount}"));
        }

        public void CoinInventory_AddCoins(List<CoinModel> coins)
        {
            coins.AddRange(RetrieveCoins());

            SaveCoins(coins);
        }

        public List<CoinModel> CoinInventory_GetAll()
        {
            return RetrieveCoins();
        }

        public List<CoinModel> CoinInventory_WithdrawCoins(decimal coinValue, int quantity)
        {
            var coins = RetrieveCoins();
            var coinsToReturn = coins.Where(x => x.Amount == coinValue).Take(quantity).ToList();

            coinsToReturn.ForEach(x => coins.Remove(x));
            SaveCoins(coins);

            return coinsToReturn;
        }

        private (decimal sodaPrice, decimal cashOnHand, decimal totalIncome) RetrieveMachineInfo()
        {
            (decimal sodaPrice, decimal cashOnHand, decimal totalIncome) output;
            var lines = File.ReadAllLines(machineInfoTextFile);

            try
            {
                output.sodaPrice = decimal.Parse(lines[0]);
                output.cashOnHand = decimal.Parse(lines[1]);
                output.totalIncome = decimal.Parse(lines[2]);
            }
            catch (IndexOutOfRangeException ex)
            {
                throw new Exception("Missing data in the Machine Info file.", ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("The data in the Machine Info text file has been corrupted", ex);
            }
            catch
            {
                throw;
            }
            return output;
        }

        private void SaveMachineInfo(decimal sodaPrice, decimal cashOnHand, decimal totalIncome)
        {
            string[] infoToSave = new string[] {
                    sodaPrice.ToString(),
                    cashOnHand.ToString(),
                    totalIncome.ToString()};

            File.WriteAllLines(machineInfoTextFile, infoToSave);
        }

        public decimal MachineInfo_CashOnHand()
        {
            var info = RetrieveMachineInfo();
            return info.cashOnHand;
        }

        public decimal MachineInfo_EmptyCash()
        {
            var info = RetrieveMachineInfo();

            SaveMachineInfo(info.sodaPrice, 0, info.totalIncome);

            return info.cashOnHand;
        }

        public decimal MachineInfo_SodaPrice()
        {
            var info = RetrieveMachineInfo();

            return info.sodaPrice;
        }

        public decimal MachineInfo_TotalIncome()
        {
            var info = RetrieveMachineInfo();

            return info.totalIncome;
        }

        public List<SodaModel> RetrieveSodas()
        {           
            List<SodaModel> output = new List<SodaModel>();
            var lines = File.ReadAllLines(sodaTextFile);

            try
            {
                foreach (var line in lines)
                {
                    var data = line.Split(',');
                    output.Add(new SodaModel
                    {
                        Name = data[0],
                        SlotOccupied = (data[1])
                    });
                }
            }

            catch (IndexOutOfRangeException ex)
            {
                throw new Exception("Missing data in the Soda text file.", ex);
            }

            catch
            {
                throw;
            }

            return output;
        }

        public void SaveSodas(List<SodaModel> sodas)
        {
            File.WriteAllLines(sodaTextFile, sodas.Select(s => $"{ s.Name },{ s.SlotOccupied }"));
        }

        public void SodaInventory_AddSodas(List<SodaModel> sodas)
        {
            sodas.AddRange(RetrieveSodas());
            SaveSodas(sodas);
        }

        public bool SodaInventory_CheckIfSodaInStock(SodaModel soda)
        {
            var sodas = RetrieveSodas();
            var sodaInStock = sodas.FirstOrDefault(x => x.Name == soda.Name && x.SlotOccupied == soda.SlotOccupied);
            
            return sodaInStock != null;
        }

        public List<SodaModel> SodaInventory_GetAll()
        {
            return RetrieveSodas();
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
