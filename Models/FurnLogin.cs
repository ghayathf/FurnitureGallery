using System;
using System.Collections.Generic;

#nullable disable

namespace FurnitureShop.Models
{
    public partial class FurnLogin
    {
        public decimal Id { get; set; }
        public string Username { get; set; }
        public string Passwordd { get; set; }
        public decimal? UserId { get; set; }

        public virtual FurnUserAccount User { get; set; }
    }
}
