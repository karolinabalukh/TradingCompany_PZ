using Moq;
using shopBL;
using shopDAL.Entities;
using shopDAL.Repositories;
using shopDAL_EF;
using shopDTO;
using System;
using Xunit;

namespace shopBL_Tests
{
    public class AuthServiceTests
    {
        private readonly Mock<IUserRepository> _mockRepo;
        private readonly Mock<IPasswordHasher> _mockHasher;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            _mockRepo = new Mock<IUserRepository>();
            _mockHasher = new Mock<IPasswordHasher>();

            _authService = new AuthService(_mockRepo.Object, _mockHasher.Object);
        }

        [Fact]
        public void Login_ValidCredentials_ReturnsTrue()
        {
            var salt = Guid.NewGuid();
            var expectedHash = new byte[] { 1, 2, 3 };

            _mockRepo.Setup(r => r.GetByLogin("testUser"))
                     .Returns(new TblUser { Login = "testUser", PasswordHash = expectedHash, PasswordSalt = salt });

            _mockHasher.Setup(h => h.Verify("correctPassword", salt, expectedHash))
                       .Returns(true);

            var result = _authService.Login("testUser", "correctPassword");
            Assert.True(result);
        }

        [Fact]
        public void Login_InvalidPassword_ReturnsFalse()
        {
            var salt = Guid.NewGuid();
            var expectedHash = new byte[] { 1, 2, 3 };

            _mockRepo.Setup(r => r.GetByLogin("testUser"))
                     .Returns(new TblUser { Login = "testUser", PasswordHash = expectedHash, PasswordSalt = salt });

            _mockHasher.Setup(h => h.Verify("wrongPassword", salt, expectedHash))
                       .Returns(false);

            var result = _authService.Login("testUser", "wrongPassword");
            Assert.False(result);
        }

        [Fact]
        public void RegisterGuest_ShortPassword_ThrowsBusinessRuleException()
        {
            var userDto = new UserDTO { Login = "newUser", FirstName = "Іван", LastName = "Іванов", Email = "test@test.com", SecretWord = "secret" };
            var cardDto = new BankCardDTO { Number = "1234567812345678" };

            var exception = Assert.Throws<BusinessRuleException>(() =>
                _authService.RegisterGuest(userDto, cardDto, "123", "123"));

            Assert.Contains("Пароль має містити мінімум 6 символів.", exception.Message);
        }

        [Fact]
        public void RegisterGuest_InvalidEmail_ThrowsBusinessRuleException()
        {
            var userDto = new UserDTO { Login = "newUser", FirstName = "Іван", LastName = "Іванов", Email = "bad-email", SecretWord = "secret" };
            var cardDto = new BankCardDTO { Number = "1234567812345678" };

            var exception = Assert.Throws<BusinessRuleException>(() =>
                _authService.RegisterGuest(userDto, cardDto, "Pass123", "Pass123"));

            Assert.Contains("Введіть коректний Email", exception.Message);
        }

        [Fact]
        public void RegisterGuest_ValidData_CallsCreateUser()
        {
            var userDto = new UserDTO { Login = "newUser", FirstName = "Іван", LastName = "Іванов", Email = "test@test.com", SecretWord = "secret" };
            var cardDto = new BankCardDTO { Number = "1234567812345678" };

            _mockRepo.Setup(r => r.GetByLogin("newUser")).Returns((TblUser)null);
            _mockHasher.Setup(h => h.Hash(It.IsAny<string>(), It.IsAny<Guid>())).Returns(new byte[] { 1, 2, 3 });

            _authService.RegisterGuest(userDto, cardDto, "Pass123", "Pass123");

            _mockRepo.Verify(r => r.CreateUserWithCard(It.IsAny<TblUser>(), "1234567812345678"), Times.Once);
        }

        [Fact]
        public void RecoverPassword_WrongSecretWord_ReturnsFalse()
        {
            var salt = Guid.NewGuid();
            var hash = new byte[] { 1, 2, 3 };
            var userInDb = new TblUser { Login = "testUser", SecretWord = "MySecret", PasswordHash = hash, PasswordSalt = salt };

            _mockRepo.Setup(r => r.GetByLogin("testUser")).Returns(userInDb);

            var result = _authService.RecoverPassword("testUser", "WrongSecret", "NewPass123");
            Assert.False(result);
        }

        [Fact]
        public void RecoverPassword_ValidData_UpdatesPasswordAndReturnsTrue()
        {
            var userInDb = new TblUser { Login = "testUser", SecretWord = "MySecret" };
            _mockRepo.Setup(r => r.GetByLogin("testUser")).Returns(userInDb);

            var result = _authService.RecoverPassword("testUser", "MySecret", "NewPass123");
            Assert.True(result);
            _mockRepo.Verify(r => r.update(It.IsAny<TblUser>()), Times.Once);
        }
    }
}