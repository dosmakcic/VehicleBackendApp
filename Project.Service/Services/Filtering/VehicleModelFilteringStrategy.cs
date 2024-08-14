using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Services.Filtering
{
    public class VehicleModelFilteringStrategy : IFilteringStrategy<VehicleModel>
    {
        public IQueryable<VehicleModel> ApplyFiltering(IQueryable<VehicleModel> query, string? searchString, int? selectedMakeId)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(m => m.Name.Contains(searchString) || m.Abrv.Contains(searchString));
            }

            if (selectedMakeId.HasValue)
            {
                query = query.Where(m => m.MakeId == selectedMakeId.Value);
            }
            return query;
        }
    }
}
