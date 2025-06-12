using CustomerManagement.Core.DTOs.Contact;
using CustomerManagement.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.Controllers;

[Route("api/[controller]")]
public class ContactController(IContactService _contactService) : BaseApiController
{
    [HttpPut]
    public async Task<IActionResult> GetAll(ContactSearchDto request, CancellationToken cancellationToken) =>
        ToActionResult(await _contactService.GetAllAsync(request, cancellationToken));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken) =>
        ToActionResult(await _contactService.GetByIdAsync(id, cancellationToken));

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateContactDto dto, CancellationToken cancellationToken) =>
        ToActionResult(await _contactService.AddAsync(dto, cancellationToken));

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id, [FromBody] UpdateContactDto dto, CancellationToken cancellationToken) =>
        ToActionResult(await _contactService.UpdateAsync(dto, cancellationToken));

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken) =>
        ToActionResult(await _contactService.DeleteAsync(id, cancellationToken));
}
