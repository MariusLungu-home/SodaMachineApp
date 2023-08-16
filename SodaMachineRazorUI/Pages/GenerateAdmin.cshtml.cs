using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SodaMachineRazorUI.Pages
{
    [Authorize]
    public class GenerateAdminModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly string _adminRole = "Admin";
        private IConfiguration _config;

        public bool RoleCreated { get; set; }
        public string AdminId { get; set; }

        public GenerateAdminModel(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, IConfiguration config)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _config = config;
        }
        public async Task OnGet()
        {
            var roleExists = await _roleManager.RoleExistsAsync(_adminRole);
            
            if (roleExists == false)
            {
                await _roleManager.CreateAsync(new IdentityRole(_adminRole));
            }

            string userEmail = _config.GetValue<string>("AdminUser");
            if (string.IsNullOrEmpty(userEmail) != true)
            {
                var user = await _userManager.FindByEmailAsync(userEmail);

                if (user != null)
                {
                    await _userManager.AddToRoleAsync(user, _adminRole);
                    AdminId = user.Id;
                    RoleCreated = true;
                }
            }
        }
    }
}
