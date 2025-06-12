using CustomerManagement.Core.DTOs.Company;
using CustomerManagement.Core.DTOs.Contact;
using CustomerManagement.Core.DTOs.CustomAttribute;
using CustomerManagement.Core.Entities;
using CustomerManagement.Core.Interfaces;
using CustomerManagement.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Infrastructure.Repositories;
public sealed class CompanyRepository(AppDbContext _context) : ICompanyRepository
{
    public async Task<List<CompanyDto>> GetAllAsync(
            CompanySearchDto request, CancellationToken cancellationToken)
    {
        var searchText = request.SearchText?.Trim();
        var query = _context.Companies.AsNoTracking()
            .Where(c => request.LastCompanyId == null || c.Id > request.LastCompanyId);

        if (!string.IsNullOrWhiteSpace(searchText))
        {
            query = query.Where(c =>
                EF.Functions.ToTsVector("english", c.Name).Matches(
                    EF.Functions.PlainToTsQuery("english", searchText)) ||

                _context.CustomAttributes.Any(attr =>
                    attr.CompanyId == c.Id &&
                    EF.Functions.ToTsVector("english", attr.Name + " " + attr.Value).Matches(
                        EF.Functions.PlainToTsQuery("english", searchText))) ||

                _context.CompanyContacts.Any(cc =>
                    cc.CompanyId == c.Id &&
                    EF.Functions.ToTsVector("english", cc.Contact.Name).Matches(
                        EF.Functions.PlainToTsQuery("english", searchText))) ||

                _context.CustomAttributes.Any(attr =>
                    attr.ContactId != null &&
                    _context.CompanyContacts.Any(cc => cc.CompanyId == c.Id && cc.ContactId == attr.ContactId) &&
                    EF.Functions.ToTsVector("english", attr.Name + " " + attr.Value).Matches(
                        EF.Functions.PlainToTsQuery("english", searchText)))
            );
        }

        var result = await query
            .OrderBy(c => c.Id)
            .Take(request.PageSize)
            .Select(c => new CompanyDto(
                c.Id,
                c.Name,
                _context.CustomAttributes
                    .Where(attr => attr.CompanyId == c.Id)
                    .Select(attr => new CustomAttributeDto(
                        attr.Id,
                        attr.Name,
                        attr.Value,
                        attr.AttributeType.ToString(),
                        attr.EntityType.ToString()
                    )).ToList(),

                _context.CompanyContacts
                    .Where(cc => cc.CompanyId == c.Id)
                    .Select(cc => new CompanyContactDto(
                        cc.Contact.Id,
                        cc.Contact.Name,
                        _context.CustomAttributes
                            .Where(attr => attr.ContactId == cc.Contact.Id)
                            .Select(attr => new CustomAttributeDto(
                                attr.Id,
                                attr.Name,
                                attr.Value,
                                attr.AttributeType.ToString(),
                                attr.EntityType.ToString()
                            )).ToList()
                    )).ToList()
            ))
            .ToListAsync(cancellationToken);

        return result;
    }

    public async Task<CompanyDto> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var result = await _context.Companies
            .AsNoTracking()
            .Where(c => c.Id == id)
            .Select(c => new CompanyDto(
                c.Id,
                c.Name,
                _context.CustomAttributes
                    .Where(attr => attr.CompanyId == c.Id)
                    .Select(attr => new CustomAttributeDto(
                        attr.Id,
                        attr.Name,
                        attr.Value,
                        attr.AttributeType.ToString(),
                        attr.EntityType.ToString()
                    )).ToList(),

                _context.CompanyContacts
                    .Where(cc => cc.CompanyId == c.Id)
                    .Select(cc => new CompanyContactDto(
                        cc.Contact.Id,
                        cc.Contact.Name,
                        _context.CustomAttributes
                            .Where(attr => attr.ContactId == cc.Contact.Id)
                            .Select(attr => new CustomAttributeDto(
                                attr.Id,
                                attr.Name,
                                attr.Value,
                                attr.AttributeType.ToString(),
                                attr.EntityType.ToString()
                            )).ToList()
                    )).ToList()
            ))
            .FirstOrDefaultAsync(cancellationToken);

        return result;
    }

    public async Task AddAsync(
        Company company, CancellationToken cancellationToken = default)
    {
        await _context.Companies.AddAsync(company, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Company company, CancellationToken cancellationToken = default)
    {
        await _context.Companies
                .Where(c => c.Id == company.Id)
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(c => c.Name, company.Name), cancellationToken);
    }

    public async Task DeleteAsync(
        int id, CancellationToken cancellationToken = default)
    {
        await _context.Companies
            .Where(e => e.Id == id)
            .ExecuteDeleteAsync(cancellationToken);
    }

    public async Task<bool> IsExistsAsync(int id, CancellationToken cancellationToken = default) =>
        await _context.Companies
            .Where(c => c.Id == id)
            .AnyAsync(cancellationToken);

    public async Task<bool> NameExistsAsync(string name, int? excludeId = null, CancellationToken cancellationToken = default) =>
        await _context.Companies
            .Where(c => c.Name == name && (!excludeId.HasValue || c.Id != excludeId.Value))
            .AnyAsync(cancellationToken);
}