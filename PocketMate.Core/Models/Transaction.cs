using PocketMate.Core.Enums;

namespace PocketMate.Core.Models
{
	public abstract class Transaction
	{
		public long Id { get; set; }
		public string Title { get; set; } = string.Empty;

		public DateTime CreatedAt { get; set; } = DateTime.Now;

		public DateTime? AccountedAt { get; set; }

		public eTransactionType Type { get; init; } = eTransactionType.Withdraw;

		public decimal Amount { get; set; }

		public long CategoryId { get; set; }

		public Category Category { get; set; } =  null!;

		public string UserId { get; set; } = string.Empty;
	}
}
