using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Store.Contracts.V1;
using Store.Contracts.V1.Requests;
using Store.Contracts.V1.Responses;
using Store.Data;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Store.IntegrationTests
{
    public abstract class IntegrationTest
    {
        protected readonly HttpClient TestClient;
        protected readonly DataContext DbContext;
        private readonly IServiceScope serviceScope;

        protected IntegrationTest()
        {
            var factory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(DataContext));
                        services.AddDbContext<DataContext>(options =>
                        {
                            options.UseInMemoryDatabase("Store_Test");
                        });
                    });
                });

            TestClient = factory.CreateClient();

            serviceScope = factory.Server.Host.Services.CreateScope();
            DbContext = serviceScope.ServiceProvider.GetRequiredService<DataContext>();        
        }

        protected async Task LoginAsAdmin()
        {
            await SeedDatabase.SeedAsync(serviceScope);
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetAdminToken());
        }

        protected async Task LoginAsUser()
        {
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetUserToken());
        }

        private async Task<string> GetAdminToken()
        {
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Users.Login, new LoginUserRequest
            {
                UserName = "admin@test.pl",
                Password = "Start123!"
            });

            var authSuccess = await response.Content.ReadAsAsync<AuthSuccessResponse>();
            return authSuccess.Token;
        }

        private async Task<string> GetUserToken()
        {
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Users.Add, new NewUserRequest
            {
                Email = "test123@test.pl",
                Password = "Start123!"
            });

            var authSuccess = await response.Content.ReadAsAsync<AuthSuccessResponse>();
            return authSuccess.Token;
        }
    }
}
