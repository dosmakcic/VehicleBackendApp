using Microsoft.EntityFrameworkCore;
using Project.Service.Models;
using Project.Service.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly VehicleContext _context;

        public VehicleService(VehicleContext context)
        {
            _context = context;
        }



        public async Task<PaginatedList<VehicleMake>> GetAllMakesAsync(string sortOrder, string searchString, int? pageNumber, int pageSize)
        {
            var query = _context.VehicleMakes.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(m => m.Name.Contains(searchString) || m.Abrv.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    query = query.OrderByDescending(m => m.Name);
                    break;
                case "abrv":
                    query = query.OrderBy(m => m.Abrv);
                    break;
                case "abrv_desc":
                    query = query.OrderByDescending(m => m.Abrv);
                    break;
                default:
                    query = query.OrderBy(m => m.Name);
                    break;
            }

            return await PaginatedList<VehicleMake>.CreateAsync(query.AsNoTracking(), pageNumber ?? 1, pageSize);
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


        public async Task<PaginatedList<VehicleModel>> GetAllModelsAsync(string sortOrder, string searchString, int? pageNumber, int pageSize)
        {
            var query = _context.VehicleModels.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(m => m.Name.Contains(searchString) || m.Abrv.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    query = query.OrderByDescending(m => m.Name);
                    break;
                case "abrv":
                    query = query.OrderBy(m => m.Abrv);
                    break;
                case "abrv_desc":
                    query = query.OrderByDescending(m => m.Abrv);
                    break;
                default:
                    query = query.OrderBy(m => m.Name);
                    break;
            }

            return await PaginatedList<VehicleModel>.CreateAsync(query.AsNoTracking(), pageNumber ?? 1, pageSize);
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
