using Manager_Request.Utilities.Constants;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager_Request.Utilities
{
    public static class PageUtility
    {
        public static async Task<Pager> ToPaginationAsync<T>(this IQueryable<T> query, int currentPage, int pageSize = Commons.PageSize)
        {
            //Tính tổng số lượng record
            var count = await query.CountAsync();

            //Tính số lượng bỏ qua
            int skip = (currentPage - 1) * pageSize;

            //Lấy ra số lượng record của trang hiện tại
            var items = await query.Skip(skip).Take(pageSize).ToListAsync();
            return new Pager(items, count, currentPage, pageSize, pageSize);
        }
    }

    public class Pager
    {
        public Pager(object result, int totalItems, int currentPage = 1, int pageSize = Commons.PageSize, int maxPages = Commons.MaxPagePagination)
        {
            var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);

            if (currentPage < 1)
            {
                currentPage = 1;
            }
            else if (currentPage > totalPages)
            {
                currentPage = totalPages;
            }

            int startPage, endPage;
            if (totalPages <= maxPages)
            {
                startPage = 1;
                endPage = totalPages;
            }
            else
            {
                var maxPagesBeforeCurrentPage = (int)Math.Floor((decimal)maxPages / (decimal)2);
                var maxPagesAfterCurrentPage = (int)Math.Ceiling((decimal)maxPages / (decimal)2) - 1;
                if (currentPage <= maxPagesBeforeCurrentPage)
                {
                    startPage = 1;
                    endPage = maxPages;
                }
                else if (currentPage + maxPagesAfterCurrentPage >= totalPages)
                {
                    startPage = totalPages - maxPages + 1;
                    endPage = totalPages;
                }
                else
                {
                    startPage = currentPage - maxPagesBeforeCurrentPage;
                    endPage = currentPage + maxPagesAfterCurrentPage;
                }
            }

            var startIndex = (currentPage - 1) * pageSize;
            var endIndex = Math.Min(startIndex + pageSize - 1, totalItems - 1);

            var pages = Enumerable.Range(startPage, (endPage + 1) - startPage);

            Result = result;
            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
            StartIndex = startIndex;
            EndIndex = endIndex;
            PageNext = currentPage + 1 > totalPages ? totalPages : currentPage + 1;
            PagePrev = currentPage - 1 <= 0 ? 1 : currentPage - 1;
            PageFirst = 1;
            PageLast = totalPages;
            Pages = pages;
        }

        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
        public int StartIndex { get; private set; }
        public int EndIndex { get; private set; }
        public int PageNext { get; private set; }
        public int PagePrev { get; private set; }
        public int PageFirst { get; private set; }
        public int PageLast { get; private set; }
        public IEnumerable<int> Pages { get; private set; }
        public object Result { get; private set; }
    }

    public class ParamaterPagination
    {
        public int page { get; set; }
        public int pageSize { get; set; } = 10;
    }
}