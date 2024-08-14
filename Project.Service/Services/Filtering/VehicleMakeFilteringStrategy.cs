using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Services.Filtering
{
    public class VehicleMakeFilteringStrategy : IFilteringStrategy<VehicleMake>
    {
        public IQueryable<VehicleMake> ApplyFiltering(IQueryable<VehicleMake> query, string? searchString, int? selectedMakeId)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(m => m.Name.Contains(searchString) || m.Abrv.Contains(searchString));
            }
            return query;
        }
    }
}
