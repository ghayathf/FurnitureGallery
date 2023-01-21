using System;
using System.Collections.Generic;

#nullable disable

namespace FurnitureShop.Models
{
    public partial class FurnRole
    {
        public FurnRole()
        {
            FurnUserAccounts = new HashSet<FurnUserAccount>();
        }

        public decimal Id { get; set; }
        public string Rolename { get; set; }

        public virtual ICollection<FurnUserAccount> FurnUserAccounts { get; set; }
    }
}
