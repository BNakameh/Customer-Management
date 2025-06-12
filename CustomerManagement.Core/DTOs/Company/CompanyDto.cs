using CustomerManagement.Core.DTOs.CustomAttribute;

namespace CustomerManagement.Core.DTOs.Company;
public record CompanyDto(
    int Id, string Name, List<CustomAttributeDto> Attributes, List<CompanyContactDto> Contacts);
