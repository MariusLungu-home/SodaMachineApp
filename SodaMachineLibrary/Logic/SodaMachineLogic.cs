using SodaMachineLibrary.DataAccess;
using SodaMachineLibrary.Models;

namespace SodaMachineLibrary.Logic
{
    public class SodaMachineLogic : ISodaMachineLogic
    {
        private IDataAccess _dataAccess;
        public SodaMachineLogic(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public void AddToCoinInventory(List<CoinModel> coins)
        {
            try
            {
                _dataAccess.CoinInventory_AddCoin(coins);
            }
            catch (Exception)
            {
                throw;
            }       
        }

        public void AddToSodaInventory(List<SodaModel> sodas)
        {
            try
            {
                _dataAccess.SodaInventory_AddSodas(sodas);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public decimal EmptyMoneyFromMachine()
        {
            try
            {
                return _dataAccess.MachineInfo_EmptyCash();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<CoinModel> GetCoinInventory()
        {
            try
            {
                return _dataAccess.CoinInventory_GetAll();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public decimal GetCurrentIncome()
        {
            try
            {
                return _dataAccess.MachineInfo_CashOnHand();
            }
            catch (Exception)
            {
                throw;
            }        
        }

        public decimal GetMoneyInsertedTotal(string userId)
        {
            try
            {
                return _dataAccess.UserCredit_GetTotal(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SodaModel> GetSodaInventory()
        {
            try
            {
                return _dataAccess.SodaInventory_GetAll();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public decimal GetSodaPrice()
        {
            try
            {
                return _dataAccess.MachineInfo_SodaPrice();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public decimal GetTotalIncome()
        {
            try
            {
                return _dataAccess.MachineInfo_TotalIncome();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void IssueFullRefund(string userId)
        {
            try
            {
                _dataAccess.UserCredit_Clear(userId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<SodaModel> ListTypesOfSoda()
        {
            try
            {
                return _dataAccess.SodaInventory_GetTypes();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public decimal MoneyInserted(string userId, decimal monetaryAmount)
        {
            try
            {
                _dataAccess.UserCredit_Insert(userId, monetaryAmount);
                return monetaryAmount;
            }
            catch (Exception)
            {

                throw;
            }        
        }

        public (SodaModel soda, List<CoinModel> change, string errorMessage) RequestSoda(SodaModel soda)
        {
            try
            {
               var expectedSoda = _dataAccess.SodaInventory_GetSoda(soda);

               return (expectedSoda, new List<CoinModel>(), "");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
