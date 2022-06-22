namespace TFG.API.Dto.Response
{
    public class TokenDto
    {
        public string Token { get; set; }
        public string Scheme { get; set; }
        public double Expiry { get; set; }
    }
}
