using SodaMachineLibrary.Models;

namespace SodaMachineLibrary.Logic;

public interface ISodaMachineLogic
{
    List<Soda> ListTypesOfSoda();
    decimal MoneyInserted(decimal insertedAmount);
    decimal GetSodaPrice(int sodaId);
    void RequestSoda(Soda beverage);
    void IssueFullRefund();
    decimal GetMoneyInsertedTotal(decimal totalInsertedAmount);
    decimal CalculateChange();
    void EmptyMoneyFromMachine(decimal soldAmount);
    List<Coin> GetCoinInventory();
    Coin AddCoinToInventory(Coin coin);
    decimal GetCurrentIncome();
    decimal GetTotalIncome();
}
