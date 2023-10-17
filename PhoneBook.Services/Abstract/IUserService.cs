namespace PhoneBook.Services.Abstract
{
    using PhoneBook.Core.request;
    using PhoneBook.Core.response;

    public interface IUserService
    {
        /// <summary>
        /// Kullanıcının bilgilerini veritabanından çeker.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<ResponseMessage> GetUserById(int id);

        /// <summary>
        /// Kullanıcıtı veritabanından siler
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<ResponseMessage> DeleteUser(int id);
        /// <summary>
        /// Kullanıcının bilgilerini günceller. 
        /// </summary>
        /// <param name="addUserRequest"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<ResponseMessage> UpdateUser(AddUserRequest addUserRequest, int id);
        /// <summary>
        /// Telefon rehberine kullanıcı ekler.
        /// </summary>
        /// <param name="addUserRequest"></param>
        /// <returns></returns>
        public Task<ResponseMessage> AddUser(AddUserRequest addUserRequest);
        /// <summary>
        /// Bütün kullanıcıları listeler.
        /// </summary>
        /// <returns></returns>
        public Task<ResponseMessage> GetAllUsers();

    }
}





