using System.Text.Json.Serialization;

namespace PocketMate.Core.Responses
{
    public class Response<TData>
	{
		private readonly int _code;

		[JsonConstructor]
		public Response()
		{
			_code = Configuration.DEFAULT_STATUS_CODE;
		}

		public Response(TData? data, int code = Configuration.DEFAULT_STATUS_CODE, string? message = null)
		{
			Data = data;
			_code = code;
			Message = message;
		}

		public TData? Data { get; set; }
		public string? Message { get; set; }

		[JsonIgnore]
		public bool IsSuccess => _code >= 200 && _code < 300;
	}
}
