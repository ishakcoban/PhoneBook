using PhoneBook.Entities.Models;

namespace PhoneBook.DataAccess.Repositories.Abstract
{
    public interface IUserRepository
    {
        /// <summary>
        /// Kullanıcıyı id'ye göre veritabanından çeker.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        User GetUserById(int id);
        /// <summary>
        /// Kullanıcıyı email'e göre veritabanından çeker.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public User GetUserByEmail(string email);
        /// <summary>
        /// Bütün kullancıları listeler.
        /// </summary>
        /// <returns></returns>
        List<User> GetAllUsers();
        /// <summary>
        /// Kullanıcıyı veritabanına kaydeder.
        /// </summary>
        /// <param name="user"></param>
        void SaveUser(User user);

        /// <summary>
        /// Kullanıcıyı veritabanından siler.
        /// </summary>
        /// <param name="user"></param>
        public void DeleteUser(User user);
        /// <summary>
        /// Varolan kullanıcıyı günceller.
        /// </summary>
        /// <param name="user"></param>
        public void UpdateUser(User user);
    }

}