using System;
using System.Collections.Generic;
using System.Text;

namespace shopDTO
{
    public class BankCardDTO
    {
        public int CardId { get; set; }
        public int UserId { get; set; }
        public string Number { get; set; } = null!;
    }
}