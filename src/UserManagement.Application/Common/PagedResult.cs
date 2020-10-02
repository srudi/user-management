using System.Collections.Generic;

namespace UserManagement.Application.Common
{
    public class PagedResult<T>
    {
        public PagedResult(IEnumerable<T> result, PageInfo pageInfo)
        {
            Result = result;
            PageInfo = pageInfo;
        }

        public IEnumerable<T> Result { get; }
        public PageInfo PageInfo { get; }
    }
}
