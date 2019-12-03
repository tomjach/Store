using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Store.Contracts.V1;
using Store.Contracts.V1.Requests;
using Store.Contracts.V1.Responses;
using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Store.IntegrationTests
{
    public class CategoriesControllerTests : IntegrationTest
    {
        [Fact]
        public async Task GetAll_AsAnonymousUser_ReturnsUnauthorized()
        {           
            var response = await TestClient.GetAsync(ApiRoutes.Categories.GetAll);
            
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GetAll_AsAuthorizedUser_ReturnsCategories()
        {
            var fixture = new Fixture();
            await DbContext.Categories.AddRangeAsync(fixture.CreateMany<Category>(2));

            await DbContext.SaveChangesAsync();

            await LoginAsAdmin();

            var response = await TestClient.GetAsync(ApiRoutes.Categories.GetAll);
            var categories = await response.Content.ReadAsAsync<PagedResponse<CategoryResponse>>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(2, categories.Data.Count());

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            categories.Data.Count().Should().Be(2);
        }

        [Fact]
        public async Task Add_AsAdmin_ReturnsCreatedCategory()
        {
            await LoginAsAdmin();

            var newCategory = new CategoryRequest
            {
                Name = "Test"
            };

            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Categories.Add, newCategory);
            var createdCategory = await response.Content.ReadAsAsync<CategoryResponse>();

            var dbCategory = await DbContext.Categories.FindAsync(createdCategory.Id);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(newCategory.Name, createdCategory.Name);
            Assert.Equal(newCategory.Name, dbCategory.Name);
        }
    }
}
