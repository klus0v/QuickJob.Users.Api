using System.ComponentModel.DataAnnotations;

namespace QuickJob.Users.DataModel.Api.Requests.Registration;

public sealed record InitCreateUserRequest
{
    [Required, EmailAddress]
    public string Email { get; set; }
    [Required, Phone]
    public string Phone { get; set; }
    [Required]
    public string Password { get; set; }
    public string Fio { get; set; }
    public string BirthDate { get; set; }
}