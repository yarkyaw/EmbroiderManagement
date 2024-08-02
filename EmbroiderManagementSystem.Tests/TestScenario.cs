using System;
using EmbroideryData;
using EmbroideryService.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;


namespace EmbroiderManagementSystem.Tests;

public class TestScenario : IClassFixture<TestFixture>

{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IProductService _productService;
    public TestScenario(TestFixture fixture)
    {
        _userManager = fixture.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        _productService=fixture.ServiceProvider.GetRequiredService<IProductService>();
    }
    protected DbContextOptions<EmbroideryContext> ContextOptions { get; }

    [Fact]
    public async Task TestSeeding()
    {
        var adminUser=await _userManager.FindByNameAsync("admin");
        Assert.NotNull(adminUser);
    }

    [Fact]
    public async Task TestSaveGroup()
    {
        var adminUser = await _userManager.FindByNameAsync("admin");
        var gp = new ProductGroup
        {   
            GroupCode="gp1",
            Name = "GP1 Name",
            UpdatedOn = DateTimeOffset.Now,
            CreatedOn = DateTimeOffset.Now,
            CreatedBy=adminUser.Id,
            UpdatedBy=adminUser.Id
        };
        var result=await _productService.SaveGroupAsync(gp);
        Assert.True(result.Id>0);
    }
}