using CustomerManagement.Core.DTOs.CustomAttribute;

namespace CustomerManagement.Core.DTOs.Contact;
public record CreateContactDto(
    string Name, List<int>companiesId, List<CreateCustomAttributeDto> Attributes);
