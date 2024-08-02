using AutoMapper;
using EmbroiderData;
using EmbroideryData;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EmbroiderManagementSystem.ViewModels
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ApplicationUser, UserViewModel>().ForMember<string[]>((Expression<Func<UserViewModel, string[]>>)(d => d.Roles), (Action<IMemberConfigurationExpression<ApplicationUser, UserViewModel, string[]>>)(map => map.Ignore()));
            CreateMap<UserViewModel, ApplicationUser>().ForMember<ICollection<IdentityUserRole<string>>>((Expression<Func<ApplicationUser, ICollection<IdentityUserRole<string>>>>)(d => d.Roles), (Action<IMemberConfigurationExpression<UserViewModel, ApplicationUser, ICollection<IdentityUserRole<string>>>>)(map => map.Ignore())).ForMember<string>((Expression<Func<ApplicationUser, string>>)(d => d.Id), (Action<IMemberConfigurationExpression<UserViewModel, ApplicationUser, string>>)(map => map.Condition((Func<UserViewModel, bool>)(src => src.Id != null))));
            CreateMap<ApplicationUser, UserEditViewModel>().ForMember<string[]>((Expression<Func<UserEditViewModel, string[]>>)(d => d.Roles), (Action<IMemberConfigurationExpression<ApplicationUser, UserEditViewModel, string[]>>)(map => map.Ignore()));
            CreateMap<UserEditViewModel, ApplicationUser>().ForMember<ICollection<IdentityUserRole<string>>>((Expression<Func<ApplicationUser, ICollection<IdentityUserRole<string>>>>)(d => d.Roles), (Action<IMemberConfigurationExpression<UserEditViewModel, ApplicationUser, ICollection<IdentityUserRole<string>>>>)(map => map.Ignore())).ForMember<string>((Expression<Func<ApplicationUser, string>>)(d => d.Id), (Action<IMemberConfigurationExpression<UserEditViewModel, ApplicationUser, string>>)(map => map.Condition((Func<UserEditViewModel, bool>)(src => src.Id != null))));
            CreateMap<ApplicationUser, UserPatchViewModel>().ReverseMap();
            CreateMap<ApplicationRole, RoleViewModel>().ForMember<PermissionViewModel[]>((Expression<Func<RoleViewModel, PermissionViewModel[]>>)(d => d.Permissions), (Action<IMemberConfigurationExpression<ApplicationRole, RoleViewModel, PermissionViewModel[]>>)(map => map.MapFrom<ICollection<IdentityRoleClaim<string>>>((Expression<Func<ApplicationRole, ICollection<IdentityRoleClaim<string>>>>)(s => s.Claims)))).ForMember<int>((Expression<Func<RoleViewModel, int>>)(d => d.UsersCount), (Action<IMemberConfigurationExpression<ApplicationRole, RoleViewModel, int>>)(map => map.MapFrom<int>((Expression<Func<ApplicationRole, int>>)(s => s.Users != default(object) ? s.Users.Count : 0)))).ReverseMap();
            CreateMap<RoleViewModel, ApplicationRole>().ForMember<string>((Expression<Func<ApplicationRole, string>>)(d => d.Id), (Action<IMemberConfigurationExpression<RoleViewModel, ApplicationRole, string>>)(map => map.Condition((Func<RoleViewModel, bool>)(src => src.Id != null))));
            CreateMap<IdentityRoleClaim<string>, ClaimViewModel>().ForMember<string>((Expression<Func<ClaimViewModel, string>>)(d => d.Type), (Action<IMemberConfigurationExpression<IdentityRoleClaim<string>, ClaimViewModel, string>>)(map => map.MapFrom<string>((Expression<Func<IdentityRoleClaim<string>, string>>)(s => s.ClaimType)))).ForMember<string>((Expression<Func<ClaimViewModel, string>>)(d => d.Value), (Action<IMemberConfigurationExpression<IdentityRoleClaim<string>, ClaimViewModel, string>>)(map => map.MapFrom<string>((Expression<Func<IdentityRoleClaim<string>, string>>)(s => s.ClaimValue)))).ReverseMap();
            CreateMap<ApplicationPermission, PermissionViewModel>().ReverseMap();
            CreateMap<IdentityRoleClaim<string>, PermissionViewModel>().ConvertUsing((Expression<Func<IdentityRoleClaim<string>, PermissionViewModel>>)(s => (PermissionViewModel)ApplicationPermissions.GetPermissionByValue(s.ClaimValue)));
            

            CreateMap<Embroider, EmbroiderModel>()
                .ForMember(x => x.Balance, y => y.MapFrom(z => z.Balance));
            CreateMap<EmbroiderModel, Embroider>()
                .IgnoreAllPropertiesWithAnInaccessibleSetter();


            CreateMap<EmbroiderOrder, EmbroiderOrderModel>()
                .ForMember(x => x.CategoryId, y => y.MapFrom(z => z.EmbroiderOrder_Category.CategoryId))
                .ForMember(x => x.EmbroiderId, y => y.MapFrom(z => z.EmbroiderOrder_Embroider.EmbroiderId))
                .ForMember(x => x.ProductWeightId, y => y.MapFrom(z => z.EmbroiderOrder_ProductWeight.ProductWeightId))
                .ForMember(x=>x.OrderDetails,y=>y.MapFrom(z=>z.OrderDetails));

            CreateMap<EmbroiderOrderModel, EmbroiderOrder>();

            CreateMap<EmbroiderOrderDetail, EmbroiderOrderDetailModel>()
                .ForMember(x=>x.Ratio,y=>y.MapFrom(z=>z.Ratio))
                .ForMember(x => x.SubCategoryId, y => y.MapFrom(z => z.EmbroiderOrderDetail_SubCategory.SubCategoryId));

            CreateMap<EmbroiderOrderDetailModel, EmbroiderOrderDetail>()
                .ForMember(x => x.EmbroiderOrderDetail_SubCategory, y => y.MapFrom(z => new EmbroiderOrderDetail_SubCategory {SubCategoryId= z.SubCategoryId,OrderDetailId=z.Id }));


            CreateMap<ProductGroup, ProductGroupModel>().ReverseMap();
            CreateMap<Category, CategoryModel>().ReverseMap();
            CreateMap<SubCategory, SubCategoryModel>().ReverseMap();
            CreateMap<ProductWeight, ProductWeightModel>().ReverseMap();

           

            CreateMap<EmbroiderInvoice, EmbroiderInvoiceModel>()
                .ForMember(x => x.CategoryId, y => y.MapFrom(z => z.EmbroiderInvoice_Category.CategoryId))
                .ForMember(x => x.EmbroiderId, y => y.MapFrom(z => z.EmbroiderInvoice_Embroider.EmbroiderId))
                .ForMember(x => x.ProductWeightId, y => y.MapFrom(z => z.EmbroiderInvoice_ProductWeight.ProductWeightId))
                .ForMember(x => x.Order, y => y.MapFrom(z => z.EmbroiderOrder_EmbroiderInvoice.EmbroiderOrder))
                .ForMember(x => x.InvoiceDetails, y => y.MapFrom(z => z.InvoiceDetails));
            
            CreateMap<EmbroiderInvoiceModel, EmbroiderInvoice>()
                .ForMember(x => x.EmbroiderOrder_EmbroiderInvoice, y => y.MapFrom(z => new EmbroiderOrder_EmbroiderInvoice { OrderId = z.OrderId, InvoiceId = z.Id }))
                .ForMember(x => x.EmbroiderInvoice_Category, y => y.MapFrom(z => new EmbroiderInvoice_Category { CategoryId=z.CategoryId,InvoiceId=z.Id }))
                .ForMember(x => x.EmbroiderInvoice_Embroider, y => y.MapFrom(z => new EmbroiderInvoice_Embroider { EmbroiderId=z.EmbroiderId,InvoiceId=z.Id }))
                .ForMember(x => x.EmbroiderInvoice_ProductWeight, y => y.MapFrom(z => new EmbroiderInvoice_ProductWeight {ProductWeightId=z.ProductWeightId,InvoiceId=z.Id }))
                .ForMember(x => x.InvoiceDetails, y => y.MapFrom(z => z.InvoiceDetails));


            CreateMap<EmbroiderInvoiceDetail, EmbroiderInvoiceDetailModel>()
                .ForMember(x => x.SubCategoryId, y => y.MapFrom(z => z.EmbroiderInvoiceDetail_SubCategory.SubCategoryId));

            CreateMap<EmbroiderInvoiceDetailModel, EmbroiderInvoiceDetail>()
                .ForMember(x=>x.Description,y=>y.MapFrom(z=>string.IsNullOrEmpty(z.Description)?"":z.Description))
                .ForMember(x => x.EmbroiderInvoiceDetail_SubCategory, y => y.MapFrom(z => new EmbroiderInvoiceDetail_SubCategory { SubCategoryId = z.SubCategoryId, InvoiceDetailId = z.Id }));

            
            CreateMap<EmbroiderOrder_EmbroiderInvoice, EmbroiderOrderInvoiceModel>().ReverseMap();
        }
    }
}
