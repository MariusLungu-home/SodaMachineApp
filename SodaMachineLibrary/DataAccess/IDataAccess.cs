using SodaMachineLibrary.Models;

namespace SodaMachineLibrary.DataAccess;

public interface IDataAccess
{
    List<SodaModel> SodaInventory_GetTypes();
    
    SodaModel SodaInventory_GetSoda(SodaModel sodaModel);
    
    void SodaInventory_AddSodas(List<SodaModel> sodaModel);

    List<SodaModel> SodaInventory_GetAll();

    void UserCredit_Insert(string userId, decimal amount);
    
    void UserCredit_Clear(string userId);

    void UserCredit_Deposit(string userId);

    decimal UserCredit_GetTotal(string userId);
    
    decimal MachineInfo_SodaPrice();

    decimal MachineInfo_EmptyCash();

    decimal MachineInfo_CashOnHand();

    decimal MachineInfo_TotalIncome();
    
    List<CoinModel> CoinInventory_WithdrawCoins(decimal coinValue, int quantity);

    List<CoinModel> CoinInventory_GetAll();

    void CoinInventory_AddCoin(List<CoinModel> coins);

}
