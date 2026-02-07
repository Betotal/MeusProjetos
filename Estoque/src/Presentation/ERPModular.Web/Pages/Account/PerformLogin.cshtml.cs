using ERPModular.Shared.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ERPModular.Web.Pages.Account;

public class PerformLoginModel : PageModel
{
    private readonly SignInManager<ERPUser> _signInManager;
    private readonly UserManager<ERPUser> _userManager;

    public PerformLoginModel(SignInManager<ERPUser> signInManager, UserManager<ERPUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<IActionResult> OnGetAsync(string email, string password, bool rememberMe)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            return RedirectToPage("/Account/Login");
        }

        var result = await _signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            return LocalRedirect("/");
        }

        return Redirect($"/Account/Login?error=InvalidLogin");
    }
}
