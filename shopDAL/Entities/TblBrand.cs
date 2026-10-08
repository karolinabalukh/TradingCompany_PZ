using System;
using System.Collections.Generic;

namespace shopDAL.Entities;

public partial class TblBrand
{
    public int BrandId { get; set; }

    public string BrandName { get; set; } = null!;

    public string? Country { get; set; }

    public virtual ICollection<TblProduct> TblProducts { get; set; } = new List<TblProduct>();
}
