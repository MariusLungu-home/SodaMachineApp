using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SodaMachineLibrary.Logic;
using System.Data;

namespace SodaMachineRazorUI.Pages
{
    [Authorize(Roles = "Admin")]

    public class MachineIncome: PageModel
    {
        private readonly ISodaMachineLogic _sodaMachine;

        public decimal CurrentIncome { get; set; }
        public decimal TotalIncome { get; set; }

        public MachineIncome(ISodaMachineLogic sodaMachine)
        {
            _sodaMachine = sodaMachine;
        }

        public void OnGet()
        {
            CurrentIncome = _sodaMachine.GetCurrentIncome();
            TotalIncome = _sodaMachine.GetTotalIncome();
        }

        public IActionResult OnPost()
        {
            _sodaMachine.EmptyMoneyFromMachine();
            return RedirectToPage();
        }
    }
}
