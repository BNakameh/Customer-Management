using CustomerManagement.Core.DTOs.CustomAttribute;

namespace CustomerManagement.Core.DTOs.Contact;
public record ContactDto(int Id, string Name, List<ContactCompanyDto> Companies, List<CustomAttributeDto> Attributes);
