using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string OwnerUserId { get; set; }
                
        public IdentityUser OwnerUser { get; set; }

        public int CategoryId { get; set; }
                
        public Category Category { get; set; }

        public int Price { get; set; }
    }
}
