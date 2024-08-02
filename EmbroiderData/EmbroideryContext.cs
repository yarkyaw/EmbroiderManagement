// Decompiled with JetBrains decompiler
// Type: EmbroideryData.EmbroideryContext
// Assembly: EmbroideryData, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85FC7BD1-C71A-46CF-AFDC-ECA6425FE3FE
// Assembly location: D:\RealProject\EmbroiderManagementSystem\EmbroiderManagement\obj\Release\netcoreapp3.1\PubTmp\Out\EmbroideryData.dll

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace EmbroideryData
{
  public class EmbroideryContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
  {
    public string CurrentUserId { get; set; }

    public EmbroideryContext(DbContextOptions<EmbroideryContext> options)
      : base((DbContextOptions) options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.Entity<ApplicationUser>().HasMany<IdentityUserClaim<string>>((Expression<Func<ApplicationUser, IEnumerable<IdentityUserClaim<string>>>>) (u => u.Claims)).WithOne((string) null).HasForeignKey((Expression<Func<IdentityUserClaim<string>, object>>) (c => c.UserId)).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
      modelBuilder.Entity<ApplicationUser>().HasMany<IdentityUserRole<string>>((Expression<Func<ApplicationUser, IEnumerable<IdentityUserRole<string>>>>) (u => u.Roles)).WithOne((string) null).HasForeignKey((Expression<Func<IdentityUserRole<string>, object>>) (r => r.UserId)).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
      modelBuilder.Entity<ApplicationRole>().HasKey((Expression<Func<ApplicationRole, object>>) (t => t.Id));
      modelBuilder.Entity<ApplicationRole>().Property<string>((Expression<Func<ApplicationRole, string>>) (t => t.Id)).HasMaxLength(36);
      modelBuilder.Entity<ApplicationRole>().HasMany<IdentityRoleClaim<string>>((Expression<Func<ApplicationRole, IEnumerable<IdentityRoleClaim<string>>>>) (r => r.Claims)).WithOne((string) null).HasForeignKey((Expression<Func<IdentityRoleClaim<string>, object>>) (c => c.RoleId)).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
      modelBuilder.Entity<ApplicationRole>().HasMany<IdentityUserRole<string>>((Expression<Func<ApplicationRole, IEnumerable<IdentityUserRole<string>>>>) (r => r.Users)).WithOne((string) null).HasForeignKey((Expression<Func<IdentityUserRole<string>, object>>) (r => r.RoleId)).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
      modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
  }
}
