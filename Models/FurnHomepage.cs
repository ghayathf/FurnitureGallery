using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace FurnitureShop.Models
{
    public partial class FurnHomepage
    {
        public decimal Id { get; set; }
        public string ImagePath { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public string LogoPath { get; set; }
        [NotMapped]
        public IFormFile LogoFile { get; set; }
        public string Paragraph { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Text1 { get; set; }
    }
}
