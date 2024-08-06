using PocketMate.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace PocketMate.Core.Requests.Transactions
{
	public class UpdateTransactionRequest : BaseRequest
	{
		public long Id { get; set; }

		[Required(ErrorMessage = "Título inválido")]
		public string Title { get; set; } = string.Empty;

		[Required(ErrorMessage = "Tipo inválido")]
		public eTransactionType Type { get; set; }

		[Required(ErrorMessage = "Valor inválido")]
		public decimal Amount { get; set; }

		[Required(ErrorMessage = "Categoria inválida")]
		public long CategoryId { get; set; }

		[Required(ErrorMessage = "Data inválida")]
		public DateTime? PaidOrReceivedAt { get; set; }
	}
}
