using DataContractor.ApplicationContracts.Common;
using DataContractor.ApplicationContracts.Dtos;
using RestEase;

namespace DataContractor.ApplicationContracts.RestApi;

public interface IAppApi
{
    [Get("api/product-api/v1/products")]
    Task<ResultDto<ListResultDto<ProductDto>>> GetProductsAsync([Header("x-query")] string xQuery);
}