namespace PocketMate.Core.Requests
{
    public class PagedRequest : BaseRequest
	{
		public int PageNumber { get; set; } = Configuration.DEFAULT_PAGE_NUMBER;
		public int PageSize { get; set; } = Configuration.DEFAULT_PAGE_SIZE;
	}
}
