using CustomerManagement.Core.DTOs.Company;
using CustomerManagement.Core.Entities;
using CustomerManagement.Core.Helpers;
using CustomerManagement.Core.Interfaces;
using CustomerManagement.Core.Interfaces.Services;

namespace CustomerManagement.Infrastructure.Services;
public sealed class CompanyService(ICompanyRepository _companyRepository) : ICompanyService
{
    public async Task<Result<IEnumerable<CompanyDto>>> GetAllAsync(
        CompanySearchDto request, CancellationToken cancellationToken = default)
    {
        var data = await _companyRepository.GetAllAsync(request, cancellationToken);
        return Result<IEnumerable<CompanyDto>>.Ok(data);
    }

    public async Task<Result<CompanyDto>> GetByIdAsync(
        int id, CancellationToken cancellationToken = default)
    {
        if (!(await _companyRepository.IsExistsAsync(id, cancellationToken)))
        {
            return Result<CompanyDto>.Fail($"A company with id: '{id}' doesn't exist.");
        }

        var data = await _companyRepository.GetByIdAsync(id, cancellationToken);
        return Result<CompanyDto>.Ok(data);
    }

    public async Task<Result<int>> AddAsync(
        CreateCompanyDto company, CancellationToken cancellationToken = default)
    {
        if (await _companyRepository.NameExistsAsync(company.Name, null, cancellationToken))
        {
            return Result<int>.Fail($"A company with the name '{company.Name}' already exists.");
        }

        var entity = new Company()
        {
            Name = company.Name
        };
        await _companyRepository.AddAsync(entity);

        return Result<int>.Ok(entity.Id);
    }

    public async Task<Result<int>> UpdateAsync(
        UpdateCompanyDto company, CancellationToken cancellationToken = default)
    {
        if (!(await _companyRepository.IsExistsAsync(company.Id, cancellationToken)))
        {
            return Result<int>.Fail($"A company with id: '{company.Id}' doesn't exist.");
        }

        var entity = new Company()
        {
            Name = company.Name,
            Id = company.Id
        };
        await _companyRepository.UpdateAsync(entity);

        return Result<int>.Ok(entity.Id);
    }

    public async Task<Result<int>> DeleteAsync(
        int id, CancellationToken cancellationToken = default)
    {
        if (!(await _companyRepository.IsExistsAsync(id, cancellationToken)))
        {
            return Result<int>.Fail($"A company with id: '{id}' doesn't exist.");
        }

        await _companyRepository.DeleteAsync(id, cancellationToken);
        return Result<int>.Ok(id);
    }
}
