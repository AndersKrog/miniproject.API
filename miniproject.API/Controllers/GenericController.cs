using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using miniproject.DAL.Models;
using miniproject.DAL.repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace miniproject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController : ControllerBase
    {
        private readonly IGenericRepo<Author> repo;
        public GenericController(IGenericRepo<Author> a)
        {
            repo = a;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthorsgeneric()
        {
            var results = await repo.getAsync();

            return Ok(results);
        }
        // GET: api/Authors/5
        [HttpGet("{id:int:min(1)}")]
        public async Task<ActionResult> GetAuthorgeneric(int id)
        {
            var author = await repo.getByIdAsync(id);

            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }
        [HttpPut]
        public async Task<IActionResult> PutAuthorgeneric(int id, Author author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }
            var result = await repo.updateAsync(id, author);

            return result != null ? Ok(result) : NotFound();
        }
        
        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostAuthorgeneric(Author author)
        {
            var result = await repo.createAsync(author);

            return result != null ? Ok(author) : StatusCode(StatusCodes.Status500InternalServerError);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> DeleteAuthorgeneric(int id)
        {
            var result = await repo.deleteAsync(id);
            return result != null ? Ok(result) : NotFound();
        }        
    }
}