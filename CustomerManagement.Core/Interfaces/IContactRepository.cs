using CustomerManagement.Core.DTOs.Contact;
using CustomerManagement.Core.Entities;

namespace CustomerManagement.Core.Interfaces;
public interface IContactRepository
{
    Task<List<ContactDto>> GetAllAsync(
             ContactSearchDto request, CancellationToken cancellationToken);
    Task<ContactDto> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<Contact> GetByIdAsync(int id);
    Task AddAsync(Contact company, CancellationToken cancellationToken = default);
    Task UpdateAsync(Contact company, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> IsExistsAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> NameExistsAsync(string name, int? excludeId = null, CancellationToken cancellationToken = default);
}
