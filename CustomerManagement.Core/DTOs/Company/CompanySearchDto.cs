namespace CustomerManagement.Core.DTOs.Company;
public record CompanySearchDto(string SearchText, int? LastCompanyId, int PageSize = 20);
