using CustomerManagement.Core.DTOs.CustomAttribute;
using CustomerManagement.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.Controllers;

[Route("api/[controller]")]
public class CustomAttributeController(ICustomAttributeService _customAttributeService) : BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> Create(
       [FromBody] CreateCustomAttributeDto dto, CancellationToken cancellationToken) =>
       ToActionResult(await _customAttributeService.AddAsync(dto, cancellationToken));

    [HttpPut]
    public async Task<IActionResult> Update(
       [FromBody] UpdateCustomAttributeDto dto, CancellationToken cancellationToken) =>
       ToActionResult(await _customAttributeService.UpdateAsync(dto, cancellationToken));

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken) =>
        ToActionResult(await _customAttributeService.DeleteAsync(id, cancellationToken));
}
