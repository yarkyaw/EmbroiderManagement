using EmbroiderData.DTO;
using EmbroideryData;
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

namespace EmbroideryService
{
    public class EmbroiderInvoiceService : IEmbroiderInvoiceService
    {
        /// <summary>
        /// Defines the _repoAsync.
        /// </summary>
        private readonly IAsyncRepository<EmbroiderInvoice> _repoAsync;

        /// <summary>
        /// Defines the _repoDetailAsync.
        /// </summary>
        private readonly IAsyncRepository<EmbroiderInvoiceDetail> _repoDetailAsync;

        public EmbroiderInvoiceService(
      IAsyncRepository<EmbroiderInvoice> repoAsync,
      IAsyncRepository<EmbroiderInvoiceDetail> repoDetailAsync)
        {
            this._repoAsync = repoAsync;
            this._repoDetailAsync = repoDetailAsync;
        }

        public async Task DeleteAsync(EmbroiderInvoice entity) => await this._repoAsync.DeleteAsync(entity);

        public async Task<List<EmbroiderInvoice>> GetAsyncWithInclude(
          Func<IQueryable<EmbroiderInvoice>, IIncludableQueryable<EmbroiderInvoice, object>> include,
          Expression<Func<EmbroiderInvoice, bool>> criteria,
          Func<IQueryable<EmbroiderInvoice>, IOrderedQueryable<EmbroiderInvoice>> orderBy = null,
          bool noTrack = true,
          CancellationToken cancellationToken = default(CancellationToken))
        {
            List<EmbroiderInvoice> byCriteriaAsync = await this._repoAsync.GetByCriteriaAsync(include, criteria, orderBy, noTrack);
            return byCriteriaAsync;
        }

        public async Task<List<EmbroiderInvoice>> GetByCriteriaAsync(
      Expression<Func<EmbroiderInvoice, bool>> criteria)
        {
            List<EmbroiderInvoice> byCriteriaAsync = await this._repoAsync.GetByCriteriaAsync((Func<IQueryable<EmbroiderInvoice>, IIncludableQueryable<EmbroiderInvoice, object>>)null, criteria);
            return byCriteriaAsync;
        }

        public async Task<EmbroiderInvoice> GetById(int id)
        {
            EmbroiderInvoice byIdAsync = await this._repoAsync.GetByIdAsync(id);
            return byIdAsync;
        }

        public async Task<IQueryable<EmbroiderInvoiceDTO>> GetDTOQueryable()
        {
            return await _repoAsync.GetWithSelectorAsync<EmbroiderInvoiceDTO>(x => new EmbroiderInvoiceDTO
            {
                Id = x.Id,
                InvoiceNo = x.InvoiceNo,
                OrderNo = x.EmbroiderOrder_EmbroiderInvoice.EmbroiderOrder.OrderNo,
                OrderId = x.EmbroiderOrder_EmbroiderInvoice.OrderId,
                InvoiceDate = x.InvoiceDate,
                ReceivedGold = x.ReceivedGold,
                DiposalGold = x.DiposalGold,
                PaidToEmbroider = x.PaidToEmbroider,
                Total = x.Total,
                InvoiceStatus = x.InvoiceStatus,
                Remark = x.Remark,
                HasBalance = x.HasBalance,
                Balance = x.Balance,
                GoldGradeId = x.GoldGradeId,
                ServiceFee = x.ServiceFee,
                ServiceFeePerItem = x.ServiceFeePerItem,
                ExcessOrLack = x.ExcessOrLack,
                EmbroiderId=x.EmbroiderInvoice_Embroider.EmbroiderId,
                EmbroiderName=x.EmbroiderInvoice_Embroider.Embroider.Name,
                EmbroiderCode = x.EmbroiderInvoice_Embroider.Embroider.EmbroiderCode,
                CategoryName=x.EmbroiderInvoice_Category.Category.Name,
                ProductWeightName=x.EmbroiderInvoice_ProductWeight.ProductWeight.Name,
                OrderType=x.EmbroiderOrder_EmbroiderInvoice.EmbroiderOrder.OrderType,
                InvoiceDetails = x.InvoiceDetails.AsQueryable().Select(y => new EmbroiderInvoiceDetailDTO
                {
                    SubCategoryId = y.EmbroiderInvoiceDetail_SubCategory.SubCategory.Id,
                    SubCategoryCode = y.EmbroiderInvoiceDetail_SubCategory.SubCategory.SubCategoryCode,
                    SubCategoryName = y.EmbroiderInvoiceDetail_SubCategory.SubCategory.Name,
                    Id = y.Id,
                    InvoiceId = y.InvoiceId,
                    Quantity = y.Quantity,
                    Description = string.IsNullOrEmpty(y.Description)? y.EmbroiderInvoiceDetail_SubCategory.SubCategory.Name:y.Description,
                    DetailType = y.DetailType,
                    ActualQuantity = y.ActualQuantity

                }).OrderBy(x=>x.SubCategoryId).ThenBy(x=>x.DetailType).ToList()

            }, x => x.Include(y => y.EmbroiderInvoice_ProductWeight).ThenInclude(z=>z.ProductWeight).Include(y => y.InvoiceDetails).ThenInclude(z => z.EmbroiderInvoiceDetail_SubCategory).ThenInclude(a => a.SubCategory).Include(x => x.EmbroiderOrder_EmbroiderInvoice).ThenInclude(z => z.EmbroiderOrder).Include(x=>x.EmbroiderInvoice_Embroider).ThenInclude(z=>z.Embroider).Include(x=>x.EmbroiderInvoice_Category).ThenInclude(y=>y.Category));
        }

        public async Task<List<EmbroiderInvoiceDetail>> GetEmbroiderInvoiceDetailByCriteriaAsync(
        Expression<Func<EmbroiderInvoiceDetail, bool>> criteria)
        {
            List<EmbroiderInvoiceDetail> byCriteriaAsync = await this._repoDetailAsync.GetByCriteriaAsync((Func<IQueryable<EmbroiderInvoiceDetail>, IIncludableQueryable<EmbroiderInvoiceDetail, object>>)null, criteria);
            return byCriteriaAsync;
        }

        public async Task<List<EmbroiderInvoice>> GetListAllAsync()
        {
            List<EmbroiderInvoice> embroiderInvoiceList = await this._repoAsync.ListAllAsync();
            return embroiderInvoiceList;
        }

        public async Task<EmbroiderInvoice> SaveAsync(EmbroiderInvoice entity)
        {
            EmbroiderInvoice embroiderInvoice = await this._repoAsync.AddAsync(entity);
            return embroiderInvoice;
        }

        public async Task UpdateAsync(EmbroiderInvoice entity)
        {
            EmbroiderInvoice embroiderInvoice = await this._repoAsync.UpdateAsync(entity);
        }

        public async Task UpdateEmbroiderInvoiceDetails(IList<EmbroiderInvoiceDetail> entities) => await this._repoDetailAsync.UpdateListAsync((IEnumerable<EmbroiderInvoiceDetail>)entities);
    }
}
