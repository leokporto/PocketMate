﻿using System.ComponentModel.DataAnnotations;

namespace PocketMate.Core.Requests.Account
{
	public class LoginRequest : BaseRequest
	{
		[Required(ErrorMessage = "E-mail")]
		[EmailAddress(ErrorMessage = "E-mail inválido")]
		public string Email { get; set; } = string.Empty;

		[Required(ErrorMessage = "Senha Inválida")]
		public string Password { get; set; } = string.Empty;
	}
}
