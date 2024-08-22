namespace PocketMate.Core
{
	public static class Configuration
    {
        public const int DEFAULT_STATUS_CODE = 200;
        public const int DEFAULT_PAGE_NUMBER = 1;
        public const int DEFAULT_PAGE_SIZE = 25;

		public static string ConnectionString { get; set; } = string.Empty;
		public static string BackendUrl { get; set; } = string.Empty;
		public static string FrontendUrl { get; set; } = string.Empty;

		public static long PremiumPrice { get; set; } = 79990;
	}
}
