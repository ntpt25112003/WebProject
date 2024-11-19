using Microsoft.AspNetCore.Identity;
using WebProject.Data;

namespace WebProject.Components.Account;

internal sealed class IdentityUserAccessor(UserManager<ApplicationUser> userManager, IdentityRedirectManager redirectManager)
{
    public async Task<ApplicationUser> GetRequiredUserAsync(HttpContext context)
    {
        var user = await userManager.GetUserAsync(context.User);

        if (user is null)
        {
            redirectManager.RedirectToWithStatus("Account/InvalidUser", $"Error: Unable to load user with ID '{userManager.GetUserId(context.User)}'.", context);
        }

        return user;
    }

    public async Task<ApplicationUser> GetUserByUsernameAsync(string username)
    {
        var user = await userManager.FindByNameAsync(username);

        if (user is null)
        {
            throw new InvalidOperationException($"Unable to load user with username '{username}'.");
        }

        return user;
    }
}