using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Service.Models;

namespace Project.Service.Services
{
    
    

    public interface IVehicleService
    {
        Task<PaginatedList<VehicleMake>> GetAllMakesAsync(string sortOrder, string searchString, int? pageNumber, int pageSize);

        Task<List<VehicleMake>> TestGetMakes();
        Task<VehicleMake> GetMakeByIdAsync(int id);
        Task InsertMakeAsync(VehicleMake make);
        Task UpdateMakeAsync(VehicleMake make);
        Task DeleteMakeAsync(int id);

        Task<PaginatedList<VehicleModel>> GetAllModelsAsync(string sortOrder, string searchString, int? pageNumber, int pageSize);
        Task<VehicleModel> GetModelByIdAsync(int id);
        Task InsertModelAsync(VehicleModel model);
        Task UpdateModelAsync(VehicleModel model);
        Task DeleteModelAsync(int id);
    }

}
