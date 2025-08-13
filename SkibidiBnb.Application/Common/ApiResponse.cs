namespace SkibidiBnb.Application.Common
{
    public class ApiResponse<T>(int pageIndex, int pageSize, IEnumerable<T> data, int count)
    {
        public int PageIndex { get; set; } = pageIndex;
        public int PageSize { get; set; } = pageSize;
        public IEnumerable<T> Data { get; set; } = data;
        public int Count { get; set; } = count;
    }
}
