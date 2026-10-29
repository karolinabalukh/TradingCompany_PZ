using System;
using System.Collections.Generic;
using System.Text;


namespace shopDTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Login { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Gender { get; set; }
        public string SecretWord { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}