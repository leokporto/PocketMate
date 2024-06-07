using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketMate.Core.Models
{
	public class Category
	{
		public long Id { get; set; }

		public string Name { get; set; } = string.Empty;

		public string Description { get; set; } = string.Empty;

		public string UserId { get; set; } = string.Empty;
	}
}
