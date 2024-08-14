namespace Project.Service.GetParameters
{
    public class ModelGetParameters
    {
        public int PageSize { get; private set; } = 10;
        public string? SortOrder { get; set; }
        public string? SearchString { get; set; }
        public int? PageNumber { get; set; }
        public int? SelectedMakeId { get; set; }

    }
}
