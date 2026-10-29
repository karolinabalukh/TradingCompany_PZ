using System;
using System.Collections.Generic;
using System.Text;
using shopDTO;

namespace shopBL
{
    public interface IAuthService
    {
        bool Login(string login, string password);
        void RegisterGuest(UserDTO userDto, BankCardDTO cardDto, string password, string confirmPassword);
        bool RecoverPassword(string login, string secretWord, string newPassword);
    }
}