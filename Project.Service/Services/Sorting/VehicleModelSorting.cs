using Project.Service.Models;

namespace Project.Service.Services.Sorting
{
    public  class VehicleModelSorting : ISortingStrategy<VehicleModel>
    {
        public IQueryable<VehicleModel> ApplySorting(IQueryable<VehicleModel> query, string? sortOrder)
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
                case "makeId_desc":
                    return query.OrderByDescending(m => m.MakeId);
                case "makeId_asc":
                    return query.OrderBy(m => m.MakeId);
                default:
                    return query.OrderBy(m => m.Name);
            }
        }
    }
}
