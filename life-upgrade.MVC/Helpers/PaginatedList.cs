using AutoMapper;
using LifeUpgrade.Application.Photo;
using LifeUpgrade.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LifeUpgrade.MVC.Helpers;

public class PaginatedList<T> : List<T>
{
    public int PageIndex { get; private set; }
    public int TotalPages { get; private set; }

    public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        
        AddRange(items);
    }
    
    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < TotalPages;

    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PaginatedList<T>(items, count, pageIndex, pageSize);
    }

    public static async Task<PaginatedList<Domain.Entities.Product>> CreateAsync(IQueryable<Domain.Entities.Product> source, int pageIndex, int pageSize, IEnumerable<PhotoDto> mainPhotos)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        items.ForEach(x =>
        {
            var photo = mainPhotos.FirstOrDefault(p => p.ProductId == x.Id);
            if (photo != null)
                x.Photos.Add(new Domain.Entities.Photo
                {
                    Id = photo.Id,
                    Bytes = photo.Bytes,
                    Description = photo.Description,
                    FileExtension = photo.FileExtension,
                    Size = photo.Size,
                    Order = 0,
                    ProductId = photo.ProductId,
                    Product = x,
                });
        });
        return new PaginatedList<Domain.Entities.Product>(items, count, pageIndex, pageSize);
    }
}