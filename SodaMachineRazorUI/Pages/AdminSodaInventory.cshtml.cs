using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SodaMachineLibrary.Logic;
using SodaMachineLibrary.Models;
using System.Data;

namespace SodaMachineRazorUI.Pages
{
    [Authorize(Roles = "Admin")]
    public class AdminSodaInventoryModel : PageModel
    {
        private readonly ISodaMachineLogic _sodaMachine;

        public List<SodaModel> Sodas { get; set; }
        public List<SelectListItem> SodaOptions { get; set; }
        public List<SelectListItem> SlotOptions { get; set; }

        [BindProperty]
        public string SelectedSoda { get; set; }

        [BindProperty]
        public string SelectedSlot { get; set; }

        public AdminSodaInventoryModel(ISodaMachineLogic sodaMachine)
        {
            _sodaMachine = sodaMachine;
        }

        public void OnGet()
        {
            Sodas = _sodaMachine.GetSodaInventory().OrderBy(x => x.SlotOccupied).ToList();

            SodaOptions = Sodas
                .GroupBy(x => x.Name)
                .Select(x => new SelectListItem
                {
                    Value = x.Key,
                    Text = x.Key
                }).ToList();

            SlotOptions = Sodas
                .GroupBy(x => x.SlotOccupied)
                .Select(x => new SelectListItem
                {
                    Value = x.Key,
                    Text = x.Key
                }).ToList();
        }

        public IActionResult OnPost()
        {
            _sodaMachine.AddToSodaInventory(new List<SodaModel>
            {
                new SodaModel
                {
                    SlotOccupied = SelectedSlot,
                    Name = SelectedSoda
                }
            });

            return RedirectToPage();
        }
    }
}