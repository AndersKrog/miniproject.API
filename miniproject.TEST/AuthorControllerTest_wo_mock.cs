using Microsoft.AspNetCore.Mvc.Infrastructure;
using miniproject.API.Controllers;
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
    public class AuthorControllerTest_wo_mock
    {
        //https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-6.0

        // System under test
        private readonly AuthorsController _sut;

        //private readonly Mock<IAuthorRepo> authorrepo = new Mock<IAuthorRepo>();

        private readonly IAuthorRepo authorrepo;

        public AuthorControllerTest_wo_mock(IAuthorRepo _authorRepo)
        {
            authorrepo = _authorRepo;

            _sut = new AuthorsController(authorrepo);
        }
        
        [Fact] // decorates a method in xunit
        public async void getAuthors_wo_mock_return200()
        {
            // Arrange
            //authorrepo.Setup(x => x.getAuthorsAsync())
             //       .ReturnsAsync(authorlist);
            
            // Act
            // should return 200
            var result = await _sut.GetAuthors();
            var status = (IStatusCodeActionResult)result;
            // Assert
            Assert.Equal(200, status.StatusCode);
        }
        /*
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
        */
    }
}
