using PocketMate.Core.Enums;

namespace PocketMate.Core.Models
{
	public class WithdrawTransaction : Transaction
	{
		public WithdrawTransaction()
		{
			Type = eTransactionType.Withdraw;
		}
	}
}
