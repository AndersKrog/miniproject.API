using Microsoft.EntityFrameworkCore;
using miniproject.DAL.Database;
using miniproject.DAL.Models;
using miniproject.DAL.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace miniproject.TEST
{
    public class AuthorRepoTest
    {
        private List<Author> authorlist = DummyData.Authorlist;

        private readonly DbContextOptions<AbContext> _options;
        private readonly AbContext _context;
        private readonly AuthorRepo _authorRepo;
        public AuthorRepoTest()
            {
            _options = new DbContextOptionsBuilder<AbContext>()
                .UseInMemoryDatabase(databaseName: "MiniprojectAuthors").Options;

            _context = new AbContext(_options);

            _authorRepo = new AuthorRepo(_context);
            }

        [Fact]
        public async void getAuthorAsync_return_200() 
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            _context.Author.AddRange(authorlist);

            await _context.SaveChangesAsync();

            // Act
            var result = await _authorRepo.getAuthorsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Author>>(result);
            Assert.Equal(authorlist.Count, result.Count);
        }
    }
}
