using shopDAL.Entities;
using shopDAL.Repositories;
using shopDAL_EF;
using shopDTO;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace shopBL
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public AuthService(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public bool Login(string login, string password)
        {
            var user = _userRepository.GetByLogin(login);
            if (user == null) return false;

            return _passwordHasher.Verify(password, user.PasswordSalt, user.PasswordHash);
        }

        public void RegisterGuest(UserDTO userDto, BankCardDTO cardDto, string password, string confirmPassword)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(userDto.Login)) errors.Add("Логін є обов'язковим.");
            if (string.IsNullOrWhiteSpace(userDto.FirstName)) errors.Add("Ім'я є обов'язковим.");
            if (string.IsNullOrWhiteSpace(userDto.LastName)) errors.Add("Прізвище є обов'язковим.");


            if (string.IsNullOrWhiteSpace(userDto.Email) || !Regex.IsMatch(userDto.Email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                errors.Add("Введіть коректний Email.");


            if (!string.IsNullOrWhiteSpace(userDto.Phone) && !Regex.IsMatch(userDto.Phone, @"^\+?\d{10,13}$"))
                errors.Add("Введіть коректний номер телефону (від 10 до 13 цифр).");

            if (string.IsNullOrWhiteSpace(userDto.SecretWord)) errors.Add("Ключове слово є обов'язковим.");

            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
                errors.Add("Пароль має містити мінімум 6 символів.");
            if (password != confirmPassword)
                errors.Add("Паролі не співпадають.");

            if (_userRepository.GetByLogin(userDto.Login) != null)
                errors.Add("Користувач з таким логіном вже існує.");


            if (string.IsNullOrWhiteSpace(cardDto.Number) || !Regex.IsMatch(cardDto.Number, @"^\d{16}$"))
                errors.Add("Невірний формат номеру картки (потрібно рівно 16 цифр).");

            if (errors.Count > 0)
                throw new BusinessRuleException(errors);

            var salt = Guid.NewGuid();
            var hash = _passwordHasher.Hash(password, salt);

            var entity = new TblUser
            {
                Login = userDto.Login,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                Phone = userDto.Phone,
                Address = userDto.Address,
                Gender = userDto.Gender,
                SecretWord = userDto.SecretWord,
                PasswordHash = hash,
                PasswordSalt = salt,
                Role = "Guest"
            };

            _userRepository.CreateUserWithCard(entity, cardDto.Number);
        }

        public bool RecoverPassword(string login, string secretWord, string newPassword)
        {
            var user = _userRepository.GetByLogin(login);

            if (user == null || user.SecretWord != secretWord)
                return false;

            if (string.IsNullOrWhiteSpace(newPassword) || newPassword.Length < 6)
                throw new BusinessRuleException("Новий пароль має містити мінімум 6 символів.");

            user.PasswordSalt = Guid.NewGuid();
            user.PasswordHash = _passwordHasher.Hash(newPassword, user.PasswordSalt);

            _userRepository.update(user); 
            return true;
        }
    }
}