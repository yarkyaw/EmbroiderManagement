using EmbroideryData;
using EmbroideryRepo.Interfaces;
using EmbroideryRepo;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmbroideryService.Interface;
using GroupyService;

namespace EmbroiderManagementSystem.Tests
{
    public class TestFixture : IDisposable
    {
        public ServiceProvider ServiceProvider { get; private set; }
        public TestFixture() {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging();
            serviceCollection.AddDbContext<EmbroideryContext>(options =>
                options.UseSqlServer("Server=.;Database=Embroider_last;User Id=sa;Password=123456;", sqlOptions =>
                sqlOptions.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null)));

           serviceCollection.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<EmbroideryContext>()
                .AddDefaultTokenProviders();
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            serviceCollection.AddScoped(typeof(IAsyncRepository<>), typeof(Repository<>));
            serviceCollection.AddScoped<IProductService, ProductService>();


            ServiceProvider = serviceCollection.BuildServiceProvider();
            using (var scope = ServiceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<EmbroideryContext>();
            }

        }

        public void Dispose()
        {
            ServiceProvider?.Dispose();
        }

    }
}
