namespace TFG.Application.Models;

public class IdentityResult {
    public string ErrorMessage { get; set; }

    public string Token { get; set; }
    
    public bool Success { get; set; }
}