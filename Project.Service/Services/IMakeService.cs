using Project.Service.GetParameters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Services
{
    public interface IMakeService
    {
        Task<PaginatedList<VehicleMake>> GetAllMakesAsync(MakeGetParameters makeGetParameters);

        Task<List<VehicleMake>> TestGetMakes();
        Task<VehicleMake> GetMakeByIdAsync(int id);
        Task InsertMakeAsync(VehicleMake make);
        Task UpdateMakeAsync(VehicleMake make);
        Task DeleteMakeAsync(int id);
       
    }
}
