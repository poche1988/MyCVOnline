using Microsoft.AspNet.Identity;
using MyCVonline.Core.Models;
using MyCVonline.Core.Repositories;
using MyCVonline.Persistence;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;

namespace MyCVonline.Repositories
{

    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<ApplicationUser> GetUsers()
        {
            return _context.Users;
        }

        public ApplicationUser GetUser()
        {
            var currentUserId = ClaimsPrincipal.Current.Identity.GetUserId();
            return _context.Users.Where(u => u.Id == currentUserId).FirstOrDefault();
        }

        public ApplicationUser GetUserById(string id)
        {
            return _context.Users.Where(u => u.Id == id).FirstOrDefault();
        }

        public ApplicationUser GetUserByEmail(string email)
        {
            return _context.Users.Where(u => u.Email == email).FirstOrDefault();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}