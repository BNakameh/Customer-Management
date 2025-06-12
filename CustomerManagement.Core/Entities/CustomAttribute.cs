using CustomerManagement.Core.Enums;

namespace CustomerManagement.Core.Entities;
public sealed class CustomAttribute : BaseEntity
{

    public string Name { get; set; }
    public string Value { get; set; }
    public AttributeType AttributeType { get; set; }
    public EntityType EntityType { get; set; }

    public int? CompanyId { get; set; }
    public Company Company { get; set; }

    public int? ContactId { get; set; }
    public Contact Contact { get; set; }
}
