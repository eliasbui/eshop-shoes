using System;

namespace Eshop.Core.Domain.Attributes;

public class DaprPubSubNameAttribute : Attribute
{
    public string? PubSubName { get; set; }
}