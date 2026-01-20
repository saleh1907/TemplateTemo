using Microsoft.AspNetCore.Identity;

namespace Templetotemo101Saleh.Models;

public class AppUser :IdentityUser
{
    public string FullName { get; set; }=string.Empty;
}
