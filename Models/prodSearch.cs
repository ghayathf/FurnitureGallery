using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace FurnitureShop.Models
{
    public class prodSearch
    {
        public List<FurnProduct>? products { get; set; }
        public SelectList? Genres { get; set; }
        public string? prodGenre { get; set; }
        public string? SearchString { get; set; }
    }
}
