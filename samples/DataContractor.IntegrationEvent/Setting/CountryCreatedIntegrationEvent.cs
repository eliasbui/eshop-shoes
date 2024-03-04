using Eshop.Core.Domain.Bases;

namespace DataContractor.IntegrationEvent.Setting;

public class CountryCreatedIntegrationEvent : EventBase
{
    public Guid Id { get; set; }
    private string Name { get; set; } = default!;

    public override void Flatten()
    {
        MetaData.Add("Id", Id);
        MetaData.Add("Name", Name);
    }
}