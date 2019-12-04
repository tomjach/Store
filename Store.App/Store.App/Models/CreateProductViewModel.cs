using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Store.App.Models
{
    public class CreateProductViewModel
    {
        public string Name { get; set; }

        public int Price { get; set; }

        public int CategoryId { get; set; }

        public SelectList Categories { get; set; }
    }
}
