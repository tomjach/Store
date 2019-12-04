using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.App.StoreApiContracts.Respones
{
    public class PagedResponse<T>
    {
        public IEnumerable<T> Data { get; set; }

        public string PreviousPage { get; set; }

        public string NextPage { get; set; }

        public PagedResponse(IEnumerable<T> data)
        {
            Data = data;
        }
    }
}
