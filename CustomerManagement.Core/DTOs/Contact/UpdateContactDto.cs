using CustomerManagement.Core.DTOs.CustomAttribute;

namespace CustomerManagement.Core.DTOs.Contact;
public record UpdateContactDto(
    int Id, string Name, List<int> companiesId, List<CreateCustomAttributeDto> Attributes);
