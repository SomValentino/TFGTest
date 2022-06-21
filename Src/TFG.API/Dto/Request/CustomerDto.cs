using System.ComponentModel.DataAnnotations;

namespace TFG.API.Dto.Request;

public class CustomerDto {
    [Required]
    [MinLength (3)]
    [MaxLength (50)]
    public string FirstName { get; set; }

    [Required]
    [MinLength (3)]
    [MaxLength (50)]
    public string LastName { get; set; }

    [Required]
    [MinLength (3)]
    [MaxLength (50)]
    public string UserName { get; set; }

    public string RoleName { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType (DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }
    public DateTime CreatedAt => DateTime.UtcNow;
}