namespace NanoviConference.Catalog
{
    public class PagingRequest
    {
        public string SortOrder { get; set; }
        public string Keyword { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
