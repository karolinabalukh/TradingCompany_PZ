using System;
using System.Collections.Generic;

namespace shopDAL_EF;

public partial class TblBankCard
{
    public int CardId { get; set; }

    public int UserId { get; set; }

    public string Number { get; set; } = null!;

    public virtual TblUser User { get; set; } = null!;
}
