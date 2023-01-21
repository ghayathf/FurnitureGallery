using System;
using System.Collections.Generic;

#nullable disable

namespace FurnitureShop.Models
{
    public partial class FurnProductOrder
    {
        public decimal Id { get; set; }
        public string Status { get; set; }
        public decimal? ProductId { get; set; }
        public decimal? OrderId { get; set; }

        public virtual FurnOrder Order { get; set; }
        public virtual FurnProduct Product { get; set; }
    }
}
