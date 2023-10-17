using Microsoft.EntityFrameworkCore;
using PhoneBook.DataAccess.DBContext;
using PhoneBook.DataAccess.Repositories.Abstract;
using PhoneBook.Entities.Models;

namespace PhoneBook.DataAccess.Repositories.Concrete
{
    public class PhoneNumberRepository : IPhoneNumberRepository
    {
        private readonly ApplicationDbContext context;

        public PhoneNumberRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void SavePhoneNumber(PhoneNumber phoneNumber)
        {
            context.PhoneNumbers.Add(phoneNumber);
            context.SaveChanges();
        }
    }
}
