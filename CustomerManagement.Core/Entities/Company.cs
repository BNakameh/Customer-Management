namespace CustomerManagement.Core.Entities;
public sealed class Company : BaseEntity
{
    public string Name { get; set; }

    #region Navigation Properties

    public ICollection<CompanyContact> CompanyContacts { get; set; } = new List<CompanyContact>();
    public ICollection<CustomAttribute> CustomAttributes { get; set; } = new List<CustomAttribute>();
    #endregion
}
