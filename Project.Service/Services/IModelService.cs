using Project.Service.GetParameters;
using Project.Service.Models;

namespace Project.Service.Services
{



    public interface IModelService
    {
       

        Task<List<VehicleModel>> GetModelsByMakeIdAsync(int? makeId);

        Task<PaginatedList<VehicleModel>> GetAllModelsAsync(ModelGetParameters modelGetParameters);
        Task<VehicleModel> GetModelByIdAsync(int id);
        Task InsertModelAsync(VehicleModel model);
        Task UpdateModelAsync(VehicleModel model);
        Task DeleteModelAsync(int id);
    }

}
