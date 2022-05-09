using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using miniproject.DAL.Database.Models;
using miniproject.DAL.repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace miniproject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepo context;
        public BookController(IBookRepo a)
        {
            context = a;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBook()
        {
            return await context.GetBooksAsync();
        }
        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await context.getBookAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }
        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }
            try
            {
                await context.updateBookAsync(id, book);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<Book> PostBook(Book book)
        {
            await context.createBookAsync(book);
            return book;
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteBook(int id)
        {

            var book = await context.getBookAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            await context.deleteBookAsync(id);

            return NoContent();
        }

        private bool BookExists(int id)
        {
            return context.BookExists(id);
        }
    }
}
