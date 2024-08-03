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
                .ForMember(dest => dest.Id, opt => opt.Ignore()); 

        
            CreateMap<VehicleMake, VehicleMakeViewModel>()
                .ForMember(dest => dest.MakeId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.MakeName, opt => opt.MapFrom(src => src.Name));


            CreateMap<VehicleModelViewModel, VehicleModel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.MakeId, opt => opt.MapFrom(src => src.MakeId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Abrv, opt => opt.MapFrom(src => src.Abrv));

            CreateMap<VehicleModel, VehicleModelViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.MakeId, opt => opt.MapFrom(src => src.MakeId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Abrv, opt => opt.MapFrom(src => src.Abrv));




        }
        

    }
}
