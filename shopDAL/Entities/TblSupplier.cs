using System;
using System.Collections.Generic;

namespace shopDAL.Entities;

public partial class TblSupplier
{
    public int SupplierId { get; set; }

    public string CompanyName { get; set; } = null!;

    public string? ContactPhone { get; set; }

    public virtual ICollection<TblProduct> TblProducts { get; set; } = new List<TblProduct>();
}
