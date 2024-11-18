using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WebProject.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
  [Required]
  public override string UserName { get; set; } = default!;
}