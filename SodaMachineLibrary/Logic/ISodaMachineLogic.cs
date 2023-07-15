using SodaMachineLibrary.Models;

namespace SodaMachineLibrary.Logic;

public interface ISodaMachineLogic
{
    List<Beverage> ListTypesOfSoda();
    double MoneyInserted(double insertedAmount);
    double GetSodaPrice(int sodaId);
    void RequestSoda(Beverage beverage);
    void IssueFullRefund();
    double GetMoneyInsertedTotal(double totalInsertedAmount);
    double CalculateChange();
    void EmptyMoneyFromMachine(double soldAmount);
    List<Coin> GetCoinInventory();
    Coin AddCoinToInventory(Coin coin);
    double GetCurrentIncome();
    double GetTotalIncome();
}
