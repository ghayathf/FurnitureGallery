using System;
using System.Collections.Generic;

#nullable disable

namespace FurnitureShop.Models
{
    public partial class FurnOrder
    {
        public FurnOrder()
        {
            FurnProductOrders = new HashSet<FurnProductOrder>();
        }

        public decimal Id { get; set; }
        public DateTime? Datee { get; set; }
        public decimal? UserId { get; set; }

        public virtual FurnUserAccount User { get; set; }
        public virtual ICollection<FurnProductOrder> FurnProductOrders { get; set; }
    }
}
