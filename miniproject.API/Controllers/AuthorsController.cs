using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using miniproject.DAL.Models;
using miniproject.DAL.repository;

namespace miniproject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepo repo;
        public AuthorsController(IAuthorRepo a) 
        {
            repo = a;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult> GetAuthors()
        {
            try
            {
                List<Author> results = await repo.getAuthorsAsync();

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
        // GET: api/Authors/5
        [HttpGet("{id:int:min(1)}")]
        public async Task<ActionResult> GetAuthor(int id)
        {
            var author = await repo.getAuthorAsync(id);

            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }
        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int:min(1)}")]
        public async Task<IActionResult> PutAuthor(int id, Author author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }
            var result = await repo.updateAuthorAsync(id,author);

            return result != null ? Ok(result) : NotFound();
        }

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostAuthor(Author author)
        {
            var result = await repo.createAuthorAsync(author);

            return result != null? Ok(author): StatusCode(StatusCodes.Status500InternalServerError);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var result = await repo.deleteAuthorAsync(id);
            return result != null ? Ok(result) : NotFound();
        }
        private bool AuthorExists(int id)
        {
            return repo.authorExists(id);
        }
    }
}
