using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using miniproject.DAL.Database.Models;
using miniproject.DAL.repository;

namespace miniproject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepo repo;
        public BooksController(IBookRepo a)
        {
            repo = a;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult> GetBooks()
        {
            try
            {
                List<Book> results = await repo.getBooksAsync();

                if (results.Count == 0)
                {
                    return NoContent();
                }
                else if (results == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
                return Ok(results);
            }
            catch (Exception error)
            {
                return Problem(error.Message);
            }
        }
        // GET: api/Books/5
        [HttpGet("{id:int:min(1)}")]
        public async Task<ActionResult> GetBook(int id)
        {
            var book = await repo.getBookAsync(id);

            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }
        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int:min(1)}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }
            try
            {
                await repo.updateBookAsync(id, book);
            }
            catch (DbUpdateConcurrencyException)
            {
                return !BookExists(id) ? NotFound() : Ok(book);
            }
            return Ok(book);
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostBook(Book book)
        {
            var result = await repo.createBookAsync(book);

            return result != null ? Ok(book) : StatusCode(StatusCodes.Status500InternalServerError);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await repo.deleteBookAsync(id);
            return result != null ? Ok(result) : NotFound();
        }
        private bool BookExists(int id)
        {
            return repo.bookExists(id);
        }
    }
}
