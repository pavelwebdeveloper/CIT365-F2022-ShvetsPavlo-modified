using Microsoft.AspNetCore.Identity;

namespace MegaDesk.Areas.Identity.Data
{
    public class User : IdentityUser
    {
        [PersonalData]
        public string? Name { get; set; }
        [PersonalData]
        public DateTime DOB { get; set; }
    }
}