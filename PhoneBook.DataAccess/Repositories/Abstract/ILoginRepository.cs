using PhoneBook.Entities.Models;

namespace PhoneBook.DataAccess.Repositories.Abstract
{
    public interface ILoginRepository
    {
        /// <summary>
        /// Kullanıcı bilgilerini email ile veritabanından çeker.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Login GetUserByEmail(string email);

    }
}
