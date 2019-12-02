using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Store.Contracts.V1.Responses;
using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Helpers
{
    public class PaginationHelper
    {
        public static PagedResponse<T> CreateResponse<T>(PaginationFilter paginationFilter, IEnumerable<T> data, HttpContext httpConext, string url)
        {
            var response = new PagedResponse<T>(data);

            var absoluteUri = string.Concat(httpConext.Request.Scheme, "://", httpConext.Request.Host.ToUriComponent(), "/");

            var previousPage = QueryHelpers.AddQueryString(absoluteUri + url, "pageNumber", (paginationFilter.PageNumber - 1).ToString());
            previousPage = QueryHelpers.AddQueryString(previousPage, "pageSize", paginationFilter.PageSize.ToString());

            var nextPage = QueryHelpers.AddQueryString(absoluteUri + url, "pageNumber", (paginationFilter.PageNumber + 1).ToString());
            nextPage = QueryHelpers.AddQueryString(nextPage, "pageSize", paginationFilter.PageSize.ToString());

            response.PreviousPage = paginationFilter.PageNumber > 1 ? previousPage : null;
            response.NextPage = data.Any() ? nextPage : null;

            return response;
        }
    }
}
