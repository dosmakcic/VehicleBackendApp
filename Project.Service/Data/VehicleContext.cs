using Microsoft.EntityFrameworkCore;
using Project.Service.Models;

namespace Project.Service.Data
{
    public class VehicleContext : DbContext
    {
        public VehicleContext(DbContextOptions<VehicleContext> options) :  base(options)
        { 
        
        }

        public DbSet<VehicleMake> VehicleMakes { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }

       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleMake>()
                         .HasMany(v => v.VehicleModels)
                         .WithOne(vm => vm.VehicleMake)
                         .HasForeignKey(vm => vm.MakeId)
                         .OnDelete(DeleteBehavior.Cascade);




            modelBuilder.Entity<VehicleModel>()
                        .HasOne(v => v.VehicleMake)
                        .WithMany(m => m.VehicleModels)
                        .HasForeignKey(v => v.MakeId);


            modelBuilder.Entity<VehicleMake>()
                        .HasData(
          new VehicleMake { Id = 1, Name = "Bayerische Motoren Werke", Abrv = "BMW" },
          new VehicleMake { Id = 2, Name = "Ford", Abrv = "Ford" },
          new VehicleMake { Id = 3, Name = "Volkswagen", Abrv = "VW" }
                                 );

            modelBuilder.Entity<VehicleModel>()
                        .HasData(
            new VehicleModel { Id = 1, MakeId = 1, Name = "BMW X3", Abrv = "X3" },
            new VehicleModel { Id = 3, MakeId = 1, Name = "X5", Abrv = "X5" },
            new VehicleModel { Id = 4, MakeId = 2, Name = "Mustang", Abrv = "MST" },
            new VehicleModel { Id = 5, MakeId = 2, Name = "Explorer", Abrv = "EXP" },
            new VehicleModel { Id = 6, MakeId = 3, Name = "Golf", Abrv = "GLF" },
            new VehicleModel { Id = 7, MakeId = 3, Name = "Polo", Abrv = "PLO" }
                               );



        }


    }
}
