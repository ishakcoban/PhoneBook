using PhoneBook.Core.request;
using PhoneBook.Core.response;

namespace PhoneBook.Services.Abstract
{
    public interface ILoginService
    {
        /// <summary>
        /// Kullanıcının sisteme girişini kontrol etmek için kullanılır.
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        public Task<ResponseMessage> Login(LoginRequest loginRequest);
    }
}
