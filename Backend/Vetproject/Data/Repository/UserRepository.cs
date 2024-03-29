using Microsoft.EntityFrameworkCore;
using Vetproject.Model;

namespace Vetproject.Data.Repository;

public class UserRepository : IUserRepository
{
    private readonly UsersContext _usersContext;

    public UserRepository(UsersContext usersContext)
    {
        _usersContext = usersContext;
    }

    public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
    {
        return await _usersContext.Users.FirstOrDefaultAsync(user => user.Email == email);
    }

    public async Task DeleteUserAsync(ApplicationUser userToDelete)
    {
        _usersContext.Users.Remove(userToDelete);
        await _usersContext.SaveChangesAsync();
    }
    


}