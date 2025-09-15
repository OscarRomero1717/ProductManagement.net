using Catalog.QueriesService;
using Catalog.QueriesService._01.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Quala.ProductManagement.API.Controllers.Quala.ProductManagement.API.Controllers;
using Xunit;

namespace Catalog.Quala.ProductManagement.API.Test
{

    public class AuthControllerTest
    {
        [Fact]
        public async void GivenCredentialAreOk_WhenLoginIsCall_ThenReturnValidToken()
        {
            // Arrange
            var auth = new Mock<IAuthService>();
            auth.Setup(_ => _.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new AuthResultDto() { ExpiresIn = 1, IsAuthenticated = true, Token = "MoclToekn" });
            var logger = new Mock<ILogger<AuthController>>();
            var requestDto = new LoginRequestDto()
            { Username = "test", Password = "test" };

            // Act
            var uuthController = new AuthController(auth.Object, logger.Object);
            var result = await uuthController.Login(requestDto);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualItem = Assert.IsType<AuthResponseDto>(okObjectResult.Value);
            Assert.Equal("MoclToekn", actualItem.Token);

        }

        [Fact]
        public async void GivenCredentialAreNotOk_WhenLoginIsCall_ThenReturnUnauthorized()
        {
            // Arrange
            var auth = new Mock<IAuthService>();
            auth.Setup(_ => _.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new AuthResultDto() { IsAuthenticated = false, Message = "Error Au" });
            var logger = new Mock<ILogger<AuthController>>();
            var requestDto = new LoginRequestDto()
            { Username = "testgg", Password = "testgg" };

            // Act
            var uuthController = new AuthController(auth.Object, logger.Object);
            var result = await uuthController.Login(requestDto);

            // Assert
            var unauthorizeObjectResult = Assert.IsType<UnauthorizedObjectResult>(result.Result);
            Assert.NotNull(unauthorizeObjectResult.Value);


        }
    }
}