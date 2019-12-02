using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Contracts.V1.Requests
{
    public class PaginationRequest
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public PaginationRequest()
        {
            PageNumber = 1;
            PageSize = 3;
        }

        public PaginationRequest(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
