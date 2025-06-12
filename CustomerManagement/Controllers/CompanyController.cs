using CustomerManagement.Core.DTOs.Company;
using CustomerManagement.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.Controllers;

[Route("api/[controller]")]
public class CompanyController(ICompanyService _companyService) : BaseApiController
{
    [HttpPut]
    public async Task<IActionResult> GetAll(CompanySearchDto dto, CancellationToken cancellationToken) =>
        ToActionResult(await _companyService.GetAllAsync(dto, cancellationToken));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken) =>
        ToActionResult(await _companyService.GetByIdAsync(id, cancellationToken));

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateCompanyDto dto, CancellationToken cancellationToken) =>
        ToActionResult(await _companyService.AddAsync(dto, cancellationToken));

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id, [FromBody] UpdateCompanyDto dto, CancellationToken cancellationToken) =>
        ToActionResult(await _companyService.UpdateAsync(dto, cancellationToken));

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken) =>
        ToActionResult(await _companyService.DeleteAsync(id, cancellationToken));
}
