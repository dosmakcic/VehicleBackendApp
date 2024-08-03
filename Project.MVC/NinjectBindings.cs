using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ninject.Modules;
using Project.MVC;
using Project.Service.Data;


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

            
            Bind<IVehicleService>().To<VehicleService>().InTransientScope();

         
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

