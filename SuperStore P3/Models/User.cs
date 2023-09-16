using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    [Table("User")]
    public partial class User
    {
        [Key]
        public int UserId { get; set; }
        [Unicode(false)]
        public string Email { get; set; } = null!;
        [Unicode(false)]
        public string Password { get; set; } = null!;
    }
}
