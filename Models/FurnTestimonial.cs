using System;
using System.Collections.Generic;

#nullable disable

namespace FurnitureShop.Models
{
    public partial class FurnTestimonial
    {
        public decimal Id { get; set; }
        public string Message { get; set; }
        public decimal? UserId { get; set; }
        public string Status { get; set; }

        public virtual FurnUserAccount User { get; set; }
    }
}
