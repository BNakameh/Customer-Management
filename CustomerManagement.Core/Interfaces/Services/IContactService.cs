using CustomerManagement.Core.DTOs.Contact;
using CustomerManagement.Core.Helpers;

namespace CustomerManagement.Core.Interfaces.Services;
public interface IContactService
{
    Task<Result<IEnumerable<ContactDto>>> GetAllAsync(
        ContactSearchDto request, CancellationToken cancellationToken = default);
    Task<Result<ContactDto>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<int>> AddAsync(CreateContactDto company, CancellationToken cancellationToken = default);
    Task<Result<int>> UpdateAsync(UpdateContactDto company, CancellationToken cancellationToken = default);
    Task<Result<int>> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
