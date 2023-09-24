namespace MvcTraining.Models
{
    public class DataTablesRequest
    {
        public string Draw { get; set; }
        public string Start { get; set; }
        public string Length { get; set; }
        public string SortColumn { get; set; }
        public string SortColumnDirection { get; set; }
        public int PageSize { get; set; }
        public int Skip { get; set; }
        public string Search { get; set; }
    }
}
