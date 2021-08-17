using FlowersStore.WebUI.ViewModels;
using AutoMapper;

namespace FlowersStore.WebUI
{
    public class WebUIMappingProfile : Profile
    {
        public WebUIMappingProfile()
        {
            CreateMap<Core.CoreModels.ProductCard, ProductCardViewModel>();
            CreateMap<Core.CoreModels.Product, ProductViewModel>();

            CreateMap<Core.CoreModels.Product, Contracts.Product>().ReverseMap();
            CreateMap<Core.CoreModels.ProductCard, Contracts.ProductCard>().ReverseMap();
            CreateMap<Core.CoreModels.Category, Contracts.Category>().ReverseMap();
            CreateMap<Core.CoreModels.User, Contracts.User>().ReverseMap();
            CreateMap<Core.CoreModels.User, DataAccess.MSSQL.Entities.User>().ReverseMap();
            CreateMap<Core.CoreModels.Basket, Contracts.Basket>().ReverseMap();
        }
    }
}