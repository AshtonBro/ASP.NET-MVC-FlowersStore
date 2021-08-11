using AutoMapper;

namespace FlowersStore.DataAccess.MSSQL
{
    public class DataAccessMappingProfile : Profile
    {
        public DataAccessMappingProfile()
        {
            CreateMap<Entities.Basket, Core.CoreModels.Basket>().ReverseMap();
            CreateMap<Entities.ShopingCart, Core.CoreModels.ShopingCart>().ReverseMap();
            CreateMap<Entities.Product, Core.CoreModels.Product>().ReverseMap();
            CreateMap<Entities.User, Core.CoreModels.User>().ReverseMap();
            CreateMap<Entities.UserRole, Core.CoreModels.UserRole>().ReverseMap();
        }
    }
}
