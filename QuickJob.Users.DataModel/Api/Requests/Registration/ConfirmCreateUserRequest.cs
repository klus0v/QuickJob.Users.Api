using System.ComponentModel.DataAnnotations;

namespace QuickJob.Users.DataModel.Api.Requests.Registration;

public sealed record ConfirmCreateUserRequest
{
    [Required]
    public Guid UserFormId { get; set; }
    
    [StringLength(maximumLength: 4, MinimumLength = 4), Required]
    public string Code { get; set; }
}