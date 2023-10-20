using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SodaMachineLibrary.Logic;
using SodaMachineLibrary.Models;

namespace SodaMachineRazorUI.Pages
{
    [Authorize(Roles = "Admin")]
    public class AdminCoinInventoryModel : PageModel
    {
        private readonly ISodaMachineLogic _sodaMachine;

        [BindProperty(SupportsGet = true)]
        public string OutputText { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ErrorMessage { get; set; }

        public List<CoinModel> Coins{ get; set; }

        [BindProperty]
        public string TotalIncome { get; set; }

        [BindProperty]
        public string CurrentIncome { get; set; }

        [BindProperty]
        public CoinModel Coin { get; set; }

        public AdminCoinInventoryModel(ISodaMachineLogic sodaMachine)
        {
            _sodaMachine = sodaMachine;
        }

        // Used to show coins
        public void OnGet()
        {
            Coins = _sodaMachine.GetCoinInventory().OrderBy(x=>x.Name).ToList();
            CurrentIncome = _sodaMachine.GetCurrentIncome().ToString();
            TotalIncome = _sodaMachine.GetTotalIncome().ToString();
        }

        // Used to deposit coins
        public IActionResult OnPost()
        {
            _sodaMachine.AddToCoinInventory(new List<CoinModel> { Coin });
            
            return RedirectToPage();
        }

        public IActionResult OnPostEmpty() 
        {
            _sodaMachine.EmptyMoneyFromMachine();

            return RedirectToPage();
        }
    }
}
