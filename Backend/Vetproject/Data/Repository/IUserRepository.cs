using Vetproject.Model;

namespace Vetproject.Data.Repository;

public interface IUserRepository
{
    Task<ApplicationUser?> GetUserByEmailAsync(string email);
    Task DeleteUserAsync(ApplicationUser userToDelete);
    
}