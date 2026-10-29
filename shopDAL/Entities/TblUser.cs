using System;
using System.Collections.Generic;
namespace shopDAL_EF;

public partial class TblUser
{
    public int UserId { get; set; }
    public string Login { get; set; } = null!;
    public byte[] PasswordHash { get; set; } = null!;
    public Guid PasswordSalt { get; set; }
    public string Role { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? Gender { get; set; }
    public string SecretWord { get; set; } = null!; 

    public virtual ICollection<TblBankCard> TblBankCards { get; set; } = new List<TblBankCard>();
}
