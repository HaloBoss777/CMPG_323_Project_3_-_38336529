using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    public partial class OrderDetail
    {
        [Key]
        [Column("OrderDetailsID")]
        public short OrderDetailsId { get; set; }
        public short OrderId { get; set; }
        public short ProductId { get; set; }
        public int Quantity { get; set; }
        public int? Discount { get; set; }

        [ForeignKey("OrderId")]
        [InverseProperty("OrderDetails")]
        public virtual Order? Order { get; set; }
        [ForeignKey("ProductId")]
        [InverseProperty("OrderDetails")]
        public virtual Product? Product { get; set; }
    }
}
