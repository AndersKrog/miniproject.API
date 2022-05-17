using Microsoft.EntityFrameworkCore;
using miniproject.DAL.Database;
using miniproject.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miniproject.DAL.repository
{
    public interface IAuthorRepo
    {
        Task<List<Author>> getAuthorsAsync();
        Task<Author> getAuthorAsync(int Id);
        Task<Author> createAuthorAsync(Author author);
        Task<Author> updateAuthorAsync(int Id, Author author);
        Task<Author> deleteAuthorAsync(int Id);
        bool authorExists(int id);

    }
    public class AuthorRepo: IAuthorRepo
    {
        private readonly AbContext myContext;
        public AuthorRepo(AbContext _context)
        {
            myContext = _context;
        }
        public async Task<List<Author>> getAuthorsAsync()
        {
            //List<Author> authors = await myContext.Author.ToListAsync();
            
            List<Author> authors = await myContext.Author.Include(author =>
            author.Books).ToListAsync();
            
            return authors;
        }
        public async Task<Author> getAuthorAsync(int Id) 
        {
            Author author = await myContext.Author.FirstOrDefaultAsync(x => x.Id == Id);  
            return author; 
        }
        public async Task<Author> createAuthorAsync(Author author) 
        {
            myContext.Author.Add(author);

            var result = await myContext.SaveChangesAsync();

            return result != 0 ? author : null;
        }
        public async Task<Author> updateAuthorAsync(int Id, Author author) {
            int result = 0;

            if (authorExists(Id) == true)
            {
                myContext.Entry(author).State = EntityState.Modified;
                result = await myContext.SaveChangesAsync();
            }
            return result == 0 ? null : author;
        }
        public async Task<Author> deleteAuthorAsync(int Id) {

            var author = await getAuthorAsync(Id);
            if (author != null)
            {
                myContext.Author.Remove(author);
                var result = await myContext.SaveChangesAsync();
                return result != 0? author : null;
            }
            else
            {
                return null;
            }
        }
        public bool authorExists(int id)
        {
            return myContext.Author.Any(e => e.Id == id);
        }
    }
}
