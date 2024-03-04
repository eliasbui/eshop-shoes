using DataContractor.ApplicationContracts.Common;
using DataContractor.ApplicationContracts.Dtos;
using RestEase;

namespace DataContractor.ApplicationContracts.RestApi;

public interface ICountryApi
{
    [Get("api/v1/countries/{countryId}")]
    Task<ResultDto<CountryDto>> GetCountryByIdAsync([Path] Guid countryId);
}