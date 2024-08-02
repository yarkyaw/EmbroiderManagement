using EmbroiderData;
using EmbroiderData.DTO;
using EmbroiderOrderyService.Interface;
using EmbroideryData;
using EmbroideryData.DTO;
using EmbroideryRepo.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace EmbroiderOrderyService
{
    public class EmbroiderOrderService : IEmbroiderOrderService
    {
        /// <summary>
        /// Defines the _repoAsync.
        /// </summary>
        private readonly IAsyncRepository<EmbroiderOrder> _repoAsync;

        /// <summary>
        /// Defines the _repoViewAsync.
        /// </summary>
        private readonly IAsyncRepository<EmbroiderOrderView> _repoViewAsync;

        /// <summary>
        /// Defines the _repoDetailAsync.
        /// </summary>
        private readonly IAsyncRepository<EmbroiderOrderDetail> _repoDetailAsync;

        public EmbroiderOrderService(
          IAsyncRepository<EmbroiderOrder> repoAsync,
          IAsyncRepository<EmbroiderOrderView> repoViewAsync,
          IAsyncRepository<EmbroiderOrderDetail> repoDetailAsync)
        {
            this._repoAsync = repoAsync;
            this._repoViewAsync = repoViewAsync;
            this._repoDetailAsync = repoDetailAsync;
        }

        public async Task DeleteAsync(EmbroiderOrder entity) => await this._repoAsync.DeleteAsync(entity);

        public async Task<List<EmbroiderOrder>> GetAsyncWithInclude(
          Func<IQueryable<EmbroiderOrder>, IIncludableQueryable<EmbroiderOrder, object>> include,
          Expression<Func<EmbroiderOrder, bool>> criteria,
          Func<IQueryable<EmbroiderOrder>, IOrderedQueryable<EmbroiderOrder>> orderBy = null,
          bool noTrack = true,
          CancellationToken cancellationToken = default(CancellationToken))
        {
            List<EmbroiderOrder> byCriteriaAsync = await this._repoAsync.GetByCriteriaAsync(include, criteria, orderBy, noTrack);
            return byCriteriaAsync;
        }

        public async Task<List<EmbroiderOrder>> GetByCriteriaAsync(
        Expression<Func<EmbroiderOrder, bool>> criteria)
        {
            List<EmbroiderOrder> byCriteriaAsync = await this._repoAsync.GetByCriteriaAsync((Func<IQueryable<EmbroiderOrder>, IIncludableQueryable<EmbroiderOrder, object>>)null, criteria);
            return byCriteriaAsync;
        }

        public async Task<EmbroiderOrder> GetById(int id)
        {
            EmbroiderOrder byIdAsync = await this._repoAsync.GetByIdAsync(id);
            return byIdAsync;
        }

        public async Task<IQueryable<EmbroiderOrderDTO>> GetDTOQueryable()
        {
            return await _repoAsync.GetWithSelectorAsync<EmbroiderOrderDTO>(x => new EmbroiderOrderDTO
            {
                Id = x.Id,
                OrderDate = x.OrderDate,
                OrderType = x.OrderType,
                OrderNo = x.OrderNo,
                ProductWeightName = x.EmbroiderOrder_ProductWeight.ProductWeight.LocalizeName,
                EmbroiderCode=x.EmbroiderOrder_Embroider.Embroider.EmbroiderCode,
                EmbroiderName=x.EmbroiderOrder_Embroider.Embroider.Name,
                EmbroiderId = x.EmbroiderOrder_Embroider.EmbroiderId,
                OrderStatus=x.OrderStatus,
                CategoryName=x.EmbroiderOrder_Category.Category.Name,
                OrderDetails = x.OrderDetails.AsQueryable().Select(z => new EmbroiderOrderDetailDTO
                {
                    Description = z.MaterialType== MaterialType.Gold? z.EmbroiderOrderDetail_SubCategory.SubCategory.Name :z.Description,
                    MaterialType = z.MaterialType,
                    OrderId = z.OrderId,
                    Quantity = z.Quantity,
                    Ratio = z.Quantity,
                    SubCategoryId = z.EmbroiderOrderDetail_SubCategory.SubCategoryId
                }).ToList()


            }, x => x.Include(y => y.EmbroiderOrder_ProductWeight).ThenInclude(z => z.ProductWeight).Include(z => z.OrderDetails).ThenInclude(y => y.EmbroiderOrderDetail_SubCategory).ThenInclude(z=>z.SubCategory).Include(x=>x.EmbroiderOrder_Category).ThenInclude(x=>x.Category).Include(x=>x.EmbroiderOrder_Embroider).ThenInclude(x=>x.Embroider));
        }

        public async Task<List<EmbroiderOrderDetail>> GetEmbroiderOrderDetailByCriteriaAsync(
      Expression<Func<EmbroiderOrderDetail, bool>> criteria)
        {
            List<EmbroiderOrderDetail> byCriteriaAsync = await this._repoDetailAsync.GetByCriteriaAsync((Func<IQueryable<EmbroiderOrderDetail>, IIncludableQueryable<EmbroiderOrderDetail, object>>)null, criteria);
            return byCriteriaAsync;
        }

        public async Task<IQueryable<EmbroiderOrderView>> GetEmbroiderOrderViewQueryable()
        {
            IQueryable<EmbroiderOrderView> quearableAsync = await this._repoViewAsync.GetQuearableAsync();
            return quearableAsync;
        }

        public async Task<List<EmbroiderOrder>> GetListAllAsync()
        {
            List<EmbroiderOrder> embroiderOrderList = await this._repoAsync.ListAllAsync();
            return embroiderOrderList;
        }

        public async Task<EmbroiderOrder> SaveAsync(EmbroiderOrder entity)
        {
            EmbroiderOrder embroiderOrder = await this._repoAsync.AddAsync(entity);
            return embroiderOrder;
        }

        public async Task UpdateAsync(EmbroiderOrder entity)
        {
            EmbroiderOrder embroiderOrder = await this._repoAsync.UpdateAsync(entity);
        }

        public async Task UpdateEmbroiderOrderDetails(IList<EmbroiderOrderDetail> entities) => await this._repoDetailAsync.UpdateListAsync((IEnumerable<EmbroiderOrderDetail>)entities);
    }
}
