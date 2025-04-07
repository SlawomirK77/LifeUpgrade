using MediatR;

namespace LifeUpgrade.Application.Product.Queries.GetProductByEncodedName;

public class GetProductByEncodedNameQuery : IRequest<ProductDto>
{
    public string EncodedName { get; set; }

    public GetProductByEncodedNameQuery(string encodedName)
    {
        EncodedName = encodedName;
    }
}