using CustomerManagement.Core.DTOs.CustomAttribute;
using CustomerManagement.Core.Helpers;

namespace CustomerManagement.Core.Interfaces.Services;
public interface ICustomAttributeService
{
    Task<Result<int>> AddAsync(
       CreateCustomAttributeDto attribute, CancellationToken cancellationToken = default);

    Task<Result<int>> UpdateAsync(
       UpdateCustomAttributeDto attribute, CancellationToken cancellationToken = default);

    Task<Result<int>> DeleteAsync(
        int id, CancellationToken cancellationToken = default);
}
