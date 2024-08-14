using Microsoft.EntityFrameworkCore;
using Project.Service.GetParameters;
using Project.Service.Data;
using Project.Service.Models;
using Project.Service.Services.Filtering;
using Project.Service.Services.Pagination;
using Project.Service.Services.Sorting;

namespace Project.Service.Services
{
    public class ModelService : IModelService
    {
        private readonly VehicleContext _context;
        private readonly ISortingStrategy<VehicleModel> _modelSortingStrategy;
        private readonly IFilteringStrategy<VehicleModel> _modelFilteringStrategy;
        private readonly IPaginationStrategy<VehicleModel> _modelPaginationStrategy;



        public ModelService(
        VehicleContext context, 
        ISortingStrategy<VehicleModel> modelSortingStrategy,
        IFilteringStrategy<VehicleModel> modelFilteringStrategy,
        IPaginationStrategy<VehicleModel> modelPaginationStrategy)
        {
            _context = context;
            _modelSortingStrategy = modelSortingStrategy;
            _modelFilteringStrategy = modelFilteringStrategy;
            _modelPaginationStrategy = modelPaginationStrategy;
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



        public async Task<PaginatedList<VehicleModel>> GetAllModelsAsync(ModelGetParameters? modelGetParameters)
        {
            var parameters = modelGetParameters ?? new ModelGetParameters();
            var query = _context.VehicleModels.AsQueryable();


            query = _modelFilteringStrategy.ApplyFiltering(query, parameters.SearchString, parameters.SelectedMakeId);

            query = _modelSortingStrategy.ApplySorting(query, parameters.SortOrder);


            return await _modelPaginationStrategy.ApplyPaginationAsync(query, parameters.PageNumber ?? 1, parameters.PageSize);
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
