using DAL.Model;
using EFLibCore;

namespace BookStoreApi.Controllers
{
    public class DBOperations
    {

        private BookStoreDbContext _context = new BookStoreDbContext();
        //The operations for log class can be used after implementing a new Logger class.
       /* public bool AddLog(Log _log)
        {
            try{
                _context.Log.Add(_log);
                 _context.SaveChanges();
            }
              catch (Exception exc)
            {
                return false;
            }
        }

        public List<Log> GetLogs()
        {   
            //fetch all books  from db
            List<Log> logs = new List<Log>();
            logs = _context.Log.OrderBy(m => m.LogId).ToList<Log>();
            return logs;
        }*/
        public bool AddModel(Book _book)
        {
            try
            {
                _context.Book.Add(_book);
                _context.SaveChanges();
                
                return true;
            }
            catch (Exception exc)
            {
                //logger.createLog("Error add operation: " + exc.Message + "\tStatus Code: " + BadRequest().StatusCode);
                return false;
            }
        }

        public List<Book> GetBooks()
        {   
            //fetch all cat profiles from db
            List<Book> books = new List<Book>();
            books = _context.Book.OrderBy(m => m.Id).ToList<Book>();
            return books;
        }

        public Book FindBook(string title = "", int id = 0)
        {
            Book? book = new Book();
            if (!string.IsNullOrEmpty(title))
                book = _context.Book.FirstOrDefault(m => m.Title == title);
            else if (id > 0)
            {
                book = _context.Book.FirstOrDefault(m => m.Id == id);
            }
            return book;
        }

        public bool DeleteModel(int id)
        {
            try
            {
                _context.Book.Remove(FindBook("", id));
                _context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                //logger.createLog("HATA " + exc.Message);
                return false;
            }
        }

    }
}