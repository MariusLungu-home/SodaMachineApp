using SodaMachineLibrary.DataAccess;
using SodaMachineLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachineLibrary.Tests.Mocks;

public class MockDataAccess : IDataAccess
{
    #region Properties
    public List<CoinModel> CoinInventory { get; set; } = new List<CoinModel>();
    public (decimal sodaPrice, decimal cashOnHand, decimal totalIncome) MachineInfo { get; set; }
    public List<SodaModel> SodaInventory { get; set; } = new List<SodaModel>();
    public Dictionary<string, decimal> UserCredit { get; set; } = new Dictionary<string, decimal>();

    #endregion

    #region Constructor

    public MockDataAccess()
    {
        CoinInventory.Add(new CoinModel { Name = "Quarter", Value = 0.25M });
        CoinInventory.Add(new CoinModel { Name = "Quarter", Value = 0.25M });
        CoinInventory.Add(new CoinModel { Name = "Quarter", Value = 0.25M });
        CoinInventory.Add(new CoinModel { Name = "Quarter", Value = 0.25M });
        CoinInventory.Add(new CoinModel { Name = "Dime", Value = 0.1M });
        CoinInventory.Add(new CoinModel { Name = "Dime", Value = 0.1M });
        CoinInventory.Add(new CoinModel { Name = "Nickle", Value = 0.5M });
        CoinInventory.Add(new CoinModel { Name = "Nickle", Value = 0.5M });
        CoinInventory.Add(new CoinModel { Name = "Nickle", Value = 0.5M });
        CoinInventory.Add(new CoinModel { Name = "Nickle", Value = 0.5M });
        CoinInventory.Add(new CoinModel { Name = "Nickle", Value = 0.5M });
        CoinInventory.Add(new CoinModel { Name = "Nickle", Value = 0.5M });

        MachineInfo = (0.75M, 25.65M, 201.50M);

        SodaInventory.Add(new SodaModel { Name = "Cola", SlotOccupied = "1"});
        SodaInventory.Add(new SodaModel { Name = "Cola", SlotOccupied = "1" });
        SodaInventory.Add(new SodaModel { Name = "Cola", SlotOccupied = "1" });
        SodaInventory.Add(new SodaModel { Name = "Cola", SlotOccupied = "1" });
        SodaInventory.Add(new SodaModel { Name = "Cola", SlotOccupied = "1" });

        SodaInventory.Add(new SodaModel { Name = "Cola 0", SlotOccupied = "2" });

        SodaInventory.Add(new SodaModel { Name = "Sprite", SlotOccupied = "3" });
        SodaInventory.Add(new SodaModel { Name = "Sprite", SlotOccupied = "3" });
    }

    #endregion

    #region Methods

    public void CoinInventory_AddCoin(List<CoinModel> coins)
    {
        CoinInventory.AddRange(coins);
    }

    public List<CoinModel> CoinInventory_GetAll()
    {
        return CoinInventory;
    }

    public List<CoinModel> CoinInventory_WithdrawCoins(decimal coinValue, int quantity)
    {
        var coins = CoinInventory.Where(x => x.Value == coinValue).Take(quantity).ToList();

        coins.ForEach(x => CoinInventory.Remove(x));

        return coins;
    }

    public decimal MachineInfo_CashOnHand()
    {
        return MachineInfo.cashOnHand;
    }

    public decimal MachineInfo_EmptyCash()
    {
        var output = MachineInfo.cashOnHand;
        var machineinfo = MachineInfo;
        machineinfo.cashOnHand = 0;
        MachineInfo = machineinfo;

        return output;
    }

    public decimal MachineInfo_SodaPrice()
    {
        return MachineInfo.sodaPrice;
    }

    public decimal MachineInfo_TotalIncome()
    {
        return MachineInfo.totalIncome;
    }

    public void SodaInventory_AddSodas(List<SodaModel> sodas)
    {
        SodaInventory.AddRange(sodas);
    }

    public List<SodaModel> SodaInventory_GetAll()
    {
        return SodaInventory;
    }

    public SodaModel SodaInventory_GetSoda(SodaModel sodaModel)
    {
        return SodaInventory.FirstOrDefault(x => x.Name == sodaModel.Name);
    }

    public List<SodaModel> SodaInventory_GetTypes()
    {
        return SodaInventory.GroupBy(x => x.Name)
                            .Select(x => x.First()).ToList();
    }

    public void UserCredit_Clear(string userId)
    {
        if (UserCredit.ContainsKey(userId))
        {
            UserCredit[userId] = 0;
        }
    }

    public void UserCredit_Deposit(string userId)
    {
        if (UserCredit.ContainsKey(userId) == false)
        {
            throw new Exception("User not found for deposit!");
        }

        var info = MachineInfo;
        info.cashOnHand += UserCredit[userId];
        info.totalIncome += UserCredit[userId];

        UserCredit.Remove(userId);
    }

    public void UserCredit_Insert(string userId, decimal amount)
    {
        UserCredit.TryGetValue(userId, out decimal currentCredit);
        UserCredit[userId] = currentCredit + amount;
    }

    public decimal UserCredit_GetTotal(string userId)
    {
        UserCredit.TryGetValue(userId, out decimal currentCredit);
        return currentCredit;
    }

    #endregion
}
