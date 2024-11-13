namespace NCRSPOTLIGHT.Models.ViewModels
{
    public class TwoFactorAuthenticationVIewModel
    {
        public string Code { get; set; }
        public string? Token { get; set; }
        public string? QRCodeUrl { get; set; }
    }
}
