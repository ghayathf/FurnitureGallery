using System;
using System.Collections.Generic;

#nullable disable

namespace FurnitureShop.Models
{
    public partial class FurnPayment
    {
        public decimal Id { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? Paydate { get; set; }
        public decimal? UserId { get; set; }

        public virtual FurnUserAccount User { get; set; }
    }
}
