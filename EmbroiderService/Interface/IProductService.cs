// Decompiled with JetBrains decompiler
// Type: EmbroideryService.Interface.IProductService
// Assembly: EmbroideryService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B47DA04D-694D-42C7-AAB8-A117E695B3FB
// Assembly location: D:\RealProject\EmbroiderManagementSystem\EmbroideryService\bin\Debug\netcoreapp3.1\EmbroideryService.dll

using EmbroiderData.DTO;
using EmbroideryData;
using EmbroideryData.DTO;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace EmbroideryService.Interface
{
  public interface IProductService
  {
    Task<ProductGroup> SaveGroupAsync(ProductGroup entity);

    Task<ProductGroup> GetGroupById(int id);

    Task UpdateGroupAsync(ProductGroup entity);

    Task DeleteGroupAsync(ProductGroup entity);

    Task<List<ProductGroup>> GetGroupListAllAsync();

    Task<List<ProductGroup>> GetGroupAsyncWithInclude(
      Func<IQueryable<ProductGroup>, IIncludableQueryable<ProductGroup, object>> include,
      Expression<Func<ProductGroup, bool>> criteria,
      Func<IQueryable<ProductGroup>, IOrderedQueryable<ProductGroup>> orderBy = null,
      bool noTrack = true,
      CancellationToken cancellationToken = default (CancellationToken));

    Task<List<ProductGroup>> GetGroupByCriteriaAsync(
      Expression<Func<ProductGroup, bool>> criteria);

    Task<IQueryable<GroupView>> GroupViewAsQuerable();

    Task<IQueryable<GroupDTO>> GetGroupDTOQueryable();

    Task<Category> SaveCategoryAsync(Category entity);

    Task<Category> GetCategoryById(int id);

    Task UpdateCategoryAsync(Category entity);

    Task DeleteCategoryAsync(Category entity);

    Task<List<Category>> GetCategoryListAllAsync();

    Task<List<Category>> GetCategoryAsyncWithInclude(
      Func<IQueryable<Category>, IIncludableQueryable<Category, object>> include,
      Expression<Func<Category, bool>> criteria,
      Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null,
      bool noTrack = true,
      CancellationToken cancellationToken = default (CancellationToken));

    Task<List<Category>> GetCategoryByCriteriaAsync(
      Expression<Func<Category, bool>> criteria);

    Task<IQueryable<CategoryView>> CategoryViewAsQuerable();

    Task<IQueryable<CategoryDTO>> GetCategoryDTOQueryable();

    Task<SubCategory> SaveSubCategoryAsync(SubCategory entity);

    Task<SubCategory> GetSubCategoryById(int id);

    Task UpdateSubCategoryAsync(SubCategory entity);

    Task DeleteSubCategoryAsync(SubCategory entity);

    Task<List<SubCategory>> GetSubCategoryListAllAsync();

    Task<List<SubCategory>> GetSubCategoryAsyncWithInclude(
      Func<IQueryable<SubCategory>, IIncludableQueryable<SubCategory, object>> include,
      Expression<Func<SubCategory, bool>> criteria,
      Func<IQueryable<SubCategory>, IOrderedQueryable<SubCategory>> orderBy = null,
      bool noTrack = true,
      CancellationToken cancellationToken = default (CancellationToken));

    Task<List<SubCategory>> GetSubCategoryByCriteriaAsync(
      Expression<Func<SubCategory, bool>> criteria);

    Task<IQueryable<SubCategoryView>> SubCategoryViewAsQuerable();

    Task<IQueryable<SubCategoryDTO>> GetSubCategoryDTOQueryable();

    Task<ProductWeight> SaveProductWeightAsync(ProductWeight entity);

    Task<ProductWeight> GetProductWeightById(int id);

    Task UpdateProductWeightAsync(ProductWeight entity);

    Task DeleteProductWeightAsync(ProductWeight entity);

    Task<List<ProductWeight>> GetProductWeightListAllAsync();

    Task<List<ProductWeight>> GetProductWeightAsyncWithInclude(
      Func<IQueryable<ProductWeight>, IIncludableQueryable<ProductWeight, object>> include,
      Expression<Func<ProductWeight, bool>> criteria,
      Func<IQueryable<ProductWeight>, IOrderedQueryable<ProductWeight>> orderBy = null,
      bool noTrack = true,
      CancellationToken cancellationToken = default (CancellationToken));

    Task<List<ProductWeight>> GetProductWeightByCriteriaAsync(
      Expression<Func<ProductWeight, bool>> criteria);

    Task<IQueryable<ProductWeight>> GetProductWeightQueryable();

    Task<IQueryable<ProductWeightDTO>> GetProductWeightDTOQueryable();
  }
}
