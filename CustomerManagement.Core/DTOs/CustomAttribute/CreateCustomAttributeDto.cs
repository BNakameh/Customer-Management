namespace CustomerManagement.Core.DTOs.CustomAttribute;
public record CreateCustomAttributeDto(
    string Name, string Value, int EntityId, string AttributeType, string EntityType);