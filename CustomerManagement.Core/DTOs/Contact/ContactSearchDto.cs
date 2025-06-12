namespace CustomerManagement.Core.DTOs.Contact;
public record ContactSearchDto(string SearchText, int? LastContactId, int PageSize = 20);
