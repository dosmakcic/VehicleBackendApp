using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Services.Pagination
{
    public interface IPaginationStrategy<T>
    {
        Task<PaginatedList<T>> ApplyPaginationAsync(IQueryable<T> query, int pageNumber, int pageSize);

    }
}
