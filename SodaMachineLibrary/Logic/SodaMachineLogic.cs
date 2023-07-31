using SodaMachineLibrary.DataAccess;
using SodaMachineLibrary.Models;

namespace SodaMachineLibrary.Logic;

public class SodaMachineLogic : ISodaMachineLogic
{
    public SodaMachineLogic(IDataAccess dataAccess)
    {
    }

    public void AddCoinToInventory(List<CoinModel> coins)
    {
        throw new NotImplementedException();
    }

    public void AddToInventory(List<SodaModel> sodaModels)
    {
        throw new NotImplementedException();
    }

    public decimal EmptyMoneyFromMachine()
    {
        throw new NotImplementedException();
    }

    public List<CoinModel> GetCoinInventory()
    {
        throw new NotImplementedException();
    }

    public decimal GetCurrentIncome()
    {
        throw new NotImplementedException();
    }

    public List<SodaModel> GetInventory()
    {
        throw new NotImplementedException();
    }

    public decimal GetMoneyInsertedTotal(string userId)
    {
        throw new NotImplementedException();
    }

    public decimal GetSodaPrice(int sodaId)
    {
        throw new NotImplementedException();
    }

    public decimal GetTotalIncome()
    {
        throw new NotImplementedException();
    }

    public void IssueFullRefund()
    {
        throw new NotImplementedException();
    }

    public List<SodaModel> ListTypesOfSoda()
    {
        throw new NotImplementedException();
    }

    public decimal MoneyInserted(decimal insertedAmount)
    {
        throw new NotImplementedException();
    }

    public (SodaModel, List<CoinModel>, string) RequestSoda(SodaModel sodaModel)
    {
        throw new NotImplementedException();
    }
}
