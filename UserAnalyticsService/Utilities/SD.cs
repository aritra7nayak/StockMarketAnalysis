namespace UserAnalyticsService.Utilities
{
    public class SD
    {
        public static string DataAcquisition { get; set; }

        public const string RoleAdmin = "ADMIN";
        public const string RoleCustomer = "CUSTOMER";
        public const string TokenCookie = "JwtToken";
        public enum ApiType
        {
            GET, POST, PUT, DELETE
        }
    }
}
