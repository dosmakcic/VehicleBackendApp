using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Service.Models;

namespace Project.Service.Service
{
    
    

    public interface IVehicleService
    {
        Task<IEnumerable<VehicleMake>> GetAllMakesAsync(string sortOrder, string searchString, int? pageNumber, int pageSize);
        Task<VehicleMake> GetMakeByIdAsync(int id);
        Task InsertMakeAsync(VehicleMake make);
        Task UpdateMakeAsync(VehicleMake make);
        Task DeleteMakeAsync(int id);

        Task<IEnumerable<VehicleModel>> GetAllModelsAsync(string sortOrder, string searchString, int? pageNumber, int pageSize);
        Task<VehicleModel> GetModelByIdAsync(int id);
        Task InsertModelAsync(VehicleModel model);
        Task UpdateModelAsync(VehicleModel model);
        Task DeleteModelAsync(int id);
    }

}
