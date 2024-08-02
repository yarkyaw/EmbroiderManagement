using EmbroiderData.DTO;
using EmbroideryData;
using EmbroideryData.DTO;
using EmbroideryRepo.Interfaces;
using EmbroideryService.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace GroupyService
{
    public class ProductService : IProductService
    {
        /// <summary>
        /// Defines the _repoGroupAsync.
        /// </summary>
        private readonly IAsyncRepository<ProductGroup> _repoGroupAsync;

        /// <summary>
        /// Defines the _repoGroupViewAsync.
        /// </summary>
        private readonly IAsyncRepository<GroupView> _repoGroupViewAsync;

        /// <summary>
        /// Defines the _repoCategoryAsync.
        /// </summary>
        private readonly IAsyncRepository<Category> _repoCategoryAsync;

        /// <summary>
        /// Defines the _repoCategoryViewAsync.
        /// </summary>
        private readonly IAsyncRepository<CategoryView> _repoCategoryViewAsync;

        /// <summary>
        /// Defines the _repoSubCategoryAsync.
        /// </summary>
        private readonly IAsyncRepository<SubCategory> _repoSubCategoryAsync;

        /// <summary>
        /// Defines the _repoSubCategoryViewAsync.
        /// </summary>
        private readonly IAsyncRepository<SubCategoryView> _repoSubCategoryViewAsync;

        /// <summary>
        /// Defines the _repoProductWeightAsync.
        /// </summary>
        private readonly IAsyncRepository<ProductWeight> _repoProductWeightAsync;

        public ProductService(
      IAsyncRepository<ProductGroup> repoAsync,
      IAsyncRepository<GroupView> repoViewAsync,
      IAsyncRepository<Category> repoCategoryAsync,
      IAsyncRepository<CategoryView> repoCategoryViewAsync,
      IAsyncRepository<SubCategory> repoSubCategoryAsync,
      IAsyncRepository<SubCategoryView> repoSubCategoryViewAsync,
      IAsyncRepository<ProductWeight> repoProductWeightAsync)
        {
            this._repoGroupAsync = repoAsync;
            this._repoGroupViewAsync = repoViewAsync;
            this._repoCategoryAsync = repoCategoryAsync;
            this._repoCategoryViewAsync = repoCategoryViewAsync;
            this._repoSubCategoryAsync = repoSubCategoryAsync;
            this._repoSubCategoryViewAsync = repoSubCategoryViewAsync;
            this._repoProductWeightAsync = repoProductWeightAsync;
        }

        public async Task DeleteGroupAsync(ProductGroup entity) => await this._repoGroupAsync.DeleteAsync(entity);

        public async Task<List<ProductGroup>> GetGroupAsyncWithInclude(
      Func<IQueryable<ProductGroup>, IIncludableQueryable<ProductGroup, object>> include,
      Expression<Func<ProductGroup, bool>> criteria,
      Func<IQueryable<ProductGroup>, IOrderedQueryable<ProductGroup>> orderBy = null,
      bool noTrack = true,
      CancellationToken cancellationToken = default(CancellationToken))
        {
            List<ProductGroup> byCriteriaAsync = await this._repoGroupAsync.GetByCriteriaAsync(include, criteria, orderBy, noTrack);
            return byCriteriaAsync;
        }

        public async Task<List<ProductGroup>> GetGroupByCriteriaAsync(
      Expression<Func<ProductGroup, bool>> criteria)
        {
            List<ProductGroup> byCriteriaAsync = await this._repoGroupAsync.GetByCriteriaAsync((Func<IQueryable<ProductGroup>, IIncludableQueryable<ProductGroup, object>>)null, criteria);
            return byCriteriaAsync;
        }

        public async Task<ProductGroup> GetGroupById(int id)
        {
            ProductGroup byIdAsync = await this._repoGroupAsync.GetByIdAsync(id);
            return byIdAsync;
        }

        public async Task<List<ProductGroup>> GetGroupListAllAsync()
        {
            List<ProductGroup> productGroupList = await this._repoGroupAsync.ListAllAsync();
            return productGroupList;
        }

        public async Task<IQueryable<GroupView>> GroupViewAsQuerable()
        {
            IQueryable<GroupView> quearableAsync = await this._repoGroupViewAsync.GetQuearableAsync();
            return quearableAsync;
        }

        public async Task<ProductGroup> SaveGroupAsync(ProductGroup entity)
        {
            ProductGroup productGroup = await this._repoGroupAsync.AddAsync(entity);
            return productGroup;
        }

        public async Task UpdateGroupAsync(ProductGroup entity)
        {
            ProductGroup productGroup = await this._repoGroupAsync.UpdateAsync(entity);
        }

        public async Task DeleteCategoryAsync(Category entity) => await this._repoCategoryAsync.DeleteAsync(entity);

        public async Task<List<Category>> GetCategoryAsyncWithInclude(
      Func<IQueryable<Category>, IIncludableQueryable<Category, object>> include,
      Expression<Func<Category, bool>> criteria,
      Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null,
      bool noTrack = true,
      CancellationToken cancellationToken = default(CancellationToken))
        {
            List<Category> byCriteriaAsync = await this._repoCategoryAsync.GetByCriteriaAsync(include, criteria, orderBy, noTrack);
            return byCriteriaAsync;
        }

        public async Task<List<Category>> GetCategoryByCriteriaAsync(
      Expression<Func<Category, bool>> criteria)
        {
            List<Category> byCriteriaAsync = await this._repoCategoryAsync.GetByCriteriaAsync((Func<IQueryable<Category>, IIncludableQueryable<Category, object>>)null, criteria);
            return byCriteriaAsync;
        }

        public async Task<Category> GetCategoryById(int id)
        {
            Category byIdAsync = await this._repoCategoryAsync.GetByIdAsync(id);
            return byIdAsync;
        }

        public async Task<List<Category>> GetCategoryListAllAsync()
        {
            List<Category> categoryList = await this._repoCategoryAsync.ListAllAsync();
            return categoryList;
        }

        public async Task<IQueryable<CategoryView>> CategoryViewAsQuerable()
        {
            IQueryable<CategoryView> quearableAsync = await this._repoCategoryViewAsync.GetQuearableAsync();
            return quearableAsync;
        }

        public async Task<Category> SaveCategoryAsync(Category entity)
        {
            Category category = await this._repoCategoryAsync.AddAsync(entity);
            return category;
        }

        public async Task UpdateCategoryAsync(Category entity)
        {
            Category category = await this._repoCategoryAsync.UpdateAsync(entity);
        }

        public async Task<SubCategory> SaveSubCategoryAsync(SubCategory entity)
        {
            SubCategory subCategory = await this._repoSubCategoryAsync.AddAsync(entity);
            return subCategory;
        }

        public async Task<SubCategory> GetSubCategoryById(int id)
        {
            SubCategory byIdAsync = await this._repoSubCategoryAsync.GetByIdAsync(id);
            return byIdAsync;
        }

        public async Task UpdateSubCategoryAsync(SubCategory entity)
        {
            SubCategory subCategory = await this._repoSubCategoryAsync.UpdateAsync(entity);
        }

        public async Task DeleteSubCategoryAsync(SubCategory entity) => await this._repoSubCategoryAsync.DeleteAsync(entity);

        public async Task<List<SubCategory>> GetSubCategoryListAllAsync()
        {
            List<SubCategory> subCategoryList = await this._repoSubCategoryAsync.ListAllAsync();
            return subCategoryList;
        }

        public async Task<List<SubCategory>> GetSubCategoryAsyncWithInclude(
      Func<IQueryable<SubCategory>, IIncludableQueryable<SubCategory, object>> include,
      Expression<Func<SubCategory, bool>> criteria,
      Func<IQueryable<SubCategory>, IOrderedQueryable<SubCategory>> orderBy = null,
      bool noTrack = true,
      CancellationToken cancellationToken = default(CancellationToken))
        {
            List<SubCategory> byCriteriaAsync = await this._repoSubCategoryAsync.GetByCriteriaAsync(include, criteria, orderBy, noTrack);
            return byCriteriaAsync;
        }

        public async Task<List<SubCategory>> GetSubCategoryByCriteriaAsync(
      Expression<Func<SubCategory, bool>> criteria)
        {
            List<SubCategory> byCriteriaAsync = await this._repoSubCategoryAsync.GetByCriteriaAsync((Func<IQueryable<SubCategory>, IIncludableQueryable<SubCategory, object>>)null, criteria);
            return byCriteriaAsync;
        }

        public async Task<IQueryable<SubCategoryView>> SubCategoryViewAsQuerable()
        {
            IQueryable<SubCategoryView> quearableAsync = await this._repoSubCategoryViewAsync.GetQuearableAsync();
            return quearableAsync;
        }

        public async Task DeleteProductWeightAsync(ProductWeight entity) => await this._repoProductWeightAsync.DeleteAsync(entity);

        public async Task<List<ProductWeight>> GetProductWeightAsyncWithInclude(
      Func<IQueryable<ProductWeight>, IIncludableQueryable<ProductWeight, object>> include,
      Expression<Func<ProductWeight, bool>> criteria,
      Func<IQueryable<ProductWeight>, IOrderedQueryable<ProductWeight>> orderBy = null,
      bool noTrack = true,
      CancellationToken cancellationToken = default(CancellationToken))
        {
            List<ProductWeight> byCriteriaAsync = await this._repoProductWeightAsync.GetByCriteriaAsync(include, criteria, orderBy, noTrack);
            return byCriteriaAsync;
        }

        public async Task<List<ProductWeight>> GetProductWeightByCriteriaAsync(
      Expression<Func<ProductWeight, bool>> criteria)
        {
            List<ProductWeight> byCriteriaAsync = await this._repoProductWeightAsync.GetByCriteriaAsync((Func<IQueryable<ProductWeight>, IIncludableQueryable<ProductWeight, object>>)null, criteria);
            return byCriteriaAsync;
        }

        public async Task<ProductWeight> GetProductWeightById(int id)
        {
            ProductWeight byIdAsync = await this._repoProductWeightAsync.GetByIdAsync(id);
            return byIdAsync;
        }

        public async Task<IQueryable<ProductWeight>> GetProductWeightQueryable()
        {
            IQueryable<ProductWeight> quearableAsync = await this._repoProductWeightAsync.GetQuearableAsync();
            return quearableAsync;
        }

        public async Task<List<ProductWeight>> GetProductWeightListAllAsync()
        {
            List<ProductWeight> productWeightList = await this._repoProductWeightAsync.ListAllAsync();
            return productWeightList;
        }

        public async Task<ProductWeight> SaveProductWeightAsync(ProductWeight entity)
        {
            ProductWeight productWeight = await this._repoProductWeightAsync.AddAsync(entity);
            return productWeight;
        }

        public async Task UpdateProductWeightAsync(ProductWeight entity)
        {
            ProductWeight productWeight = await this._repoProductWeightAsync.UpdateAsync(entity);
        }

        public async Task<IQueryable<GroupDTO>> GetGroupDTOQueryable()
        {
            return await _repoGroupAsync.GetWithSelectorAsync<GroupDTO>(x => new GroupDTO
            {
                Id = x.Id,
                Name = x.Name,
                GroupCode = x.GroupCode

            }, x => x.Include(y => y.Categories).ThenInclude(y => y.SubCategories));
        }

        public async Task<IQueryable<CategoryDTO>> GetCategoryDTOQueryable()
        {
            return await _repoCategoryAsync.GetWithSelectorAsync<CategoryDTO>(x => new CategoryDTO
            {
                Id = x.Id,
                CategoryCode = x.CategoryCode,
                GroupCode = x.Group.GroupCode,
                GroupName = x.Group.Name,
                Name = x.Name,
                GroupId = x.GroupId

            }, x => x.Include(y => y.SubCategories).Include(y => y.Group));
        }

        public async Task<IQueryable<SubCategoryDTO>> GetSubCategoryDTOQueryable()
        {
            return await _repoSubCategoryAsync.GetWithSelectorAsync<SubCategoryDTO>(x => new SubCategoryDTO
            {
                Id = x.Id,
                SubCategoryCode = x.SubCategoryCode,
                Name = x.Name,
                CategoryCode = x.Category.CategoryCode,
                CategoryId = x.Category.Id,
                CategoryName = x.Category.Name,
                GroupCode = x.Category.Group.GroupCode,
                GroupId = x.Category.GroupId,
                GroupName = x.Category.Group.Name,
            }, x => x.Include(y => y.Category).ThenInclude(y => y.Group));
        }

        public async Task<IQueryable<ProductWeightDTO>> GetProductWeightDTOQueryable()
        {
            return await _repoProductWeightAsync.GetWithSelectorAsync<ProductWeightDTO>(x => new ProductWeightDTO
            {
                Id = x.Id,
                LocalizeName = x.LocalizeName,
                Gram = x.Gram,
                Name = x.Name

            }, null);
        }
    }
}
