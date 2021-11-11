namespace InvoicesControl.Api.Extensions
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int HoursToExpirations { get; set; }
        public string Issuer { get; set; }
        public string ValidUrls { get; set; }
    }
}
