using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SodaMachineLibrary.Logic;
using SodaMachineLibrary.Models;

namespace SodaMachineRazorUI.Pages
{
    public class SodaMachineModel : PageModel
    {
        private ISodaMachineLogic _sodaMachinelogic;

        public decimal SodaPrice { get;set; }
        public decimal DepositedAmount { get; set; }
        public List<SodaModel> SodaOptions{ get; set; }

        [BindProperty]
        public decimal Deposit { get; set; }

        [BindProperty]
        public SodaModel SelectedSoda{ get; set; }

        [BindProperty(SupportsGet = true)]
        public string UserId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string OutputText { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ErrorMessage { get; set; }

        public SodaMachineModel(ISodaMachineLogic sodaMachineLogic)
        {
            _sodaMachinelogic = sodaMachineLogic;
        }

        public void OnGet()
        {
            if (string.IsNullOrWhiteSpace(UserId))
            {
                UserId = Guid.NewGuid().ToString();
            }

            SodaPrice = _sodaMachinelogic.GetSodaPrice();
            DepositedAmount = _sodaMachinelogic.GetMoneyInsertedTotal(UserId);
            SodaOptions = _sodaMachinelogic.ListTypesOfSoda();
        }

        //used for depositing money
        public IActionResult OnPost() 
        {
            if (Deposit > 0)
            {
                _sodaMachinelogic.MoneyInserted(UserId, Deposit);
            }

            return RedirectToPage(new { UserId });
        }

        //used for requesting soda
        public IActionResult OnPostSoda() 
        {
            var results = _sodaMachinelogic.RequestSoda(SelectedSoda, UserId);
            OutputText = string.Empty;

            if (results.errorMessage.Length > 0)
            {
                ErrorMessage = results.errorMessage;
            }
            else 
            {
                OutputText = $"Enjoy your {results.soda.Name}!";

                if (results.change.Count > 0)
                {
                    OutputText += $"<br><br>Don't forget your change:<br>";
                    results.change.ForEach(x => OutputText += $"{x.Name}<br>");
                }
                else 
                {
                    OutputText += "<br><br>There is no change to refund.";
                }
            }
            return RedirectToPage(new { UserId, ErrorMessage, OutputText });
        }

        //used for refunding money
        public IActionResult OnPostCancel()
        {
            DepositedAmount = _sodaMachinelogic.GetMoneyInsertedTotal(UserId);
            _sodaMachinelogic.IssueFullRefund(UserId);

            OutputText = $"You have been refunded {DepositedAmount}$.";

            return RedirectToPage(new { UserId, OutputText });
        }
    }
}