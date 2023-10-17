using Microsoft.EntityFrameworkCore;
using PhoneBook.DataAccess.DBContext;
using PhoneBook.DataAccess.Repositories.Abstract;
using PhoneBook.Entities.Models;

namespace PhoneBook.DataAccess.Repositories.Concrete
{
    public class LoginRepository : ILoginRepository
    {
        private readonly ApplicationDbContext context;

        public LoginRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Login GetUserByEmail(string email)
        {
            return context.Logins
                .FirstOrDefault(u => u.Email == email);
        }

    }
}
