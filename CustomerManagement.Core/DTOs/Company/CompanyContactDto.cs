using CustomerManagement.Core.DTOs.CustomAttribute;

namespace CustomerManagement.Core.DTOs.Company;
public record CompanyContactDto(int Id, string Name, List<CustomAttributeDto> Attributes);
