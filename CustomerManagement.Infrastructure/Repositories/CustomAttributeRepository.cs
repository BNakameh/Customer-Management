using CustomerManagement.Core.Entities;
using CustomerManagement.Core.Interfaces;
using CustomerManagement.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Infrastructure.Repositories;
public sealed class CustomAttributeRepository(AppDbContext _context) : ICustomAttributeRepository
{

    public async Task<CustomAttribute> GetByIdAsync(
            int id, CancellationToken cancellationToken = default) =>
        await _context.CustomAttributes
                      .AsNoTracking()
                      .Where(e => e.Id == id)
                      .FirstOrDefaultAsync(cancellationToken);
    public async Task AddAsync(
        CustomAttribute entity, CancellationToken cancellationToken = default)
    {
        await _context.CustomAttributes.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(
        CustomAttribute entity, CancellationToken cancellationToken = default)
    {
        _context.CustomAttributes.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(
        int id, CancellationToken cancellationToken = default)
    {
        await _context.CustomAttributes
            .Where(e => e.Id == id)
            .ExecuteDeleteAsync(cancellationToken);
    }

    public async Task<bool> IsExistsAsync(int id, CancellationToken cancellationToken = default) =>
        await _context.CustomAttributes
            .Where(c => c.Id == id)
            .AnyAsync(cancellationToken);
}
