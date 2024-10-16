using AutoMapper;
using Ecommerce2.Models;
using Ecommerce2.ViewModels;

namespace Ecommerce2.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterVM, KhachHang>();
            /*.ForMember(
                kh => kh.HoTen, option => option.MapFrom(
                    RegisterVM => RegisterVM.HoTen)).ReverseMap();*/
        }
    }
}
