using AutoMapper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using EmbroiderData;
using EmbroiderManagement;
using EmbroiderManagementSystem.Helpers;
using EmbroiderManagementSystem.ViewModels;
using EmbroiderOrderyService.Interface;
using EmbroideryData;
using EmbroideryData.DTO;
using EmbroideryData.Interface;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace EmbroiderManagementSystem.Controllers
{
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class EmbroiderOrderController : ControllerBase
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
        /// Defines the _embroiderOrderService.
        /// </summary>
        private readonly IEmbroiderOrderService _embroiderOrderService;

        public EmbroiderOrderController(
      IMapper mapper,
      IAccountManager accountManager,
      IAuthorizationService authorizationService,
      IEmbroiderOrderService embroiderOrderService,
      ILogger<AccountController> logger)
        {
            this._mapper = mapper;
            this._accountManager = accountManager;
            this._authorizationService = authorizationService;
            this._logger = logger;
            this._embroiderOrderService = embroiderOrderService;
        }

        [HttpPost("save")]
        public async Task<EmbroiderOrderModel> SaveOrUpdateGroup(
      [FromBody] EmbroiderOrderModel model)
        {
            try
            {

                model.OrderDate = model.OrderDate.AddHours(6.0).AddMinutes(30);
                var entity = this._mapper.Map<EmbroiderOrder>(model);
                string userId = Utilities.GetUserId(this.User).ToString();
                if (model.Id > 0)
                {
                    entity.UpdatedOn = DateTimeOffset.Now;
                    entity.UpdatedBy = userId;
                    var newlyAddedDetail = model.OrderDetails.Where(x => x.Id == 0).ToList();
                    var updatedDetail = entity.OrderDetails.Where(x => x.Id > 0).ToList();

                    var originalEntity = (await this._embroiderOrderService.GetAsyncWithInclude(x => x.Include(y => y.OrderDetails).Include(y => y.EmbroiderOrder_Category).Include(y => y.EmbroiderOrder_Embroider).Include(y => y.EmbroiderOrder_ProductWeight), x => x.Id == model.Id,noTrack:false)).FirstOrDefault();

                    var deletedEntities = updatedDetail.Any() ? originalEntity.OrderDetails.Where(x => !updatedDetail.Select(z => z.Id).ToList().Contains(x.Id)).Select(x => x).ToList() : new List<EmbroiderOrderDetail>();
                    deletedEntities.ToList().ForEach(x => originalEntity.OrderDetails.Remove(x));
                    updatedDetail.ForEach(x =>
                   {
                       EmbroiderOrderDetail temp = originalEntity.OrderDetails.Where(y => y.Id == x.Id).Single();
                       temp.Quantity = x.Quantity;
                       temp.EmbroiderOrderDetail_SubCategory.SubCategoryId = x.EmbroiderOrderDetail_SubCategory.SubCategoryId;
                       temp.Description = x.Description;
                       temp.Ratio = x.Ratio;
                       temp.MaterialType = x.MaterialType;
                       temp.UpdatedBy = userId;
                       temp.UpdatedOn = DateTime.UtcNow;
                   });
                    newlyAddedDetail.ToList().ForEach(x =>
                   {
                       var orderDetails = originalEntity.OrderDetails;
                       orderDetails.Add(new EmbroiderOrderDetail()
                       {
                           OrderId = entity.Id,
                           Quantity = x.Quantity,
                           Ratio=x.Ratio,
                           CreatedBy = entity.CreatedBy,
                           UpdatedBy = entity.CreatedBy,
                           CreatedOn = entity.CreatedOn,
                           UpdatedOn = entity.UpdatedOn,
                           Description = x.Description,
                           MaterialType = x.MaterialType,
                           EmbroiderOrderDetail_SubCategory = new EmbroiderOrderDetail_SubCategory()
                           {
                               SubCategoryId = x.SubCategoryId
                           }
                       });
                   });
                    originalEntity.UpdatedBy = entity.UpdatedBy;
                    originalEntity.UpdatedOn = entity.UpdatedOn;
                    originalEntity.OrderDate = entity.OrderDate;
                    originalEntity.PaidGold = entity.PaidGold;
                    originalEntity.PaidJewel = entity.PaidJewel;
                    originalEntity.OrderType = entity.OrderType;
                    originalEntity.OrderStatus = Status.Saved;
                    originalEntity.EmbroiderOrder_Category.CategoryId = model.CategoryId;
                    originalEntity.EmbroiderOrder_Embroider.EmbroiderId = model.EmbroiderId;
                    originalEntity.EmbroiderOrder_ProductWeight.ProductWeightId = model.ProductWeightId;
                    await this._embroiderOrderService.UpdateAsync(originalEntity);
                    return model;
                }


                entity.CreatedBy = userId;
                entity.CreatedOn = DateTimeOffset.Now;
                entity.UpdatedOn = DateTimeOffset.Now;
                entity.OrderStatus = Status.Saved;
                string prefixCode = "EO-";
                string format = model.OrderDate.ToString("yyMM", (IFormatProvider)CultureInfo.InvariantCulture) + "-";
                prefixCode += format;
                var source = await _embroiderOrderService.GetByCriteriaAsync(x => x.OrderNo.Contains(prefixCode));
                string maxOrderCode = source.Select(x => x.OrderNo).Max();
                if (maxOrderCode != null)
                {
                    string last = (Convert.ToInt32(maxOrderCode.Split("-")[2]) + 1).ToString();
                    last = last.PadLeft(5, '0');
                    entity.OrderNo = prefixCode + last;
                    last = (string)null;
                }
                else
                {
                    string first = "1";
                    first = first.PadLeft(5, '0');
                    entity.OrderNo = prefixCode + first;
                    first = (string)null;
                }

                entity.EmbroiderOrder_Category = new EmbroiderOrder_Category { CategoryId = model.CategoryId };
                entity.EmbroiderOrder_ProductWeight = new EmbroiderOrder_ProductWeight { ProductWeightId = model.ProductWeightId };
                entity.EmbroiderOrder_Embroider = new EmbroiderOrder_Embroider { EmbroiderId = model.EmbroiderId };
                entity.OrderDetails = new List<EmbroiderOrderDetail>();
                model.OrderDetails.ToList().ForEach(x =>
               {

                   entity.OrderDetails.Add(new EmbroiderOrderDetail()
                   {
                       OrderId = entity.Id,
                       Quantity = x.Quantity,
                       Ratio=x.Ratio,
                       CreatedBy = entity.CreatedBy,
                       UpdatedBy = entity.CreatedBy,
                       CreatedOn = entity.CreatedOn,
                       UpdatedOn = entity.UpdatedOn,
                       Description = x.Description,
                       MaterialType = x.MaterialType,
                       EmbroiderOrderDetail_SubCategory = new EmbroiderOrderDetail_SubCategory()
                       {
                           SubCategoryId = x.SubCategoryId
                       }
                   });
               });
                await _embroiderOrderService.SaveAsync(entity);
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("getPagination")]
        public async Task<object> GetEmbroiderOrderPagination(DataSourceLoadOptions loadOptions)
        {
            IQueryable<EmbroiderOrderDTO> source = await this._embroiderOrderService.GetDTOQueryable();
            LoadResult result = DataSourceLoader.Load(source, loadOptions);
            return result;
        }

        [HttpGet("getAll")]
        public async Task<object> GetAllEmbroiderOrders()
        {
            List<EmbroiderOrder> listAllAsync = await this._embroiderOrderService.GetListAllAsync();
            return (object)listAllAsync;
        }

        [HttpGet("getByEmbroiderId")]
        public async Task<object> getEmbroiderOrderByEmbroiderId(int id)
        {
            var entity = (await this._embroiderOrderService.GetAsyncWithInclude(x => x.Include(y => y.EmbroiderOrder_ProductWeight).Include(y => y.EmbroiderOrder_Embroider).Include(y => y.EmbroiderOrder_Category), x => x.EmbroiderOrder_Embroider.EmbroiderId == id)).FirstOrDefault();
            var obj = _mapper.Map<EmbroiderOrderModel>(entity);
            return obj;
        }

        [HttpGet("getById")]
        public async Task<object> GetEmbroiderOrderById([FromQuery] int id)
        {
            if (id == 0)
                return null;
            var entity = (await this._embroiderOrderService.GetAsyncWithInclude(x => x.Include(y => y.EmbroiderOrder_ProductWeight).Include(y => y.EmbroiderOrder_Embroider).Include(y => y.EmbroiderOrder_Category).Include(y => y.OrderDetails).ThenInclude(z => z.EmbroiderOrderDetail_SubCategory), x => x.Id == id)).FirstOrDefault();
            var obj = _mapper.Map<EmbroiderOrderModel>(entity);
            return obj;
        }
    }
}
