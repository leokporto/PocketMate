using PocketMate.Core.Enums;

namespace PocketMate.Core.Models
{
	public class DepositTransaction : Transaction
	{
		public DepositTransaction()
		{
			Type = eTransactionType.Deposit;
		}
	}
}
