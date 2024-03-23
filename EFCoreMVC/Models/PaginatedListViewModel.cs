using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace EFCoreMVC.Models
{
    public class PaginatedListViewModel<T>:List<T>
    {
        public int TotalCount { get; private set; }
        public int PageIndex { get; private set; }
        public int PrevPageIndex { get; private set; }
        public int NextPageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public bool HasPrevious {  get; private set; }
        public bool HasNext { get; private set; }
        public string PrevDisabledAttr => HasPrevious? string.Empty : "disabled";
        public string NextDisabledAttr => HasNext? string.Empty : "disabled";

        public PaginatedListViewModel(List<T> items, int totalCount, int pageIndex, int pageSize = 10) {
            AddRange(items);
            TotalCount = totalCount;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(totalCount/(double)pageSize);
            HasPrevious = PageIndex > 1;
            HasNext = PageIndex < TotalPages;
            PrevPageIndex = HasPrevious ? PageIndex - 1 : 1;
            NextPageIndex = HasNext ? PageIndex + 1 : PageIndex;
        }
        public static async Task<PaginatedListViewModel<T>> FromQueryAsync(IQueryable<T> source, int pageIndex, int pageSize = 10)
        {
            int count = await source.CountAsync();
            pageIndex = Math.Max(pageIndex, 1);
            pageSize = Math.Max(pageSize, 1);
            return new(await source.Skip(pageSize*(pageIndex-1)).Take(pageSize).ToListAsync(), count, pageIndex, pageSize);
        }
    }
}
