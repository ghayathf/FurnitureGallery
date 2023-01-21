using System;
using System.Collections.Generic;

#nullable disable

namespace FurnitureShop.Models
{
    public partial class FurnFavourate
    {
        public decimal Id { get; set; }
        public decimal? UserId { get; set; }
        public decimal? ProductId { get; set; }

        public virtual FurnProduct Product { get; set; }
        public virtual FurnUserAccount User { get; set; }
    }
}
