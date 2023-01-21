using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace FurnitureShop.Models
{
    public partial class FurnUserAccount
    {
        public FurnUserAccount()
        {
            FurnFavourates = new HashSet<FurnFavourate>();
            FurnLogins = new HashSet<FurnLogin>();
            FurnOrders = new HashSet<FurnOrder>();
            FurnPayments = new HashSet<FurnPayment>();
            FurnTestimonials = new HashSet<FurnTestimonial>();
        }

        public decimal Id { get; set; }
        public string Fullname { get; set; }
        public string Phone { get; set; }
        public string ImagePath { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public string Email { get; set; }
        public decimal? RoleId { get; set; }

        public virtual FurnRole Role { get; set; }
        public virtual ICollection<FurnFavourate> FurnFavourates { get; set; }
        public virtual ICollection<FurnLogin> FurnLogins { get; set; }
        public virtual ICollection<FurnOrder> FurnOrders { get; set; }
        public virtual ICollection<FurnPayment> FurnPayments { get; set; }
        public virtual ICollection<FurnTestimonial> FurnTestimonials { get; set; }
    }
}
