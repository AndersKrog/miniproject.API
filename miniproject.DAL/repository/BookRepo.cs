using Microsoft.EntityFrameworkCore;
using miniproject.DAL.Database;
using miniproject.DAL.Database.Models;
using miniproject.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miniproject.DAL.repository
{
    public interface IBookRepo
    {
        Task<List<Book>> getBooksAsync();
        Task<Book> getBookAsync(int Id);
        Task<Book> createBookAsync(Book book);
        Task<Book> updateBookAsync(int Id, Book book);
        Task<Book> deleteBookAsync(int Id);
        bool bookExists(int id);

    }
    public class BookRepo : IBookRepo
    {
        private readonly AbContext myContext;
        public BookRepo(AbContext _context)
        {
            myContext = _context;
        }
        public async Task<List<Book>> getBooksAsync()
        {
            List<Book> books = await myContext.Book.ToListAsync();
            return books;
        }
        public async Task<Book> getBookAsync(int Id)
        {
            Book book = await myContext.Book.FirstOrDefaultAsync(x => x.Id == Id);
            return book;
        }
        public async Task<Book> createBookAsync(Book book)
        {
            myContext.Book.Add(book);

            var result = await myContext.SaveChangesAsync();

            return result != 0 ? book : null;
        }
        public async Task<Book> updateBookAsync(int Id, Book book)
        {
            int result = 0;

            if (bookExists(Id) == true)
            {
                myContext.Entry(book).State = EntityState.Modified;
                result = await myContext.SaveChangesAsync();
            }
            return result == 0 ? null : book;
        }
        public async Task<Book> deleteBookAsync(int Id)
        {

            var book = await getBookAsync(Id);
            if (book != null)
            {
                myContext.Book.Remove(book);
                var result = await myContext.SaveChangesAsync();
                return result != 0 ? book : null;
            }
            else
            {
                return null;
            }
        }
        public bool bookExists(int id)
        {
            return myContext.Book.Any(e => e.Id == id);
        }
    }
}
