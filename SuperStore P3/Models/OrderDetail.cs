using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Models
{
    public partial class OrderDetail
    {
        [DisplayName("Order Details ID")]
        public short OrderDetailsId { get; set; }
        [DisplayName("Order ID")]
        public short OrderId { get; set; }
        [DisplayName("Product ID")]
        public short ProductId { get; set; }
        [DisplayName("Quantity")]
        public int Quantity { get; set; }
        [DisplayName("Discount")]
        public double? Discount { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
