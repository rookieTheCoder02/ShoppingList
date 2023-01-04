using AutoMapper;
using ShoppingList.Models.Domain;
using ShoppingList.Models.DTO;

namespace ShoppingList.Model.Profiles
{
    public class GoodsProfile : Profile
    {
        public GoodsProfile()
        {
            CreateMap<Goods, GoodsDTO>()
                .ReverseMap();
        }
    }
}
