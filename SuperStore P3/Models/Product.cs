using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Key]
        public short ProductId { get; set; }
        [StringLength(50)]
        public string ProductName { get; set; } = null!;
        [StringLength(50)]
        public string? ProductDescription { get; set; }
        public int? UnitsInStock { get; set; }

        [InverseProperty("Product")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
