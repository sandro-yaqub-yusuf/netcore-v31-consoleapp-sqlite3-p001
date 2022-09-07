using AutoMapper;
using KITAB.Products.ConsoleApp.ViewModels;
using KITAB.Products.Domain.Models;

namespace KITAB.Products.ConsoleApp
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Product, ProductViewModel>().ReverseMap();
        }
    }
}
