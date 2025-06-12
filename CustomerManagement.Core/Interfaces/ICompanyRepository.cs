using CustomerManagement.Core.DTOs.Company;
using CustomerManagement.Core.Entities;

namespace CustomerManagement.Core.Interfaces;
public interface ICompanyRepository
{
    Task<List<CompanyDto>> GetAllAsync(
    CompanySearchDto request, CancellationToken cancellationToken);
    Task<CompanyDto> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task AddAsync(Company company, CancellationToken cancellationToken = default);
    Task UpdateAsync(Company company, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> IsExistsAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> NameExistsAsync(string name, int? excludeId = null, CancellationToken cancellationToken = default);
}
