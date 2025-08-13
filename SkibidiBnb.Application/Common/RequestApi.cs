namespace SkibidiBnb.Application.Common
{
    public class RequestApi(int pageIndex = 0, int pageSize = 10)
    {
        public int PageIndex { get; set; } = pageIndex;
        public int PageSize { get; set; } = pageSize;
    }
}
    