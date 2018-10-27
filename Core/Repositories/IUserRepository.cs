using MyCVonline.Core.Models;
using System.Collections.Generic;

namespace MyCVonline.Core.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<ApplicationUser> GetUsers();

        ApplicationUser GetUser();
        ApplicationUser GetUserById(string id);
        ApplicationUser GetUserByEmail(string email);
    }
}
