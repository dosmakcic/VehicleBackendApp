using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Services.Pagination
{
    public class PaginationStrategy<T> : IPaginationStrategy<T>
    {
        public async Task<PaginatedList<T>> ApplyPaginationAsync(IQueryable<T> query, int pageNumber, int pageSize)
        {
            return await PaginatedList<T>.CreateAsync(query, pageNumber, pageSize);
        }
    }
}
