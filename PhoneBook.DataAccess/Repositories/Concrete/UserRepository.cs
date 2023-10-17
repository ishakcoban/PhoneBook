using Microsoft.EntityFrameworkCore;
using PhoneBook.DataAccess.DBContext;
using PhoneBook.DataAccess.Repositories.Abstract;
using PhoneBook.Entities.Models;

namespace PhoneBook.DataAccess.Repositories.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public User GetUserByEmail(string email)
        {
            return context.Users
                .FirstOrDefault(u => u.Email == email);
        }
        public User GetUserById(int id)
        {
            return context.Users
            .Include(n => n.Notes)
            .Include(user => user.PhoneNumbers)
            .FirstOrDefault(u => u.UserID == id);
        }

        public List<User> GetAllUsers()
        {
            return context.Users
             .Include(n => n.Notes)
             .Include(user => user.PhoneNumbers)
            .ToList();
        }

        public void SaveUser(User user)
        {
            context.Users.Update(user);
            context.SaveChanges();
        }

        public void DeleteUser(User user)
        {
            var notes = context.Notes.Where(n => n.User.UserID == user.UserID).ToList();

            foreach (var note in notes)
            {
                note.IsDeleted = 1;
            }

            context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }


    }
}



