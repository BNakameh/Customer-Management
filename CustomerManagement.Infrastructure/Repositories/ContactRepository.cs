using CustomerManagement.Core.DTOs.Contact;
using CustomerManagement.Core.DTOs.CustomAttribute;
using CustomerManagement.Core.Entities;
using CustomerManagement.Core.Interfaces;
using CustomerManagement.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Infrastructure.Repositories;
public sealed class ContactRepository(AppDbContext _context) : IContactRepository
{
    public async Task<List<ContactDto>> GetAllAsync(
    ContactSearchDto request, CancellationToken cancellationToken)
    {
        var searchText = request.SearchText?.Trim();

        var query = _context.Contacts.AsNoTracking()
            .Where(c => request.LastContactId == null || c.Id > request.LastContactId);

        if (!string.IsNullOrWhiteSpace(searchText))
        {
            query = query.Where(contact =>
                EF.Functions.ToTsVector("english", contact.Name)
                    .Matches(EF.Functions.PlainToTsQuery("english", searchText)) ||

                _context.CustomAttributes.Any(attr =>
                    attr.ContactId == contact.Id &&
                    EF.Functions.ToTsVector("english", attr.Name + " " + attr.Value)
                        .Matches(EF.Functions.PlainToTsQuery("english", searchText))) ||

                _context.CompanyContacts.Any(cc =>
                    cc.ContactId == contact.Id &&
                    EF.Functions.ToTsVector("english", cc.Company.Name)
                        .Matches(EF.Functions.PlainToTsQuery("english", searchText))) ||

                _context.CustomAttributes.Any(attr =>
                    attr.CompanyId != null &&
                    _context.CompanyContacts.Any(cc =>
                        cc.ContactId == contact.Id && cc.CompanyId == attr.CompanyId) &&
                    EF.Functions.ToTsVector("english", attr.Name + " " + attr.Value)
                        .Matches(EF.Functions.PlainToTsQuery("english", searchText)))
            );
        }

        var result = await query
            .OrderBy(c => c.Id)
            .Take(request.PageSize)
            .Select(c => new ContactDto(
                c.Id,
                c.Name,
                _context.CompanyContacts
                    .Where(cc => cc.ContactId == c.Id)
                    .Select(cc => new ContactCompanyDto(
                        cc.Company.Id,
                        cc.Company.Name,
                        _context.CustomAttributes
                            .Where(attr => attr.CompanyId == cc.Company.Id)
                            .Select(attr => new CustomAttributeDto(
                                attr.Id,
                                attr.Name,
                                attr.Value,
                                attr.AttributeType.ToString(),
                                attr.EntityType.ToString()
                            )).ToList()
                    )).ToList(),

                _context.CustomAttributes
                    .Where(attr => attr.ContactId == c.Id)
                    .Select(attr => new CustomAttributeDto(
                        attr.Id,
                        attr.Name,
                        attr.Value,
                        attr.AttributeType.ToString(),
                        attr.EntityType.ToString()
                    )).ToList()
            ))
            .ToListAsync(cancellationToken);

        return result;
    }


    public async Task<ContactDto> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var contact = await _context.Contacts
            .AsNoTracking()
            .Where(c => c.Id == id)
            .Select(c => new ContactDto(
                c.Id,
                c.Name,
                _context.CompanyContacts
                    .Where(cc => cc.ContactId == c.Id)
                    .Select(cc => new ContactCompanyDto(
                        cc.Company.Id,
                        cc.Company.Name,
                        _context.CustomAttributes
                            .Where(attr => attr.CompanyId == cc.Company.Id)
                            .Select(attr => new CustomAttributeDto(
                                attr.Id,
                                attr.Name,
                                attr.Value,
                                attr.AttributeType.ToString(),
                                attr.EntityType.ToString()
                            )).ToList()
                    )).ToList(),

                _context.CustomAttributes
                    .Where(attr => attr.ContactId == c.Id)
                    .Select(attr => new CustomAttributeDto(
                        attr.Id,
                        attr.Name,
                        attr.Value,
                        attr.AttributeType.ToString(),
                        attr.EntityType.ToString()
                    )).ToList()
            ))
            .FirstOrDefaultAsync(cancellationToken);

        return contact;
    }

    public async Task<Contact> GetByIdAsync(int id) =>
        await _context.Contacts
                      .AsNoTracking()
                      .Where(e => e.Id == id)
                      .FirstOrDefaultAsync();

    public async Task AddAsync(
        Contact contact, CancellationToken cancellationToken = default)
    {
        await _context.Contacts.AddAsync(contact, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Contact contact, CancellationToken cancellationToken = default)
    {
        _context.Contacts.Update(contact);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(
        int id, CancellationToken cancellationToken = default)
    {
        await _context.Contacts
            .Where(e => e.Id == id)
            .ExecuteDeleteAsync(cancellationToken);
    }

    public async Task<bool> IsExistsAsync(int id, CancellationToken cancellationToken = default) =>
        await _context.Contacts
            .Where(c => c.Id == id)
            .AnyAsync(cancellationToken);

    public async Task<bool> NameExistsAsync(string name, int? excludeId = null, CancellationToken cancellationToken = default) =>
        await _context.Contacts
            .Where(c => c.Name == name && (!excludeId.HasValue || c.Id != excludeId.Value))
            .AnyAsync(cancellationToken);
}
