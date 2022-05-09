using Microsoft.AspNetCore.Mvc.Infrastructure;
using miniproject.API.Controllers;
using miniproject.DAL.Models;
using miniproject.DAL.repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace miniproject.TEST
{
    public class AuthorsControllerTest
    {
        private List<Author> authorlist = new List<Author>()
            {
                new Author{Id = 1, Age = 12, Name="Hans", IsAlive=true, Password="1234"},
                new Author{Id = 2, Age = 12, Name="Gert", IsAlive=true, Password="1234"},
                new Author{Id = 3, Age = 12, Name="Gerda", IsAlive=true, Password="1234"},
                new Author{Id = 4, Age = 12, Name="Erik", IsAlive=true, Password="1234"}
            };

        // System under test
        private readonly AuthorsController _sut;
        private readonly Mock<IAuthorRepo> authorrepo = new Mock<IAuthorRepo>();

        public AuthorsControllerTest()
        {
            _sut = new AuthorsController(authorrepo.Object);
        }
        [Fact] // decorates a method in xunit
        public async void getAuthors_return200()
        {
            // Arrange
            authorrepo.Setup(x => x.getAuthorsAsync())
                    .ReturnsAsync(authorlist);
            // Act
            // should return 200
            var result = await _sut.GetAuthors();
            var status = (IStatusCodeActionResult)result;
            // Assert
            Assert.Equal(200, status.StatusCode);
        }
        [Fact] // decorates a method in xunit
        public async void getAuthors_return_500()
        {
            // Arrange
            authorrepo.Setup(x => x.getAuthorsAsync()).ReturnsAsync(()
                    => throw new Exception("This is an exception"));
            // Act
            // should return 200
            var result = await _sut.GetAuthors();
            var status = (IStatusCodeActionResult)result;
            // Assert
            Assert.Equal(500, status.StatusCode);
        }

        [Fact] // decorates a method in xunit
        public async void getAuthor_return200()
        {
            // Arrange
            authorrepo.Setup(x => x.getAuthorAsync(authorlist[0].Id))
                    .ReturnsAsync(authorlist[0]);

            // Act
            // should return 200
            var result = await _sut.GetAuthor(1);
            var status = (IStatusCodeActionResult)result;
            // Assert
            Assert.Equal(200, status.StatusCode);
        }
        [Fact]
        public async void getAuthor_return404()
        {
            // Arrange
            authorrepo.Setup(x => x.getAuthorAsync(authorlist[0].Id))
                    .ReturnsAsync(authorlist[0]);

            int not_existing_id = 2;

            // Act
            // should return 200
            var result = await _sut.GetAuthor(not_existing_id);
            var status = (IStatusCodeActionResult)result;
            // Assert
            Assert.Equal(404, status.StatusCode);
        }
        [Fact]
        public async void getAuthor_out_of_bounds_return404()
        {
            Author author = new Author { Id = 1, Age = 12, Name = "Hans", IsAlive = true, Password = "1234" };

            // Arrange
            authorrepo.Setup(x => x.getAuthorAsync(author.Id))
                    .ReturnsAsync(author);

            int not_existing_id = -5;

            // Act
            // should return 200
            var result = await _sut.GetAuthor(not_existing_id);
            var status = (IStatusCodeActionResult)result;
            // Assert
            Assert.Equal(404, status.StatusCode);
        }
        [Fact]
        public async void postAuthor_return200()
        {
            // Arrange
            authorrepo.Setup(x => x.createAuthorAsync(authorlist[0]))
                    .ReturnsAsync(authorlist[0]);


            // Act
            // should return 200
            var result = await _sut.PostAuthor(authorlist[0]);
            var status = (IStatusCodeActionResult)result;
            // Assert
            Assert.Equal(200, status.StatusCode);
        }
        [Fact]
        public async void postAuthor_return500()
        {
            Author empty_object = null;

            // Arrange
            authorrepo.Setup(x => x.createAuthorAsync(authorlist[0]))
                    .ReturnsAsync(empty_object);

            // Act
            // should return 500
            var result = await _sut.PostAuthor(authorlist[0]);
            var status = (IStatusCodeActionResult)result;
            // Assert
            Assert.Equal(500, status.StatusCode);
        }
        [Fact]
        public async void putAuthor_return200()
        {

            // Arrange
            authorrepo.Setup(x => x.createAuthorAsync(authorlist[0]))
                    .ReturnsAsync(authorlist[0]);

            // Act
            // should return 200
            var result = await _sut.PutAuthor(1, authorlist[0]);
            var status = (IStatusCodeActionResult)result;
            // Assert
            Assert.Equal(200, status.StatusCode);
        }
        [Fact]
        public async void putAuthor_return400()
        {
            // Arrange
            authorrepo.Setup(x => x.createAuthorAsync(authorlist[0]))
                    .ReturnsAsync(authorlist[0]);

            int wrong_id = 2;

            // Act
            // should return 200
            var result = await _sut.PutAuthor(wrong_id, authorlist[0]);
            var status = (IStatusCodeActionResult)result;
            // Assert
            Assert.Equal(400, status.StatusCode);
        }
        [Fact]
        public async void DeleteAuthor_return200()
        {
            int Id = authorlist[0].Id;

            // Arrange
            authorrepo.Setup(x => x.deleteAuthorAsync(authorlist[0].Id))
            .ReturnsAsync(authorlist[0]);

            // Act
            // should return 200
            var result = await _sut.DeleteAuthor(Id);
            var status = (IStatusCodeActionResult)result;
            // Assert
            Assert.Equal(200, status.StatusCode);
        }
        [Fact]
        public async void DeleteAuthor_return404()
        {
            int Id = authorlist[0].Id +1;

            // Arrange
            authorrepo.Setup(x => x.deleteAuthorAsync(authorlist[0].Id))
            .ReturnsAsync(authorlist[0]);

            // Act
            // should return 200
            var result = await _sut.DeleteAuthor(Id);
            var status = (IStatusCodeActionResult)result;
            // Assert
            Assert.Equal(404, status.StatusCode);
        }

    }
}
