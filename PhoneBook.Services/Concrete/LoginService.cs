using PhoneBook.Core.request;
using PhoneBook.Core.response;
using PhoneBook.DataAccess.Repositories.Concrete;
using PhoneBook.Entities.Models;
using PhoneBook.Services.Abstract;
using System.Net;

namespace PhoneBook.Services.Concrete
{
    public class LoginService : ILoginService
    {
        private readonly LoginRepository loginRepository;
        private readonly LoginValidator loginValidator;

        public LoginService(LoginRepository loginRepository, LoginValidator loginValidator)
        {
            this.loginRepository = loginRepository;
            this.loginValidator = loginValidator;
        }

        public async Task<ResponseMessage> Login(LoginRequest loginRequest)
        {
            // Gelen addUserRequest'in istenen formatta olup olmaddığının kontrolünü sağlar.
            var validationResult = await loginValidator.ValidateAsync(loginRequest);

            // validationResult'ın kontrolü sağlanır.
            if (!validationResult.IsValid)
            {
                return new ResponseMessage
                {
                    // İşlemin başarısız olduğunu gösterir.
                    Success = false,
                    // Statusun kodunu integer'a çevirip gösterir.
                    Status = (int)HttpStatusCode.BadRequest,
                    // İşlemin neden başarısız olduğunu açıklar.
                    Message = "validation errors!",
                    // İstenen şartları sağlamayan kısımları gösterir.
                    Data = validationResult.Errors
                };
            }

            // Varolan kullanıcının bilgisini email ile veritabanından çeker.
            Login existingUser = loginRepository.GetUserByEmail(loginRequest.Email);

            // exitingUser'ın null olup olmadığı kontrol edilir.
            if (existingUser == null)
            {
                return new ResponseMessage
                {
                    // İşlemin başarısız olduğunu gösterir.
                    Success = false,
                    // Statusun kodunu integer'a çevirip gösterir.
                    Status = (int)HttpStatusCode.BadRequest,
                    // İşlemin neden başarısız olduğunu açıklar.
                    Message = "email or password is not valid!",
                    // İşlemin başarısız olduğunu null olarak gösterir.
                    Data = null
                };
            }
            else
            {
                // Şifrelerin uyuşup uyuşmadığını kontrol eder.
                if (loginRequest.Password == existingUser.Password)
                {
                    return new ResponseMessage
                    {
                        // İşlemin başarılı olduğunu gösterir.
                        Success = true,
                        // Statusun kodunu integer'a çevirip gösterir.
                        Status = (int)HttpStatusCode.OK,
                        // İşlemin başarılı olduğunu mesaj olarak açıklar.
                        Message = "login is successfull!",
                        // İşlemin başarılı olduğunu null olarak gösterir.
                        Data = null
                    };
                }
            }

            return new ResponseMessage
            {
                // İşlemin başarısız olduğunu gösterir.
                Success = false,
                // Statusun kodunu integer'a çevirip gösterir.
                Status = (int)HttpStatusCode.BadRequest,
                // İşlemin neden başarısız olduğunu açıklar.
                Message = "email or password is not valid!",
                // İşlemin başarısız olduğunu null olarak gösterir.
                Data = null
            };
        }


    }
}





