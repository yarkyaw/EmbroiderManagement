using AutoMapper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using EmbroiderData.DTO;
using EmbroiderManagement;
using EmbroiderManagementSystem.Helpers;
using EmbroideryData;
using EmbroideryData.Interface;
using EmbroideryService.Interface;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmbroiderManagementSystem.Controllers
{
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class EmbroiderController : ControllerBase
    {
        #region Fields

        private readonly IAccountManager _accountManager;

        private readonly IAuthorizationService _authorizationService;

        private readonly IEmbroiderService _embroiderService;

        private readonly ILogger<AccountController> _logger;

        private readonly IMapper _mapper;

        #endregion

        #region Constructors

        public EmbroiderController(
      IMapper mapper,
      IAccountManager accountManager,
      IAuthorizationService authorizationService,
      IEmbroiderService embroiderService,
      ILogger<AccountController> logger)
        {
            this._mapper = mapper;
            this._accountManager = accountManager;
            this._authorizationService = authorizationService;
            this._logger = logger;
            this._embroiderService = embroiderService;
        }

        #endregion

        #region Methods

        [HttpPost("delete")]
        public async Task<object> DeleteEmbroider(Embroider embroider)
        {
            object obj;
            try
            {
                Embroider entity = await this._embroiderService.GetById(embroider.Id);
                await this._embroiderService.DeleteAsync(entity);
                obj = (object)true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }

        [HttpGet("getAll")]
        public async Task<object> GetAllEmbroiders()
        {
            List<Embroider> listAllAsync = await this._embroiderService.GetListAllAsync();
            return (object)listAllAsync;
        }

        [HttpGet("getPagination")]
        public async Task<object> GetEmbroiderPagination(DataSourceLoadOptions loadOptions)
        {
            IQueryable<EmbroiderDTO> source = await this._embroiderService.GetEmbroiderDTOQueryable();
            LoadResult result = DataSourceLoader.Load(source, loadOptions);

            return result;
        }

        [HttpGet("hasDuplicateCode")]
        public async Task<bool> HasEmbroiderDuplicateCode(string oldVal, string newVal)
        {
            if (!string.IsNullOrEmpty(oldVal))
            {
                if ((await _embroiderService.GetByCriteriaAsync(x => x.EmbroiderCode == newVal)).Any())
                {
                    return !(await _embroiderService.GetByCriteriaAsync(x => x.EmbroiderCode == newVal && x.EmbroiderCode == oldVal)).Any();
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return (await _embroiderService.GetByCriteriaAsync(x => x.EmbroiderCode == newVal)).Any();
            }
        }

        [HttpPost("save")]
        public async Task<Embroider> SaveOrUpdateGroup([FromBody] Embroider embroider)
        {
            if (embroider.Id > 0)
            {
                string userId = Utilities.GetUserId(this.User);
                embroider.UpdatedOn = DateTimeOffset.Now;
                embroider.UpdatedBy = userId.ToString();
                await this._embroiderService.UpdateAsync(embroider);
                return embroider;
            }
            string userId1 = Utilities.GetUserId(this.User);
            embroider.CreatedBy = userId1.ToString();
            embroider.CreatedOn = DateTimeOffset.Now;
            embroider.UpdatedOn = DateTimeOffset.Now;
            Embroider embroider1 = await this._embroiderService.SaveAsync(embroider);
            return embroider;
        }

        #endregion
    }
}
