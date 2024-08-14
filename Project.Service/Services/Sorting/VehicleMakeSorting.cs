using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Services.Sorting
{
    public class VehicleMakeSorting : ISortingStrategy<VehicleMake>
    {
        public  IQueryable<VehicleMake> ApplySorting(IQueryable<VehicleMake> query, string? sortOrder)
        {
            switch (sortOrder)
            {
                case "name_desc":
                    return query.OrderByDescending(m => m.Name);
                case "name_asc":
                    return query.OrderBy(m => m.Name);
                case "abrv_desc":
                    return query.OrderByDescending(m => m.Abrv);
                case "abrv_asc":
                    return query.OrderBy(m => m.Abrv);
                default:
                    return query.OrderBy(m => m.Name);
            }
        }
    }
}
