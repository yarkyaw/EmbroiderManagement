using AutoMapper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using EmbroiderData;
using EmbroiderManagement;
using EmbroiderManagementSystem.Helpers;
using EmbroiderManagementSystem.ViewModels;
using EmbroiderOrderyService.Interface;
using EmbroideryData;
using EmbroideryData.Interface;
using EmbroideryService.Interface;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmbroiderManagementSystem.Controllers
{
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class EmbroiderInvoiceController : ControllerBase
    {
        #region Fields

        /// <summary>
        /// Defines the _accountManager.
        /// </summary>
        private readonly IAccountManager _accountManager;

        /// <summary>
        /// Defines the _authorizationService.
        /// </summary>
        private readonly IAuthorizationService _authorizationService;

        /// <summary>
        /// Defines the _EmbroiderInvoiceService.
        /// </summary>
        private readonly IEmbroiderInvoiceService _embroiderInvoiceService;

        private readonly IEmbroiderOrderService _embroiderOrderService;

        private readonly IEmbroiderService _embroiderService;

        /// <summary>
        /// Defines the _embroiderServiceItemHistoryService.
        /// </summary>
        private readonly IEmbroiderServiceItemHistoryService _embroiderServiceItemHistoryService;

        /// <summary>
        /// Defines the _logger.
        /// </summary>
        private readonly ILogger<AccountController> _logger;

        /// <summary>
        /// Defines the _mapper.
        /// </summary>
        private readonly IMapper _mapper;

        #endregion

        #region Constructors

        public EmbroiderInvoiceController(
      IMapper mapper,
      IAccountManager accountManager,
      IAuthorizationService authorizationService,
      IEmbroiderInvoiceService EmbroiderInvoiceService,
      IEmbroiderServiceItemHistoryService embroiderServiceItemHistoryService,
      IEmbroiderService embroiderService,
      IEmbroiderOrderService embroiderOrderService,
      ILogger<AccountController> logger)
        {
            this._mapper = mapper;
            this._accountManager = accountManager;
            this._authorizationService = authorizationService;
            this._logger = logger;
            this._embroiderInvoiceService = EmbroiderInvoiceService;
            this._embroiderServiceItemHistoryService = embroiderServiceItemHistoryService;
            _embroiderService = embroiderService;
            _embroiderOrderService = embroiderOrderService;
        }

        #endregion

        #region Methods

        [HttpGet("getAll")]
        public async Task<object> GetAllEmbroiderInvoices()
        {
            List<EmbroiderInvoice> listAllAsync = await this._embroiderInvoiceService.GetListAllAsync();
            return (object)listAllAsync;
        }

        [HttpGet("getByParentIdEmbroider")]
        public async Task<object> getEmbroiderInvoiceByEmbroiderId(int id)
        {
            IMapper mapper = this._mapper;

            var entity = (await this._embroiderInvoiceService.GetAsyncWithInclude(x => x.Include(y => y.EmbroiderInvoice_Category).Include(y => y.InvoiceDetails).Include(y => y.EmbroiderInvoice_ProductWeight).Include(y => y.EmbroiderOrder_EmbroiderInvoice), x => x.Id == id)).FirstOrDefault();

            var model = _mapper.Map<EmbroiderInvoiceModel>(entity);
            return model;
        }

        [HttpGet("getById")]
        public async Task<object> GetEmbroiderInvoiceById([FromQuery] int id)
        {
            if (id == 0)
                return (object)null;
            IMapper mapper = this._mapper;
            List<EmbroiderInvoice> asyncWithInclude = await this._embroiderInvoiceService.GetAsyncWithInclude(x => x.Include(y => y.InvoiceDetails).ThenInclude(z => z.EmbroiderInvoiceDetail_SubCategory).Include(y => y.EmbroiderInvoice_Category).Include(y => y.EmbroiderInvoice_Embroider).Include(y => y.EmbroiderOrder_EmbroiderInvoice), x => x.Id == id, null, true, new CancellationToken());
            return mapper.Map<EmbroiderInvoiceModel>(asyncWithInclude.FirstOrDefault());
        }

        [HttpGet("getByIdIncludeOrder")]
        public async Task<object> GetEmbroiderInvoiceByIdIncludeOrder([FromQuery] int id)
        {
            if (id == 0)
                return (object)null;
            IMapper mapper = this._mapper;
            List<EmbroiderInvoice> asyncWithInclude = await this._embroiderInvoiceService.GetAsyncWithInclude(x => x.Include(y => y.InvoiceDetails).ThenInclude(z => z.EmbroiderInvoiceDetail_SubCategory).Include(y => y.EmbroiderInvoice_Category).Include(y => y.EmbroiderInvoice_Embroider).Include(y => y.EmbroiderOrder_EmbroiderInvoice), x => x.Id == id, null, true, new CancellationToken());
            return mapper.Map<EmbroiderInvoiceModel>(asyncWithInclude.FirstOrDefault());
        }

        [HttpGet("getPagination")]
        public async Task<object> GetEmbroiderInvoicePagination(DataSourceLoadOptions loadOptions)
        {
            var source = await _embroiderInvoiceService.GetDTOQueryable();
            LoadResult result = DataSourceLoader.Load(source, loadOptions);
            return result;
        }

        [HttpPost("save")]
        public async Task<EmbroiderInvoiceModel> SaveOrUpdateGroup(
      [FromBody] EmbroiderInvoiceModel model)
        {
            try
            {
                model.InvoiceDate = model.InvoiceDate.AddHours(6.0);
                model.InvoiceDate = model.InvoiceDate.AddMinutes(30.0);
                var entity = this._mapper.Map<EmbroiderInvoice>(model);
                string userId = Utilities.GetUserId(this.User).ToString();
                if (model.Id > 0)
                {
                    var tempExcessOrLack = 0M;
                    entity.UpdatedOn = DateTimeOffset.Now;
                    entity.UpdatedBy = userId;
                    List<EmbroiderInvoiceDetail> newlyAddedDetail = entity.InvoiceDetails.Where(x => x.Id == 0).ToList();
                    List<EmbroiderInvoiceDetail> updatedDetail = entity.InvoiceDetails.Where(x => x.Id > 0).ToList();
                    var originalEntity = (await _embroiderInvoiceService.GetAsyncWithInclude(x => x.Include(y => y.InvoiceDetails).ThenInclude(z => z.EmbroiderInvoiceDetail_SubCategory).Include(y => y.EmbroiderOrder_EmbroiderInvoice).Include(y => y.EmbroiderInvoice_Category).Include(y => y.EmbroiderInvoice_Embroider).ThenInclude(z => z.Embroider), x => x.Id == model.Id, noTrack: false)).FirstOrDefault();
                    tempExcessOrLack = originalEntity.ExcessOrLack;
                    var deletedEntities = updatedDetail.Any() ? originalEntity.InvoiceDetails.Where(x => !updatedDetail.Select(z => z.Id).ToList().Contains(x.Id)).Select(x => x).ToList() : new List<EmbroiderInvoiceDetail>();
                    deletedEntities.ToList().ForEach((x => originalEntity.InvoiceDetails.Remove(x)));
                    updatedDetail.ForEach(x =>
                   {
                       var temp = originalEntity.InvoiceDetails.Where(y => y.Id == x.Id).Single();
                       temp.Quantity = x.Quantity;
                       temp.EmbroiderInvoiceDetail_SubCategory.SubCategoryId = x.EmbroiderInvoiceDetail_SubCategory.SubCategoryId;

                       temp.DetailType = x.DetailType;
                       temp.Quantity = x.Quantity;
                       temp.ActualQuantity = x.ActualQuantity;
                       temp.ActiveStatus = x.ActiveStatus;
                       temp.UpdatedBy = userId;
                       temp.UpdatedOn = DateTime.UtcNow;
                   });
                    newlyAddedDetail.ToList().ForEach(x =>
                    {
                        x.InvoiceId = entity.Id;
                        x.CreatedBy = entity.CreatedBy;
                        x.UpdatedBy = entity.CreatedBy;
                        x.CreatedOn = entity.CreatedOn;
                        x.UpdatedOn = entity.UpdatedOn;
                        originalEntity.InvoiceDetails.Add(x);
                    });
                    originalEntity.UpdatedBy = entity.UpdatedBy;
                    originalEntity.UpdatedOn = entity.UpdatedOn;
                    originalEntity.InvoiceDate = entity.InvoiceDate;
                    originalEntity.GoldGradeId = entity.GoldGradeId;
                    originalEntity.HasBalance = entity.HasBalance;
                    originalEntity.InvoiceStatus = Status.Saved;
                    originalEntity.ServiceFee = entity.ServiceFee;
                    originalEntity.ServiceFeePerItem = entity.ServiceFeePerItem;
                    originalEntity.ReceivedGold = entity.ReceivedGold;
                    originalEntity.DiposalGold = entity.DiposalGold;
                    originalEntity.ExcessOrLack = entity.ExcessOrLack;
                    originalEntity.EmbroiderOrder_EmbroiderInvoice.InvoiceId = model.Id;
                    originalEntity.EmbroiderOrder_EmbroiderInvoice.OrderId = model.OrderId;
                    originalEntity.EmbroiderInvoice_Category.CategoryId = model.CategoryId;
                    originalEntity.EmbroiderInvoice_ProductWeight.ProductWeightId = model.ProductWeightId;
                    originalEntity.EmbroiderInvoice_Embroider.EmbroiderId = model.EmbroiderId;

                    var status = Status.Saved;
                    if (entity.InvoiceDetails.Where(x => x.DetailType == EmbroiderInvoiceDetailType.Retrun && x.ActualQuantity > 0).Any())
                    {
                        status = Status.Processing;
                    }
                    else status = Status.Completed;
                    originalEntity.InvoiceStatus = status;
                    await _embroiderInvoiceService.UpdateAsync(originalEntity);
                    var rateHistory = new EmbroiderServiceItemHistory()
                    {
                        ProductWeightId = model.ProductWeightId,
                        CategoryId = model.CategoryId,
                        InsertedDate = entity.InvoiceDate,
                        Rate = entity.ServiceFeePerItem
                    };
                    var embroider_order = await _embroiderOrderService.GetById(model.OrderId);
                    embroider_order.OrderStatus = status;

                    await _embroiderServiceItemHistoryService.SaveAsync(rateHistory);
                    return model;
                }
                else
                {
                    entity.CreatedBy = userId;
                    entity.CreatedOn = DateTimeOffset.Now;
                    entity.UpdatedOn = DateTimeOffset.Now;
                    entity.InvoiceStatus = Status.Saved;
                    entity.InvoiceDetails.ToList().ForEach(x =>
                    {
                        x.UpdatedBy = entity.CreatedBy;
                        x.CreatedOn = entity.CreatedOn;
                        x.UpdatedOn = entity.UpdatedOn;
                    });
                    string prefixCode = "EI-";
                    string format = model.InvoiceDate.ToString("yyMM", CultureInfo.InvariantCulture) + "-";
                    prefixCode += format;
                    var list = await this._embroiderInvoiceService.GetByCriteriaAsync(x => x.InvoiceNo.Contains(prefixCode));
                    string maxInvoiceCode = list.Select(x => x.InvoiceNo).Max();
                    if (maxInvoiceCode != null)
                    {
                        var last = (Convert.ToInt32(maxInvoiceCode.Split("-")[2]) + 1).ToString();
                        last = last.PadLeft(5, '0');
                        entity.InvoiceNo = prefixCode + last;
                    }
                    else
                    {
                        string first = "1";
                        first = first.PadLeft(5, '0');
                        entity.InvoiceNo = prefixCode + first;
                        first = (string)null;
                    }

                    await _embroiderInvoiceService.SaveAsync(entity);

                    var rateHistory = new EmbroiderServiceItemHistory()
                    {
                        ProductWeightId = model.ProductWeightId,
                        CategoryId = model.CategoryId,
                        InsertedDate = entity.InvoiceDate,
                        Rate = entity.ServiceFeePerItem
                    };
                    var embroider_order = await _embroiderOrderService.GetById(model.OrderId);
                    if (entity.InvoiceDetails.Where(x => x.DetailType == EmbroiderInvoiceDetailType.Retrun && x.ActualQuantity > 0).Any())
                    {
                        embroider_order.OrderStatus = Status.Processing;
                    }
                    else embroider_order.OrderStatus = Status.Completed;
                    await _embroiderServiceItemHistoryService.SaveAsync(rateHistory);
                    return model;
                }
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion
    }
}
