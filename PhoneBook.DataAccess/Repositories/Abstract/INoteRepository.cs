using Microsoft.EntityFrameworkCore;
using PhoneBook.Entities.Models;

namespace PhoneBook.DataAccess.Repositories.Abstract
{
    public interface INoteRepository
    {
        /// <summary>
        /// Not bilgsini id ile veritabanından çeker.
        /// </summary>
        /// <param name="noteID"></param>
        /// <returns></returns>
        Note GetNoteById(int noteID);
        /// <summary>
        /// Notu kaydeder.
        /// </summary>
        /// <param name="note"></param>
        public void SaveNote(Note note);
        /// <summary>
        /// Varolan notu günceller.
        /// </summary>
        /// <param name="note"></param>
        public void UpdateNote(Note note);
        /// <summary>
        /// Kullanıcının id'sine göre bütün notları getirir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Note> GetAllNotesByUser(int id);
        /// <summary>
        /// Gelen not listesini veritabanına kaydeder.
        /// </summary>
        /// <param name="note"></param>        
        public void SaveNote(List<Note> note);
    }
}
