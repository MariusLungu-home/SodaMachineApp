using SodaMachineLibrary.DataAccess;
using SodaMachineLibrary.Models;

namespace SodaMachineLibrary.Logic
{
    public class SodaMachineLogic : ISodaMachineLogic
    {
        private IDataAccess _db;
        public SodaMachineLogic(IDataAccess dataAccess)
        {
            _db = dataAccess;
        }

        public void AddToCoinInventory(List<CoinModel> coins)
        {
            try
            {
                _db.CoinInventory_AddCoin(coins);
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
                _db.SodaInventory_AddSodas(sodas);
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
                return _db.MachineInfo_EmptyCash();
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
                return _db.CoinInventory_GetAll();
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
                return _db.MachineInfo_CashOnHand();
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
                return _db.UserCredit_GetTotal(userId);
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
                return _db.SodaInventory_GetAll();
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
                return _db.MachineInfo_SodaPrice();
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
                return _db.MachineInfo_TotalIncome();
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
                _db.UserCredit_Clear(userId);
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
                return _db.SodaInventory_GetTypes();
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
                _db.UserCredit_Insert(userId, monetaryAmount);
                return monetaryAmount;
            }
            catch (Exception)
            {

                throw;
            }        
        }

        public (SodaModel soda, List<CoinModel> change, string errorMessage) RequestSoda(SodaModel soda, string userId)
        {
            (SodaModel soda, List<CoinModel> change, string errorMessage) output = (null, null, "An unexpected error occured");

            try
            {
                decimal userCredit = _db.UserCredit_GetTotal(userId);
                decimal sodaCost = _db.MachineInfo_SodaPrice();

                if (userCredit == sodaCost) //no change needed
                {
                    var sodaToReturn = _db.SodaInventory_GetSoda(soda);
                    output = (sodaToReturn, new List<CoinModel>(), string.Empty);
                }
                else if (userCredit > sodaCost) //change required
                {

                }
                else //not enough money
                {
                    output = (null, new List<CoinModel>(), "User did not provide enough change");
                }

                return output;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
