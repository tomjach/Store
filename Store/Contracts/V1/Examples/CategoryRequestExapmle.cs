using Store.Contracts.V1.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace Store.Contracts.V1.Examples
{
    public class CategoryRequestExapmle : IExamplesProvider<CategoryRequest>
    {
        public CategoryRequest GetExamples()
        {
            return new CategoryRequest
            {
                Name = "Nowa nazwa"
            };
        }
    }
}
