using AutoMapper;
using Project.MVC.Models;
using Project.Service.Models;

namespace Project.MVC
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<VehicleMakeViewModel, VehicleMake>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()); // Ignoriraj Id jer se generira u bazi

        // Mapiranje iz modela u ViewModel
        CreateMap<VehicleMake, VehicleMakeViewModel>()
            .ForMember(dest => dest.MakeId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.MakeName, opt => opt.MapFrom(src => src.Name));
    
        }
        

    }
}
