﻿using SodaMachineLibrary.Models;
using System.Xml.Serialization;

namespace SodaMachineLibrary.Logic;

public interface ISodaMachineLogic
{
    List<SodaModel> ListTypesOfSoda();

    // Takes in an amount of money and returns the amount of money inserted
    decimal MoneyInserted(decimal insertedAmount);
    
    // Get the price of a soda
    decimal GetSodaPrice(int sodaId);

    // SodaModel or null, List<Coint> Change, string ErrorMessage
    (SodaModel, List<CoinModel>, string) RequestSoda(SodaModel sodaModel);

    void IssueFullRefund();

    decimal GetMoneyInsertedTotal(decimal totalInsertedAmount);

    void AddToInventory(List<SodaModel> sodaModels);

    List<SodaModel> GetInventory();

    decimal EmptyMoneyFromMachine();

    List<CoinModel> GetCoinInventory();
    
    CoinModel AddCoinToInventory(List<CoinModel> coin);
    
    decimal GetCurrentIncome();

    decimal GetTotalIncome();
}
