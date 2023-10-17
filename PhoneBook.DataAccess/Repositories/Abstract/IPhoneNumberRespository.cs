using PhoneBook.Entities.Models;

namespace PhoneBook.DataAccess.Repositories.Abstract
{
    public interface IPhoneNumberRepository
    {
        /// <summary>
        /// Gelen telefon numaras veritabanına kaydeder.
        /// </summary>
        /// <param name="phoneNumber"></param>
        void SavePhoneNumber(PhoneNumber phoneNumber);
    }
}
