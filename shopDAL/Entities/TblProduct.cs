using System;
using System.Collections.Generic;

namespace shopDAL.Entities;

public partial class TblProduct
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public decimal Price { get; set; }

    public int QuantityInStock { get; set; }

    public int CategoryId { get; set; }

    public int SupplierId { get; set; }

    public int BrandId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public virtual TblBrand Brand { get; set; } = null!;

    public virtual TblCategory Category { get; set; } = null!;

    public virtual TblSupplier Supplier { get; set; } = null!;
}
