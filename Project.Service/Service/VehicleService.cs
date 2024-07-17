using Microsoft.EntityFrameworkCore;
using Project.Service.Models;
using Project.Service.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Service
{
    public class VehicleService : IVehicleService
    {
        private readonly VehicleContext _context;

        public VehicleService(VehicleContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VehicleMake>> GetAllMakesAsync(string sortOrder, string searchString, int? pageNumber, int pageSize)
        {
            var makes = from m in _context.VehicleMakes
                        select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                makes = makes.Where(m => m.Name.Contains(searchString) || m.Abrv.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    makes = makes.OrderByDescending(m => m.Name);
                    break;
                default:
                    makes = makes.OrderBy(m => m.Name);
                    break;
            }

            return await PaginatedList<VehicleMake>.CreateAsync(makes.AsNoTracking(), pageNumber ?? 1, pageSize);
        }

        public async Task<VehicleMake> GetMakeByIdAsync(int id)
        {
            return await _context.VehicleMakes.FindAsync(id);
        }

        public async Task InsertMakeAsync(VehicleMake make)
        {
            _context.VehicleMakes.Add(make);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMakeAsync(VehicleMake make)
        {
            _context.Entry(make).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMakeAsync(int id)
        {
            var make = await _context.VehicleMakes.FindAsync(id);
            if (make != null)
            {
                _context.VehicleMakes.Remove(make);
                await _context.SaveChangesAsync();
            }
        }

       
        public async Task<IEnumerable<VehicleModel>> GetAllModelsAsync(string sortOrder, string searchString, int? pageNumber, int pageSize)
        {
            var models = from m in _context.VehicleModels.Include(m => m.VehicleMake)
                         select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                models = models.Where(m => m.Name.Contains(searchString) || m.Abrv.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    models = models.OrderByDescending(m => m.Name);
                    break;
                default:
                    models = models.OrderBy(m => m.Name);
                    break;
            }

            return await PaginatedList<VehicleModel>.CreateAsync(models.AsNoTracking(), pageNumber ?? 1, pageSize);
        }

        public async Task<VehicleModel> GetModelByIdAsync(int id)
        {
            return await _context.VehicleModels.FindAsync(id);
        }

        public async Task InsertModelAsync(VehicleModel model)
        {
            _context.VehicleModels.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateModelAsync(VehicleModel model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteModelAsync(int id)
        {
            var model = await _context.VehicleModels.FindAsync(id);
            if (model != null)
            {
                _context.VehicleModels.Remove(model);
                await _context.SaveChangesAsync();
            }
        }
    }
}
