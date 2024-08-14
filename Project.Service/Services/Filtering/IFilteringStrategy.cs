namespace Project.Service.Services.Filtering
{
    public interface IFilteringStrategy<T>
    {
        IQueryable<T> ApplyFiltering(IQueryable<T> query, string? searchString, int? selectedMakeId);

    }
}
