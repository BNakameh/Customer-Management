using CustomerManagement.Core.DTOs.CustomAttribute;
using CustomerManagement.Core.Entities;
using CustomerManagement.Core.Enums;
using CustomerManagement.Core.Helpers;
using CustomerManagement.Core.Interfaces;
using CustomerManagement.Core.Interfaces.Services;

namespace CustomerManagement.Infrastructure.Services;
public sealed class CustomAttributeService(ICustomAttributeRepository _customAttributeRepository) : ICustomAttributeService
{
    public async Task<Result<int>> AddAsync(
       CreateCustomAttributeDto attribute, CancellationToken cancellationToken = default)
    {
        var entity = new CustomAttribute()
        {
            Name = attribute.Name,
            Value = attribute.Value,
            EntityType = Enum.Parse<EntityType>(attribute.EntityType),
            AttributeType = Enum.Parse<AttributeType>(attribute.AttributeType),
            CompanyId = attribute.EntityType == nameof(EntityType.Company) ? attribute.EntityId : null,
            ContactId = attribute.EntityType == nameof(EntityType.Contact) ? attribute.EntityId : null
        };
        await _customAttributeRepository.AddAsync(entity);

        return Result<int>.Ok(entity.Id);
    }

    public async Task<Result<int>> UpdateAsync(
       UpdateCustomAttributeDto attribute, CancellationToken cancellationToken = default)
    {
        var entity = await _customAttributeRepository.GetByIdAsync(attribute.Id, cancellationToken);
        if (entity is null)
        {
            return Result<int>.Fail($"An attribute with id: '{attribute.Id}' doesn't exist.");
        }

        entity.Name = attribute.Name;
        entity.Value = attribute.Value;
        entity.EntityType = Enum.Parse<EntityType>(attribute.EntityType);
        entity.AttributeType = Enum.Parse<AttributeType>(attribute.AttributeType);
        entity.CompanyId = attribute.EntityType == nameof(EntityType.Company) ? attribute.EntityId : null;
        entity.ContactId = attribute.EntityType == nameof(EntityType.Contact) ? attribute.EntityId : null;

        await _customAttributeRepository.UpdateAsync(entity);

        return Result<int>.Ok(entity.Id);
    }

    public async Task<Result<int>> DeleteAsync(
        int id, CancellationToken cancellationToken = default)
    {
        if (!(await _customAttributeRepository.IsExistsAsync(id, cancellationToken)))
        {
            return Result<int>.Fail($"An attribute with id: '{id}' doesn't exist.");
        }

        await _customAttributeRepository.DeleteAsync(id, cancellationToken);
        return Result<int>.Ok(id);
    }
}
