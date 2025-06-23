using AutoFixture;
using FluentAssertions;
using LifeInsurance.Application.Common.Settings;
using LifeInsurance.Infrastructure.Identity;
using LifeInsurance.Infrastructure.Persistence;
using LifeInsurance.Infrastructure.Services;
using LifeInsurance.Tests.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace LifeInsurance.Tests.Services
{
    public class AuthServiceTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly Mock<AppIdentityDbContext> _dbContextMock;
        private readonly IOptions<JwtSettings> _jwtOptions;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            _fixture = new Fixture();

            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(
                userStoreMock.Object,
                Mock.Of<IOptions<IdentityOptions>>(),
                Mock.Of<IPasswordHasher<ApplicationUser>>(),
                Enumerable.Empty<IUserValidator<ApplicationUser>>(),
                Enumerable.Empty<IPasswordValidator<ApplicationUser>>(),
                Mock.Of<ILookupNormalizer>(),
                Mock.Of<IdentityErrorDescriber>(),
                Mock.Of<IServiceProvider>(),
                Mock.Of<ILogger<UserManager<ApplicationUser>>>()
            );

            _dbContextMock = new Mock<AppIdentityDbContext>();

            var jwtSettings = new JwtSettings
            {
                Secret = "THIS_IS_A_TEST_SECRET_1234567890",
                Issuer = "TestIssuer",
                Audience = "TestAudience",
                ExpirationMinutes = 60
            };

            _jwtOptions = Options.Create(jwtSettings);
            var fakeSignInManager = new FakeSignInManager(_userManagerMock.Object);
            _authService = new AuthService(_userManagerMock.Object, fakeSignInManager, _jwtOptions, _dbContextMock.Object);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnToken_WhenCredentialsAreValid()
        {
            // Arrange
            var email = _fixture.Create<string>();
            var password = _fixture.Create<string>();
            var user = _fixture.Build<ApplicationUser>().With(u=>u.Email, email).Without(u=>u.PasswordHash).Create();
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            var hashedPassword = passwordHasher.HashPassword(user, password);
            user.PasswordHash = hashedPassword;
            _userManagerMock.Setup(u => u.FindByEmailAsync(email)).ReturnsAsync(user);

            // Act
            var token = await _authService.LoginAsync(email, password);

            // Assert
            token.Should().NotBeNullOrWhiteSpace();
            _userManagerMock.Verify(u => u.FindByEmailAsync(email), Times.Once);
        }
    }
}
