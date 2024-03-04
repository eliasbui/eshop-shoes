namespace Data.ApplicationContracts.Common;

public class QueryDto
{
    public List<FilterDto> Filters { get; init; } = [];
    public List<string> Sorts { get; init; } = [];
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

public record FilterDto(string FieldName, string Comparision, string FieldValue);