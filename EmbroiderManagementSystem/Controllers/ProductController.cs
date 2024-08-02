using AutoMapper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using EmbroiderManagement;
using EmbroiderManagementSystem.Helpers;
using EmbroideryData;
using EmbroideryData.DTO;
using EmbroideryData.Interface;
using EmbroideryService.Interface;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EmbroiderManagementSystem.Controllers
{
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        /// <summary>
        /// Defines the _mapper.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Defines the _accountManager.
        /// </summary>
        private readonly IAccountManager _accountManager;

        /// <summary>
        /// Defines the _authorizationService.
        /// </summary>
        private readonly IAuthorizationService _authorizationService;

        /// <summary>
        /// Defines the _logger.
        /// </summary>
        private readonly ILogger<AccountController> _logger;

        /// <summary>
        /// Defines the _productService.
        /// </summary>
        private readonly IProductService _productService;

        public ProductController(
      IMapper mapper,
      IAccountManager accountManager,
      IAuthorizationService authorizationService,
      IProductService productService,
      ILogger<AccountController> logger)
        {
            this._mapper = mapper;
            this._accountManager = accountManager;
            this._authorizationService = authorizationService;
            this._logger = logger;
            this._productService = productService;
        }

        [HttpPost("saveGroup")]
        public async Task<ProductGroup> SaveOrUpdateGroup([FromBody] ProductGroup group)
        {
            if (group.Id > 0)
            {
                string userId = Utilities.GetUserId(this.User);
                group.UpdatedOn = DateTimeOffset.Now;
                group.UpdatedBy = userId.ToString();
                await this._productService.UpdateGroupAsync(group);
                return group;
            }
            string userId1 = Utilities.GetUserId(this.User);
            group.CreatedBy = userId1.ToString();
            group.CreatedOn = DateTimeOffset.Now;
            group.UpdatedOn = DateTimeOffset.Now;
            ProductGroup productGroup = await this._productService.SaveGroupAsync(group);
            return group;
        }

        [HttpPost("saveCategory")]
        public async Task<Category> SaveOrUpdateCategory([FromBody] Category category)
        {
            try
            {
                if (category.Id > 0)
                {
                    string userId = Utilities.GetUserId(this.User);
                    category.UpdatedOn = DateTimeOffset.Now;
                    category.UpdatedBy = userId.ToString();
                    await this._productService.UpdateCategoryAsync(category);
                    return category;
                }
                string userId1 = Utilities.GetUserId(this.User);
                category.CreatedBy = userId1.ToString();
                category.CreatedOn = DateTimeOffset.Now;
                category.UpdatedOn = DateTimeOffset.Now;
                Category category1 = await this._productService.SaveCategoryAsync(category);
                return category;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("saveSubCategory")]
        public async Task<SubCategory> SaveOrUpdateSubCategory([FromBody] SubCategory subCategory)
        {
            try
            {
                if (subCategory.Id > 0)
                {
                    string userId = Utilities.GetUserId(this.User);
                    subCategory.UpdatedOn = DateTimeOffset.Now;
                    subCategory.UpdatedBy = userId.ToString();
                    await this._productService.UpdateSubCategoryAsync(subCategory);
                    return subCategory;
                }
                string userId1 = Utilities.GetUserId(this.User);
                subCategory.CreatedBy = userId1.ToString();
                subCategory.CreatedOn = DateTimeOffset.Now;
                subCategory.UpdatedOn = DateTimeOffset.Now;
                SubCategory subCategory1 = await this._productService.SaveSubCategoryAsync(subCategory);
                return subCategory;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("saveProductWeight")]
        public async Task<ProductWeight> saveProductWeight([FromBody] ProductWeight productWeight)
        {
            try
            {
                if (productWeight.Id > 0)
                {
                    string userId = Utilities.GetUserId(this.User);
                    productWeight.UpdatedOn = DateTimeOffset.Now;
                    productWeight.UpdatedBy = userId.ToString();
                    await this._productService.UpdateProductWeightAsync(productWeight);
                    return productWeight;
                }
                string userId1 = Utilities.GetUserId(this.User);
                productWeight.CreatedBy = userId1.ToString();
                productWeight.CreatedOn = DateTimeOffset.Now;
                productWeight.UpdatedOn = DateTimeOffset.Now;
                ProductWeight productWeight1 = await this._productService.SaveProductWeightAsync(productWeight);
                return productWeight;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("getAllGroups")]
        public async Task<List<ProductGroup>> getAllGroups()
        {
            List<ProductGroup> groupListAllAsync = await this._productService.GetGroupListAllAsync();
            return groupListAllAsync;
        }

        [HttpGet("getAllCategories")]
        public async Task<List<Category>> GetAllCategories()
        {
            List<Category> categoryListAllAsync = await this._productService.GetCategoryListAllAsync();
            return categoryListAllAsync;
        }

        [HttpGet("getAllSubCategories")]
        public async Task<List<SubCategory>> GetAllSubCategories()
        {
            List<SubCategory> categoryByCriteriaAsync = await this._productService.GetSubCategoryListAllAsync();
            return categoryByCriteriaAsync;
        }

        [HttpGet("getByParentIdSubCategory")]
        public async Task<List<SubCategory>> getByParentId([FromQuery] int id)
        {
            List<SubCategory> categoryByCriteriaAsync = await this._productService.GetSubCategoryByCriteriaAsync((Expression<Func<SubCategory, bool>>)(x => x.CategoryId == id));
            return categoryByCriteriaAsync;
        }

        [HttpGet("getAllProductWeights")]
        public async Task<List<ProductWeight>> getAllProductWeight()
        {
            List<ProductWeight> weightListAllAsync = await this._productService.GetProductWeightListAllAsync();
            return weightListAllAsync;
        }

        [HttpGet("getGroupPagination")]
        public async Task<object> GetGroupPagination(DataSourceLoadOptions loadOptions)
        {
            var source = await this._productService.GetGroupDTOQueryable();
            LoadResult result = DataSourceLoader.Load(source,loadOptions);
            return result;
        }

        [HttpGet("getCategoryPagination")]
        public async Task<object> GetCategoryPagination(DataSourceLoadOptions loadOptions)
        {
           
            try
            {
                var source = await this._productService.GetCategoryDTOQueryable();
                LoadResult result = DataSourceLoader.Load(source, loadOptions);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        [HttpGet("getSubCategoryPagination")]
        public async Task<object> GetSubCategoryPagination(DataSourceLoadOptions loadOptions)
        {
            try
            {
                var source = await this._productService.GetSubCategoryDTOQueryable();
                LoadResult result = DataSourceLoader.Load(source, loadOptions);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("getProductWeightPagination")]
        public async Task<object> getProductWeightPagination(DataSourceLoadOptions loadOptions)
        {
            
            try
            {
                IQueryable<ProductWeightDTO> source = await this._productService.GetProductWeightDTOQueryable();
                LoadResult result = DataSourceLoader.Load(source, loadOptions);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        [HttpPost("deleteCategory")]
        public async Task<object> deleteCategory(CategoryView category)
        {
            object obj;
            try
            {
                Category entity = await this._productService.GetCategoryById(category.Id);
                await this._productService.DeleteCategoryAsync(entity);
                obj = (object)true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }

        [HttpGet("hasGroupDuplicateName")]
        public async Task<bool> HasDuplicateName(string oldVal, string newVal)
        {
            if (!string.IsNullOrEmpty(oldVal))
            {
                if ((await _productService.GetGroupByCriteriaAsync(x => x.Name == newVal)).Any())
                {
                    return !(await _productService.GetGroupByCriteriaAsync(x => x.Name == newVal && x.Name == oldVal)).Any();
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return (await _productService.GetGroupByCriteriaAsync(x => x.Name == newVal)).Any();
            }
        }

        [HttpGet("hasGroupDuplicateCode")]
        public async Task<bool> HasDuplicateCode(string oldVal, string newVal)
        {
            if (!string.IsNullOrEmpty(oldVal))
            {
                if ((await _productService.GetGroupByCriteriaAsync(x => x.GroupCode == newVal)).Any())
                {
                    return !(await _productService.GetGroupByCriteriaAsync(x => x.GroupCode == newVal && x.GroupCode == oldVal)).Any();
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return (await _productService.GetGroupByCriteriaAsync(x => x.GroupCode == newVal)).Any();
            }
        }

        [HttpGet("hasCategoryDuplicateName")]
        public async Task<bool> HasCategoryDuplicateName(string oldVal, string newVal)
        {
            if (!string.IsNullOrEmpty(oldVal))
            {
                if ((await _productService.GetCategoryByCriteriaAsync(x => x.Name == newVal)).Any())
                {
                    return !(await _productService.GetCategoryByCriteriaAsync(x => x.Name == newVal && x.Name == oldVal)).Any();
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return (await _productService.GetCategoryByCriteriaAsync(x => x.Name == newVal)).Any();
            }
        }

        [HttpGet("hasCategoryDuplicateCode")]
        public async Task<bool> HasCategoryDuplicateCode(string oldVal, string newVal)
        {
            if (!string.IsNullOrEmpty(oldVal))
            {
                if ((await _productService.GetCategoryByCriteriaAsync(x => x.CategoryCode == newVal)).Any())
                {
                    return !(await _productService.GetCategoryByCriteriaAsync(x => x.CategoryCode == newVal && x.CategoryCode == oldVal)).Any();
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return (await _productService.GetCategoryByCriteriaAsync(x => x.CategoryCode == newVal)).Any();
            }
        }

        [HttpGet("hasSubCategoryDuplicateName")]
        public async Task<bool> HasSubCategoryDuplicateName(string oldVal, string newVal)
        {
            if (!string.IsNullOrEmpty(oldVal))
            {
                if ((await _productService.GetSubCategoryByCriteriaAsync(x => x.Name == newVal)).Any())
                {
                    var result= !(await _productService.GetSubCategoryByCriteriaAsync(x => x.Name == newVal && x.Name == oldVal)).Any(); 
                    return !(await _productService.GetSubCategoryByCriteriaAsync(x => x.Name == newVal && x.Name == oldVal)).Any();
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return (await _productService.GetSubCategoryByCriteriaAsync(x => x.Name == newVal)).Any();
            }
        }

        [HttpGet("hasSubCategoryDuplicateCode")]
        public async Task<bool> HasSubCategoryDuplicateCode(string oldVal, string newVal)
        {
            if (!string.IsNullOrEmpty(oldVal))
            {
                if ((await _productService.GetSubCategoryByCriteriaAsync(x => x.SubCategoryCode == newVal)).Any())
                {
                    return !(await _productService.GetSubCategoryByCriteriaAsync(x => x.SubCategoryCode == newVal && x.SubCategoryCode == oldVal)).Any();
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return (await _productService.GetSubCategoryByCriteriaAsync(x => x.SubCategoryCode == newVal)).Any();
            }
        }
    }
}
