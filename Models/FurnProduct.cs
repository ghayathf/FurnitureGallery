using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace FurnitureShop.Models
{
    public partial class FurnProduct
    {
        public FurnProduct()
        {
            FurnFavourates = new HashSet<FurnFavourate>();
            FurnProductOrders = new HashSet<FurnProductOrder>();
        }

        public decimal Id { get; set; }
        public string Namee { get; set; }
        public decimal? Price { get; set; }
        public decimal? Valuee { get; set; }
        public string ImagePath { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public decimal? CategoryId { get; set; }

        public virtual FurnCategory Category { get; set; }
        public virtual ICollection<FurnFavourate> FurnFavourates { get; set; }
        public virtual ICollection<FurnProductOrder> FurnProductOrders { get; set; }
    }
}
