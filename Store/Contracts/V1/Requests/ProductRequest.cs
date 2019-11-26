using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Contracts.V1.Requests
{
    public class ProductRequest
    {
        public string Name { get; set; }

        public int CategoryId { get; set; }

        public int Price { get; set; }
    }
}
