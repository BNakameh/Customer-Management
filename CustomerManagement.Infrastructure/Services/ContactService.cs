using CustomerManagement.Core.DTOs.Contact;
using CustomerManagement.Core.Entities;
using CustomerManagement.Core.Enums;
using CustomerManagement.Core.Helpers;
using CustomerManagement.Core.Interfaces;
using CustomerManagement.Core.Interfaces.Services;

namespace CustomerManagement.Infrastructure.Services;
public sealed class ContactService(IContactRepository _contactRepository) : IContactService
{
    public async Task<Result<IEnumerable<ContactDto>>> GetAllAsync(
        ContactSearchDto request, CancellationToken cancellationToken = default)
    {
        var data = await _contactRepository.GetAllAsync(request, cancellationToken);
        return Result<IEnumerable<ContactDto>>.Ok(data);
    }

    public async Task<Result<ContactDto>> GetByIdAsync(
        int id, CancellationToken cancellationToken = default)
    {
        if (!(await _contactRepository.IsExistsAsync(id, cancellationToken)))
        {
            return Result<ContactDto>.Fail($"A contact with id: '{id}' doesn't exist.");
        }

        var data = await _contactRepository.GetByIdAsync(id, cancellationToken);
        return Result<ContactDto>.Ok(data);
    }

    public async Task<Result<int>> AddAsync(
        CreateContactDto contact, CancellationToken cancellationToken = default)
    {
        if (await _contactRepository.NameExistsAsync(contact.Name, null, cancellationToken))
        {
            return Result<int>.Fail($"A contact with the name '{contact.Name}' already exists.");
        }

        var entity = new Contact()
        {
            Name = contact.Name,
            CompanyContacts = contact.companiesId.Select(e => new CompanyContact()
            {
                CompanyId = e
            }).ToList(),
            CustomAttributes = contact.Attributes.Select(e => new CustomAttribute()
            {
                Name = e.Name,
                Value = e.Value,
                AttributeType = Enum.Parse<AttributeType>(e.AttributeType),
                EntityType = EntityType.Contact
            }).ToList()
        };
        await _contactRepository.AddAsync(entity);

        return Result<int>.Ok(entity.Id);
    }

    public async Task<Result<int>> UpdateAsync(
        UpdateContactDto contact, CancellationToken cancellationToken = default)
    {
        if (!(await _contactRepository.IsExistsAsync(contact.Id, cancellationToken)))
        {
            return Result<int>.Fail($"A contact with id: '{contact.Id}' doesn't exist.");
        }

        var oldContact = await _contactRepository.GetByIdAsync(contact.Id);

        oldContact.Name = contact.Name;
        oldContact.CompanyContacts = contact.companiesId.Select(cid => new CompanyContact { CompanyId = cid, ContactId = contact.Id }).ToList();
        oldContact.CustomAttributes = contact.Attributes.Select(attr => new CustomAttribute
        {
            Name = attr.Name,
            Value = attr.Value,
            AttributeType = Enum.Parse<AttributeType>(attr.AttributeType),
            EntityType = EntityType.Contact,
            ContactId = contact.Id
        }).ToList();
        await _contactRepository.UpdateAsync(oldContact);

        return Result<int>.Ok(contact.Id);
    }

    public async Task<Result<int>> DeleteAsync(
        int id, CancellationToken cancellationToken = default)
    {
        if (!(await _contactRepository.IsExistsAsync(id, cancellationToken)))
        {
            return Result<int>.Fail($"A contact with id: '{id}' doesn't exist.");
        }

        await _contactRepository.DeleteAsync(id, cancellationToken);
        return Result<int>.Ok(id);
    }
}
