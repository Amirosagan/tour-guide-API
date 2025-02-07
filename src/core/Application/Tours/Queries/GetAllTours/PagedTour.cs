using Application.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Tours.Queries.GetAllTours;

public class PagedTour
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int Count { get; set; }
    public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));
    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;
    public IEnumerable<Tour> Items { get; set; }

    public static async Task<PagedTour> Create(
        IQueryable<Tour> source,
        int pageNumber,
        int pageSize
    )
    {
        var count = await source.CountAsync();
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PagedTour
        {
            CurrentPage = pageNumber,
            PageSize = pageSize,
            Count = count,
            Items = items,
        };
    }
}
