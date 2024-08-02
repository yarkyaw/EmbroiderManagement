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
    public class EmbroiderService : IEmbroiderService
    {
        #region Fields

        private readonly IAsyncRepository<Embroider> _repoAsync;

        #endregion

        #region Constructors

        public EmbroiderService(IAsyncRepository<Embroider> repoAsync) => this._repoAsync = repoAsync;

        #endregion

        #region Methods

        public async Task DeleteAsync(Embroider entity) => await this._repoAsync.DeleteAsync(entity);

        public async Task<List<Embroider>> GetAsyncWithInclude(
      Func<IQueryable<Embroider>, IIncludableQueryable<Embroider, object>> include,
      Expression<Func<Embroider, bool>> criteria,
      Func<IQueryable<Embroider>, IOrderedQueryable<Embroider>> orderBy = null,
      bool noTrack = true,
      CancellationToken cancellationToken = default(CancellationToken))
        {
            List<Embroider> byCriteriaAsync = await this._repoAsync.GetByCriteriaAsync(include, criteria, orderBy, noTrack);
            return byCriteriaAsync;
        }

        public async Task<List<Embroider>> GetByCriteriaAsync(
      Expression<Func<Embroider, bool>> criteria)
        {
            List<Embroider> byCriteriaAsync = await this._repoAsync.GetByCriteriaAsync((Func<IQueryable<Embroider>, IIncludableQueryable<Embroider, object>>)null, criteria);
            return byCriteriaAsync;
        }

        public async Task<Embroider> GetById(int id)
        {
            Embroider byIdAsync = await this._repoAsync.GetByIdAsync(id);
            return byIdAsync;
        }

        public async Task<Embroider> GetByIdWithInclude(int id)
        {
            Embroider entity = (await this._repoAsync.GetByCriteriaAsync(x => x.Include(y => y.EmbroiderInvoice_Embroideries).ThenInclude(z => z.EmbroiderInvoice), x => x.Id == id)).SingleOrDefault();
            return entity;
        }

        public async Task<IQueryable<Embroider>> GetEmbroiderQueryable()
        {
            IQueryable<Embroider> quearableAsync = await this._repoAsync.GetQuearableAsync();
            return quearableAsync;
        }

        public async Task<IQueryable<EmbroiderDTO>> GetEmbroiderDTOQueryable()
        {
            return await _repoAsync.GetWithSelectorAsync<EmbroiderDTO>(x => new EmbroiderDTO
            {
                Id = x.Id,
                Name=x.Name,
                OpeningBalance=x.OpeningBalance,
                Balance=((x.EmbroiderInvoice_Embroideries.Select(y=>y.EmbroiderInvoice).Sum(z=>z.ExcessOrLack))* -1)+x.OpeningBalance,
                InvoiceCount=x.EmbroiderInvoice_Embroideries.Count,
                EmbroiderCode=x.EmbroiderCode,
                Address=x.Address,
                Phone=x.Phone,
            }, x => x.Include(y => y.EmbroiderInvoice_Embroideries).ThenInclude(x=>x.EmbroiderInvoice));
        }

        public async Task<List<Embroider>> GetListAllAsync()
        {
            List<Embroider> embroiderList = await this._repoAsync.ListAllAsync();
            return embroiderList;
        }

        public async Task<Embroider> SaveAsync(Embroider entity)
        {
            Embroider embroider = await this._repoAsync.AddAsync(entity);
            return embroider;
        }

        public async Task UpdateAsync(Embroider entity)
        {
            Embroider embroider = await this._repoAsync.UpdateAsync(entity);
        }

        #endregion
    }
}
