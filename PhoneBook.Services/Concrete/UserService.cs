using AutoMapper;
using PhoneBook.Core.DTOs;
using PhoneBook.Core.request;
using PhoneBook.Core.response;
using PhoneBook.DataAccess.Repositories.Concrete;
using PhoneBook.Entities.Models;
using PhoneBook.Services.Abstract;
using PhoneBook.Services.Helpers;
using System.Net;

namespace PhoneBook.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly LoginValidator loginValidator;
        private readonly AddUserValidator addUserValidator;
        private readonly IMapper mapper;
        private readonly UserRepository userRepository;
        private readonly PhoneNumberRepository phoneNumberRepository;
        private readonly LoginRepository loginRepository;
        private readonly NoteRepository noteRepository;
        private readonly UserIDGetter userIDGetter;

        public UserService(UserRepository userRepository, PhoneNumberRepository phoneNumberRepository, NoteRepository noteRepository, IMapper mapper, UserIDGetter userIDGetter, LoginRepository loginRepository, LoginValidator loginValidator, AddUserValidator addUserValidator)
        {
            this.userRepository = userRepository;
            this.phoneNumberRepository = phoneNumberRepository;
            this.noteRepository = noteRepository;
            this.loginRepository = loginRepository;
            this.mapper = mapper;
            this.userIDGetter = userIDGetter;
            this.loginValidator = loginValidator;
            this.addUserValidator = addUserValidator;
        }

        public UserService() { }

        public async Task<ResponseMessage> AddUser(AddUserRequest addUserRequest)
        {
            // Gelen addUserRequest'in istenen formatta olup olmaddığının kontrolünü sağlar.
            var validationResult = await addUserValidator.ValidateAsync(addUserRequest);

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
            // Gelen addUserRequest maplenerek user modeline dönüştürülür.
            User newUser = mapper.Map<User>(addUserRequest);
            // Kullanıcı veritabanına kaydedilir.
            userRepository.SaveUser(newUser);
            // Kullanıcı email bilgisiyle veritabanından çekilir.
            User existingUser = userRepository.GetUserByEmail(newUser.Email);
            // Kullanıcının id'si CreatedBy alanına yazılır.
            existingUser.CreatedBy = existingUser.UserID;
            // Kullanıcının id'si UpdatedBy alanına yazılır.
            existingUser.UpdatedBy = existingUser.UserID;
            // Kullanıcı güncel veriyle tekrar veritabanına kaydedilir.
            userRepository.SaveUser(existingUser);
            // Kullanıcının telefon numaraları ilgili tabloya kaydedilir.
            SavePhoneNumbers(addUserRequest, existingUser);
            // Kullanıcının notları ilgili tabloya kaydedilir.
            SaveNotes(addUserRequest, existingUser);

            return new ResponseMessage
            {
                // İşlemin başarılı olduğunu gösterir.
                Success = true,
                // Statusun kodunu integer'a çevirip gösterir.
                Status = (int)HttpStatusCode.OK,
                // İşlemin başarılı olduğunu mesaj olarak açıklar.
                Message = "user added!",
                // İşlemin başarılı olduğunu null olarak gösterir.
                Data = null
            };
        }

        public void SavePhoneNumbers(AddUserRequest addUserRequest, User user)
        {
            // Gelen telefon numaralari listede tutulur.
            List<Dictionary<dynamic, dynamic>> PhoneNumbers = addUserRequest.PhoneNumbers;

            // Telefon numaralarının ilgili kullanıcıya göre döngüyle veritabanına kaydedilmesi sağlanır.
            foreach (var phoneNumberEntry in PhoneNumbers)
            {
                foreach (var kvp in phoneNumberEntry)
                {
                    PhoneNumber phoneNumber = new PhoneNumber();
                    phoneNumber.PhoneNumberType = int.Parse(kvp.Key);
                    phoneNumber.Number = kvp.Value;
                    phoneNumber.User = user;
                    phoneNumber.UpdatedBy = user.UserID;
                    phoneNumber.CreatedBy = user.UserID;
                    phoneNumberRepository.SavePhoneNumber(phoneNumber);

                }
            }
        }

        public void SaveNotes(AddUserRequest addUserRequest, User user)
        {
            // Gelen notlar listede tutulur.
            List<Dictionary<dynamic, dynamic>> notes = addUserRequest.Notes;

            // Notların ilgili kullanıcıya göre döngüyle veritabanına kaydedilmesi sağlanır.
            foreach (var note in notes)
            {
                foreach (var kvp in note)
                {
                    Note newNote = new Note();
                    newNote.Description = kvp.Value;
                    newNote.User = user;
                    newNote.UpdatedBy = user.UserID;
                    newNote.CreatedBy = user.UserID;

                    noteRepository.SaveNote(newNote);

                }
            }

        }

        public async Task<ResponseMessage> DeleteUser(int id)
        {
            // Kullanıcının repositoryden aldığı verisini existingUser'a tanımlar.
            User existingUser = userRepository.GetUserById(id);

            // Kullanıcının silindi bilgisi IsDeleted alanında uygun şekilde belirtilir.
            existingUser.IsDeleted = 1;
            // Kullanıcının güncellenen veriyle beraber veritabanına kaydedilir.
            userRepository.SaveUser(existingUser);
            // Kullanıcıya ait bütün notlar veritabanından çekilir.
            List<Note> userNotes = noteRepository.GetAllNotesByUser(id);
            // Kullanıcı silindiği için her bir notun verisi de silindi olarak işaretlenir.
            foreach (var note in userNotes)
            {
                note.IsDeleted = 1;
            }

            // Kullanıcı veritabanına kaydedilir.
            noteRepository.SaveNote(userNotes);

            return new ResponseMessage
            {
                // İşlemin başarılı olduğunu gösterir.
                Success = true,
                // Statusun kodunu integer'a çevirip gösterir.
                Status = (int)HttpStatusCode.OK,
                // İşlemin başarılı olduğunu mesaj olarak açıklar.
                Message = "user deleted!",
                // dto'ya dönüştürülen veriyi alır.
                Data = null
            };
        }

        public async Task<ResponseMessage> UpdateUser(AddUserRequest addUserRequest, int id)
        {
            // Gelen addUserRequest maplenerek user modeline dönüştürülür.
            User existedUser = mapper.Map<User>(addUserRequest);
            // null gözüken id gerçek değerini alır.
            existedUser.UserID = id;
            // Güncellemeyi yapan kullanıcının id'si ilgili audit column'e kaydedilir.
            existedUser.UpdatedBy = id;
            // Kullanıcıya göre bütün notlar veritabanından çekilir
            List<Note> notes = noteRepository.GetAllNotesByUser(id);

            // Gelen güncel notlar notlar döngüyle veritabanındaki uygun kısımlara kaydedilir.
            foreach (var note in addUserRequest.Notes)
            {
                foreach (var kvp in note)
                {
                    foreach (var note1 in notes)
                    {
                        if (kvp.Key == "NoteID" && kvp.Value == note1.NoteID)
                        {
                            Note newNote = new Note();
                            newNote.NoteID = kvp.Key;
                            newNote.Description = kvp.Value;
                            newNote.User = existedUser;
                            newNote.UpdatedBy = existedUser.UserID;
                            newNote.CreatedBy = existedUser.UserID;

                            noteRepository.SaveNote(newNote);
                        }

                    }
                }
            }

            // Kullanıcı veritabanına kaydedilir.
            userRepository.SaveUser(existedUser);

            return new ResponseMessage
            {
                // İşlemin başarılı olduğunu gösterir.
                Success = true,
                // Statusun kodunu integer'a çevirip gösterir.
                Status = (int)HttpStatusCode.OK,
                // İşlemin başarılı olduğunu mesaj olarak açıklar.
                Message = "user updated!",
                // İşlemin başarılı olduğunu null olarak gösterir.
                Data = null
            };
        }

        public async Task<ResponseMessage> GetUserById(int id)
        {
            // Kullanıcının repositoryden aldığı verisini existingUser'a tanımlar.
            User existingUser = userRepository.GetUserById(id);
            // Kullanıcının bilgisini dto'ya aktarır.
            UserDto userDto = mapper.Map<UserDto>(existingUser);

            return new ResponseMessage
            {
                // İşlemin başarılı olduğunu gösterir.
                Success = true,
                // Statusun kodunu integer'a çevirip gösterir.
                Status = (int)HttpStatusCode.OK,
                // İşlemin başarılı olduğunu mesaj olarak açıklar.
                Message = "user found!",
                // dto'ya dönüştürülen veriyi alır.
                Data = userDto
            };
        }

        public async Task<ResponseMessage> GetAllUsers()
        {
            // Bütün kullanıcılar veritabanından çekilir.
            List<User> existingUsers = userRepository.GetAllUsers();
            // existingUser modeli dto modeline dönüştürülür.
            List<UserDto> userDtos = mapper.Map<List<UserDto>>(existingUsers);

            return new ResponseMessage
            {
                // İşlemin başarılı olduğunu gösterir.
                Success = true,
                // Statusun kodunu integer'a çevirip gösterir.
                Status = (int)HttpStatusCode.OK,
                // İşlemin başarılı olduğunu mesaj olarak açıklar.
                Message = "users found!",
                // dto'ya dönüştürülen veriyi alır.
                Data = userDtos
            };
        }
    }
}