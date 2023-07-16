using SodaMachineLibrary.Models;

namespace SodaMachineLibrary.DataAccess
{
    public class DataAccess : IDataAccess
    {
        public void CoinInventory_AddCoin(List<CoinModel> coins)
        {
            throw new NotImplementedException();
        }

        public List<CoinModel> CoinInventory_GetAll()
        {
            throw new NotImplementedException();
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

        public void SodaInventory_AddSodas(List<SodaModel> sodaModel)
        {
            throw new NotImplementedException();
        }

        public List<SodaModel> SodaInventory_GetAll()
        {
            throw new NotImplementedException();
        }

        public SodaModel SodaInventory_GetSoda(SodaModel sodaModel)
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

        public decimal UserCredit_GetTotal(string userId)
        {
            throw new NotImplementedException();
        }

        public void UserCredit_Insert(string userId, decimal amount)
        {
            throw new NotImplementedException();
        }
    }
}
