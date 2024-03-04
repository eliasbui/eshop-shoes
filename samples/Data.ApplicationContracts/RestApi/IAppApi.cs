using Data.ApplicationContracts.Common;
using Data.ApplicationContracts.Dtos;
using RestEase;

namespace Data.ApplicationContracts.RestApi;

public interface IAppApi
{
    [Get("api/product-api/v1/products")]
    Task<ResultDto<ListResultDto<ProductDto>>> GetProductsAsync([Header("x-query")] string xQuery);
}