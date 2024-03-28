using Microsoft.AspNetCore.Identity;

namespace Vetproject.Model;

public class ApplicationUser : IdentityUser
{
    public DateTime BirthDate { get; set; }
    public string Address { get; set; }

    public bool IsActive { get; set; }




    public ApplicationUser()
    {
    }

    public ApplicationUser(DateTime birthDate, string password, string address)
    {
        BirthDate = birthDate;
        Address = address;
        IsActive = true;
    }
}