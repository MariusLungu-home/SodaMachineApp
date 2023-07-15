using SodaMachine.Models;

namespace SodaMachine.Service;

public interface ISodaMachine
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
    void AddCoinToInventory(Coin coin);
    double GetCurrentIncome();
    double GetTotalIncome();

}
