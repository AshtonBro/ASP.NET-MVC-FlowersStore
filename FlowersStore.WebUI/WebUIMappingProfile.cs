using FlowersStore.WebUI.ViewModels;
using AutoMapper;

namespace FlowersStore.WebUI
{
    public class WebUIMappingProfile : Profile
    {
        public WebUIMappingProfile()
        {
            CreateMap<Core.CoreModels.ShopingCart, ShopingCartViewModel>();
            CreateMap<Core.CoreModels.Product, ProductViewModel>();
            CreateMap<Core.CoreModels.ShopingCart, Contracts.ShopingCart>().ReverseMap();
            CreateMap<Core.CoreModels.User, Contracts.User>().ReverseMap();
        }
    }
}