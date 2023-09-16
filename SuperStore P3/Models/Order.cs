using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Key]
        public short OrderId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime OrderDate { get; set; }
        public short CustomerId { get; set; }
        [StringLength(50)]
        public string? DeliveryAddress { get; set; }

        [ForeignKey("CustomerId")]
        [InverseProperty("Orders")]
        public virtual Customer Customer { get; set; } = null!;
        [InverseProperty("Order")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
