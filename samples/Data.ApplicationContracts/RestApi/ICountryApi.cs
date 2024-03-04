using Data.ApplicationContracts.Common;
using Data.ApplicationContracts.Dtos;
using RestEase;

namespace Data.ApplicationContracts.RestApi;

public interface ICountryApi
{
    [Get("api/v1/countries/{countryId}")]
    Task<ResultDto<CountryDto>> GetCountryByIdAsync([Path] Guid countryId);
}