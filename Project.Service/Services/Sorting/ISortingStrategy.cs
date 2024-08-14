using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Services.Sorting
{
    public interface ISortingStrategy<T>
    {
        IQueryable<T> ApplySorting(IQueryable<T> query, string? sortOrder);

    }
}
