using CustomerManagement.Core.Entities;

namespace CustomerManagement.Core.Interfaces;
public interface ICustomAttributeRepository
{
    Task<CustomAttribute> GetByIdAsync(
            int id, CancellationToken cancellationToken = default);
    Task AddAsync(
        CustomAttribute entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(
        CustomAttribute entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(
        int id, CancellationToken cancellationToken = default);
    Task<bool> IsExistsAsync(int id, CancellationToken cancellationToken = default);
}
