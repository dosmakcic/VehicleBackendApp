using Microsoft.EntityFrameworkCore;
using Project.Service.GetParameters;
using Project.Service.Data;
using Project.Service.Models;
using Project.Service.Services.Filtering;
using Project.Service.Services.Pagination;
using Project.Service.Services.Sorting;

namespace Project.Service.Services
{
    public class MakeService : IMakeService
    {

        private readonly VehicleContext _context;
        private readonly ISortingStrategy<VehicleMake> _makeSortingStrategy;
        private readonly IFilteringStrategy<VehicleMake> _makeFilteringStrategy;
        private readonly IPaginationStrategy<VehicleMake> _makePaginationStrategy;

        public MakeService(VehicleContext context, ISortingStrategy<VehicleMake> makeSortingStrategy,
        IFilteringStrategy<VehicleMake> makeFilteringStrategy,
        IPaginationStrategy<VehicleMake> makePaginationStrategy,
        ISortingStrategy<VehicleModel> modelSortingStrategy,
        IFilteringStrategy<VehicleModel> modelFilteringStrategy,
        IPaginationStrategy<VehicleModel> modelPaginationStrategy)
        {
            _context = context;
            _makeSortingStrategy = makeSortingStrategy;
            _makeFilteringStrategy = makeFilteringStrategy;
            _makePaginationStrategy = makePaginationStrategy;
            
        }


        public async Task<PaginatedList<VehicleMake>> GetAllMakesAsync(MakeGetParameters? makeGetParameters)
        {
            var parameters = makeGetParameters ?? new MakeGetParameters(); 
            var query = _context.VehicleMakes.AsQueryable();

            query = _makeFilteringStrategy.ApplyFiltering(query, parameters.SearchString, null);
            query = _makeSortingStrategy.ApplySorting(query, parameters.SortOrder);
            return await _makePaginationStrategy.ApplyPaginationAsync(query, parameters.PageNumber ?? 1, parameters.PageSize);
        }



        public async Task<List<VehicleMake>> TestGetMakes()
        {
            var makes = await _context.VehicleMakes.ToListAsync();

            return makes;
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

    }
}
