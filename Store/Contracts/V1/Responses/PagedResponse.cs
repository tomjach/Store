using System.Collections.Generic;

namespace Store.Contracts.V1.Responses
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
