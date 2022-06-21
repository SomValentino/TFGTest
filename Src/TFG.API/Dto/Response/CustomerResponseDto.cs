namespace TFG.API.Dto.Response;

public class CustomerResponseDto {

    public string Id { get; set; }
    public string FirstName { get; set; }

    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime CreatedAt { get; set; }
}