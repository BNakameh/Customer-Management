namespace CustomerManagement.Core.DTOs.CustomAttribute;
public record CustomAttributeDto(
    int Id, string Name, string Value, string AttributeType, string EntityType);