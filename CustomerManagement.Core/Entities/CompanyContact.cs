namespace CustomerManagement.Core.Entities;
public sealed class CompanyContact : BaseEntity
{
    public int CompanyId { get; set; }
    public Company Company { get; set; }

    public int ContactId { get; set; }
    public Contact Contact { get; set; }
}
