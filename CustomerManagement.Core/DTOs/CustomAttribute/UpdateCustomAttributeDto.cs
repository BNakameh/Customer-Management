namespace CustomerManagement.Core.DTOs.CustomAttribute;
public record UpdateCustomAttributeDto(
    int Id, string Name, string Value, int EntityId, string AttributeType, string EntityType);
