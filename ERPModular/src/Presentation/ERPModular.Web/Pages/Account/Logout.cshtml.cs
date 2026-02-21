using ERPModular.Shared.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ERPModular.Web.Pages.Account;

public class LogoutModel : PageModel
{
    private readonly SignInManager<ERPUser> _signInManager;

    public LogoutModel(SignInManager<ERPUser> signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        await _signInManager.SignOutAsync();
        return LocalRedirect("/");
    }
}
