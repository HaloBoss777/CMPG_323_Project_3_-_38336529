using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        public short CustomerId { get; set; }
        [StringLength(50)]
        public string? CustomerTitle { get; set; }
        [StringLength(50)]
        public string? CustomerName { get; set; }
        [StringLength(50)]
        public string? CustomerSurname { get; set; }
        [StringLength(50)]
        public string? CellPhone { get; set; }

        [InverseProperty("Customer")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
