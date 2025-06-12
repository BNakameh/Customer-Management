using CustomerManagement.Core.DTOs.Company;
using CustomerManagement.Core.Helpers;

namespace CustomerManagement.Core.Interfaces.Services;
public interface ICompanyService
{
    Task<Result<IEnumerable<CompanyDto>>> GetAllAsync(
        CompanySearchDto request, CancellationToken cancellationToken = default);
    Task<Result<CompanyDto>> GetByIdAsync(
        int id, CancellationToken cancellationToken = default);
    Task<Result<int>> AddAsync(CreateCompanyDto company, CancellationToken cancellationToken = default);
    Task<Result<int>> UpdateAsync(UpdateCompanyDto company, CancellationToken cancellationToken = default);
    Task<Result<int>> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
