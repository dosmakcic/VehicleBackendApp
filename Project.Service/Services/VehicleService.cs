using Microsoft.EntityFrameworkCore;
using Project.Service.Data;
using Project.Service.Models;

namespace Project.Service.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly VehicleContext _context;

        public VehicleService(VehicleContext context)
        {
            _context = context;
        }




        public async Task<List<VehicleMake>> TestGetMakes()
        {
            var makes = await _context.VehicleMakes.ToListAsync();

            return makes;
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
                case "name_asc":
                    query = query.OrderBy(m => m.Name);
                    break;
                case "abrv_desc":
                    query = query.OrderByDescending(m => m.Abrv);
                    break;
                case "abrv_asc":
                    query = query.OrderBy(m => m.Abrv);
                    break;
                default:
                    query = query.OrderBy(m => m.Name);
                    break;
            }

            return await PaginatedList<VehicleMake>.CreateAsync(query.AsNoTracking(), pageNumber ?? 1, pageSize);
        }


        public async Task<List<VehicleModel>> GetModelsByMakeIdAsync(int? makeId)
        {
            if (makeId.HasValue)
            {
                return await _context.VehicleModels
                                     .Where(vm => vm.MakeId == makeId)
                                     .ToListAsync();
            }
            else
            {
                return await _context.VehicleModels.ToListAsync();
            }
        }


        public async Task<VehicleMake?> GetMakeByIdAsync(int id)
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

            var existingMake = await _context.VehicleMakes.FindAsync(make.Id);

            if (existingMake == null)
            {
                throw new InvalidOperationException("Entity not found.");
            }

            existingMake.Name = make.Name;
            existingMake.Abrv = make.Abrv;

            
            _context.Entry(existingMake).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                
                throw new InvalidOperationException("Concurrency issue detected.", ex);
            }
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


        public async Task<PaginatedList<VehicleModel>> GetAllModelsAsync(int? selectedMakeId, string sortOrder, string searchString, int? pageNumber, int pageSize)
        {
            var query = _context.VehicleModels.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(m => m.Name.Contains(searchString) || m.Abrv.Contains(searchString));
            }


            if (selectedMakeId.HasValue)
            {
                query = query.Where(m => m.MakeId == selectedMakeId.Value);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    query = query.OrderByDescending(m => m.Name);
                    break;
                case "name_asc":
                    query = query.OrderBy(m => m.Name);
                    break;
                case "abrv_desc":
                    query = query.OrderByDescending(m => m.Abrv);
                    break;
                case "abrv_asc":
                    query = query.OrderBy(m => m.Abrv);
                    break;
                case "makeId_desc":
                    query = query.OrderByDescending(m => m.Id);  
                    break;
                case "makeId_asc":
                    query = query.OrderBy(m => m.Id);
                    break;
                default:
                    query = query.OrderBy(m => m.Name);
                    break;
            }

            return await PaginatedList<VehicleModel>.CreateAsync(query, pageNumber ?? 1, pageSize);
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
