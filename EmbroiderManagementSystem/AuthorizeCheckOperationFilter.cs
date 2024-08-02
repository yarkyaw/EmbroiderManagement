// Decompiled with JetBrains decompiler
// Type: EmbroiderManagementSystem.AuthorizeCheckOperationFilter
// Assembly: EmbroiderManagement, Version=3.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6C04B8B6-47A6-4678-8DD0-C49AAE9CC5B4
// Assembly location: D:\RealProject\EmbroiderManagementSystem\EmbroiderManagement\obj\Debug\netcoreapp3.1\EmbroiderManagementSystem.dll

using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace EmbroiderManagement
{
  internal class AuthorizeCheckOperationFilter : IOperationFilter
  {
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
      if (!((IEnumerable<object>) context.MethodInfo.DeclaringType.GetCustomAttributes(true)).Union<object>((IEnumerable<object>) context.MethodInfo.GetCustomAttributes(true)).OfType<AuthorizeAttribute>().Any<AuthorizeAttribute>())
        return;
      operation.Responses.Add("401", new OpenApiResponse()
      {
        Description = "Unauthorized"
      });
      OpenApiSecurityScheme apiSecurityScheme = new OpenApiSecurityScheme()
      {
        Reference = new OpenApiReference()
        {
          Type = new ReferenceType?(ReferenceType.SecurityScheme),
          Id = "oauth2"
        }
      };
      OpenApiOperation openApiOperation = operation;
      List<OpenApiSecurityRequirement> securityRequirementList1 = new List<OpenApiSecurityRequirement>();
      List<OpenApiSecurityRequirement> securityRequirementList2 = securityRequirementList1;
      OpenApiSecurityRequirement securityRequirement1 = new OpenApiSecurityRequirement();
      OpenApiSecurityScheme key = apiSecurityScheme;
      securityRequirement1[key] = (IList<string>) new string[1]
      {
        "embroider_api"
      };
      OpenApiSecurityRequirement securityRequirement2 = securityRequirement1;
      securityRequirementList2.Add(securityRequirement2);
      List<OpenApiSecurityRequirement> securityRequirementList3 = securityRequirementList1;
      openApiOperation.Security = (IList<OpenApiSecurityRequirement>) securityRequirementList3;
    }
  }
}
