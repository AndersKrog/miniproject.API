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
        Task<List<Book>> GetBooksAsync();
        Task<Book> getBookAsync(int Id);
        Task<Book> createBookAsync(Book book);
        Task<int> updateBookAsync(int Id, Book book);
        Task<int> deleteBookAsync(int Id);
        bool BookExists(int id);
    }
    public class BookRepo: IBookRepo
    {
        private readonly AbContext myContext;
        public BookRepo(AbContext _context)
        {
            myContext = _context;
        }
        public async Task<List<Book>> GetBooksAsync()
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
            await myContext.SaveChangesAsync();

            return book;
        }
        public async Task<int> updateBookAsync(int Id, Book book)
        {

            myContext.Entry(book).State = EntityState.Modified;

            return await myContext.SaveChangesAsync();
        }
        public async Task<int> deleteBookAsync(int Id)
        {

            var book = await myContext.Book.FindAsync(Id);

            myContext.Book.Remove(book);

            return await myContext.SaveChangesAsync();

        }
        public bool BookExists(int id)
        {
            return myContext.Book.Any(e => e.Id == id);
        }
    }
}
