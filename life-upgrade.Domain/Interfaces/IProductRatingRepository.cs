namespace LifeUpgrade.Domain.Interfaces;

public interface IProductRatingRepository
{
    
    Task Create(Domain.Entities.ProductRating rating);
    Task Commit();
    Task<IEnumerable<Domain.Entities.ProductRating>> GetAll();
    Task<IEnumerable<Domain.Entities.ProductRating>> GetByEncodedName(string encodedName);
    Task<Domain.Entities.ProductRating?> GetByEncodedNameAndUserId(string encodedName, Guid userId);
}