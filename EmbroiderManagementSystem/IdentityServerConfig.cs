// Decompiled with JetBrains decompiler
// Type: EmbroiderManagementSystem.IdentityServerConfig
// Assembly: EmbroiderManagement, Version=3.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6C04B8B6-47A6-4678-8DD0-C49AAE9CC5B4
// Assembly location: D:\RealProject\EmbroiderManagementSystem\EmbroiderManagement\obj\Debug\netcoreapp3.1\EmbroiderManagementSystem.dll

using IdentityServer4.Models;
using System.Collections.Generic;

namespace EmbroiderManagement
{
  public class IdentityServerConfig
  {
    public const string ApiName = "embroider_api";
    public const string ApiFriendlyName = "Embroider API";
    public const string QuickAppClientID = "embroider_spa";
    public const string SwaggerClientID = "swaggerui";

    public static IEnumerable<IdentityResource> GetIdentityResources() => (IEnumerable<IdentityResource>) new List<IdentityResource>()
    {
      (IdentityResource) new IdentityResources.OpenId(),
      (IdentityResource) new IdentityResources.Profile(),
      (IdentityResource) new IdentityResources.Phone(),
      (IdentityResource) new IdentityResources.Email(),
      new IdentityResource("roles", (IEnumerable<string>) new List<string>()
      {
        "role"
      })
    };

    public static IEnumerable<ApiResource> GetApiResources()
    {
      List<ApiResource> apiResourceList = new List<ApiResource>();
      ApiResource apiResource = new ApiResource("embroider_api");
      apiResource.UserClaims.Add("name");
      apiResource.UserClaims.Add("email");
      apiResource.UserClaims.Add("phone_number");
      apiResource.UserClaims.Add("role");
      apiResource.UserClaims.Add("permission");
      apiResourceList.Add(apiResource);
      return (IEnumerable<ApiResource>) apiResourceList;
    }

    public static IEnumerable<Client> GetClients() => (IEnumerable<Client>) new List<Client>()
    {
      new Client()
      {
        ClientId = "embroider_spa",
        AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
        AllowAccessTokensViaBrowser = true,
        RequireClientSecret = false,
        AllowedScopes = {
          "openid",
          "profile",
          "phone",
          "email",
          "roles",
          "embroider_api"
        },
        AllowOfflineAccess = true,
        RefreshTokenExpiration = TokenExpiration.Sliding,
        RefreshTokenUsage = TokenUsage.OneTimeOnly,
        AccessTokenLifetime = 2592000,
        AbsoluteRefreshTokenLifetime = 3153600,
        SlidingRefreshTokenLifetime = 3153600
      },
      new Client()
      {
        ClientId = "swaggerui",
        ClientName = "Swagger UI",
        AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
        AllowAccessTokensViaBrowser = true,
        RequireClientSecret = false,
        AllowedScopes = {
          "embroider_api"
        }
      }
    };
  }
}
