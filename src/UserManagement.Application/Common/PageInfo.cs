namespace UserManagement.Application.Common
{
    public class PageInfo
    {

        public PageInfo(int pageSize, int pageIndex)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
        }

        public int PageSize { get; }

        public int PageIndex { get; }

        public long TotalCount { get; set; }

        public int CalculateSkip() => PageIndex * PageSize;
    }
}
