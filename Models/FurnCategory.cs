using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace FurnitureShop.Models
{
    public partial class FurnCategory
    {
        public FurnCategory()
        {
            FurnProducts = new HashSet<FurnProduct>();
        }

        public decimal Id { get; set; }
        public string CategoryName { get; set; }
        public string Imagepath { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }

        public virtual ICollection<FurnProduct> FurnProducts { get; set; }
    }
}
