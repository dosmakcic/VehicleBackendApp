using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ninject.Modules;
using Project.MVC;
using Project.Service.Data;
using Project.Service.Models;
using Project.Service.Services.Filtering;
using Project.Service.Services.Pagination;
using Project.Service.Services.Sorting;


namespace Project.Service.Services
{
    public class NinjectBindings : NinjectModule
    {
        private readonly string _connectionString;

        public NinjectBindings(string connectionString)
        {
            _connectionString = connectionString;
        }

        public override void Load()
        {
      
            Bind<VehicleContext>().ToSelf().InSingletonScope().WithConstructorArgument("options", options =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<VehicleContext>();
                optionsBuilder.UseSqlServer(_connectionString)
                              .EnableSensitiveDataLogging();
                return optionsBuilder.Options;
            });



            Bind<IMakeService>().To<MakeService>().InTransientScope();
            Bind<IModelService>().To<ModelService>().InTransientScope();



            Bind<ISortingStrategy<VehicleMake>>()
            .To<VehicleMakeSorting>()
            .InSingletonScope();

            Bind<IFilteringStrategy<VehicleMake>>()
                .To<VehicleMakeFilteringStrategy>()
                .InSingletonScope();

            Bind<IPaginationStrategy<VehicleMake>>()
                .To<PaginationStrategy<VehicleMake>>()
                .InSingletonScope();

            Bind<ISortingStrategy<VehicleModel>>()
                .To<VehicleModelSorting>()
                .InSingletonScope();

            Bind<IFilteringStrategy<VehicleModel>>()
                .To<VehicleModelFilteringStrategy>()
                .InSingletonScope();

            Bind<IPaginationStrategy<VehicleModel>>()
                .To<PaginationStrategy<VehicleModel>>()
                .InSingletonScope();



            Bind<IMapper>().ToMethod(ctx =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<MappingProfile>();
                });
                return config.CreateMapper();
            }).InSingletonScope();
        }
    }
}

