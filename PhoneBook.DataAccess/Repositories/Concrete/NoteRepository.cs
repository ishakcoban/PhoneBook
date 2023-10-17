using PhoneBook.DataAccess.DBContext;
using PhoneBook.DataAccess.Repositories.Abstract;
using PhoneBook.Entities.Models;
using static Azure.Core.HttpHeader;

namespace PhoneBook.DataAccess.Repositories.Concrete
{
    public class NoteRepository : INoteRepository
    {
        private readonly ApplicationDbContext context;

        public NoteRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Note GetNoteById(int noteID)
        {
            return context.Notes.Find(noteID);
        }

        public void SaveNote(Note note)
        {
            context.Notes.Update(note);
            context.SaveChanges();
        }

        public void SaveNote(List<Note> notes)
        {
            foreach (var note in notes)
            {
                context.Notes.Update(note);

            }

            context.SaveChanges();
        }
        public List<Note> GetAllNotesByUser(int id)
        {
            return context.Notes
                .Where(u => u.User.UserID == id)
                .ToList();

        }
        public void UpdateNote(Note note)
        {
            context.Notes.Update(note);
            context.SaveChanges();
        }

    }
}