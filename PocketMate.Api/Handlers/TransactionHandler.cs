﻿using Microsoft.EntityFrameworkCore;
using PocketMate.Api.Data;
using PocketMate.Core.Common.Extensions;
using PocketMate.Core.Enums;
using PocketMate.Core.Handlers;
using PocketMate.Core.Models;
using PocketMate.Core.Requests.Transactions;
using PocketMate.Core.Responses;

namespace PocketMate.Api.Handlers
{
	public class TransactionHandler(AppDbContext context) : ITransactionHandler
	{
		public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
		{
			if (request is { Type: eTransactionType.Withdraw, Amount: >= 0 })
				request.Amount *= -1;

			try
			{
				var transaction = new Transaction
				{
					UserId = request.UserId,
					CategoryId = request.CategoryId,
					CreatedAt = DateTime.UtcNow,
					Amount = request.Amount,
					AccountedAt = request.PaidOrReceivedAt?.ToUniversalTime(),
					Title = request.Title,
					Type = request.Type
				};

				await context.Transactions.AddAsync(transaction);
				await context.SaveChangesAsync();

				return new Response<Transaction?>(transaction, 201, "Transação criada com sucesso!");
			}
			catch
			{
				return new Response<Transaction?>(null, 500, "Não foi possível criar sua transação");
			}
		}

		public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
		{
			if (request is { Type: eTransactionType.Withdraw, Amount: >= 0 })
				request.Amount *= -1;

			try
			{
				var transaction = await context
					.Transactions
					.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

				if (transaction is null)
					return new Response<Transaction?>(null, 404, "Transação não encontrada");

				transaction.CategoryId = request.CategoryId;
				transaction.Amount = request.Amount;
				transaction.Title = request.Title;
				transaction.Type = request.Type;
				transaction.AccountedAt = request.PaidOrReceivedAt?.ToUniversalTime();

				context.Transactions.Update(transaction);
				await context.SaveChangesAsync();

				return new Response<Transaction?>(transaction);
			}
			catch
			{
				return new Response<Transaction?>(null, 500, "Não foi possível recuperar sua transação");
			}
		}

		public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
		{
			try
			{
				var transaction = await context
					.Transactions
					.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

				if (transaction is null)
					return new Response<Transaction?>(null, 404, "Transação não encontrada");

				context.Transactions.Remove(transaction);
				await context.SaveChangesAsync();

				return new Response<Transaction?>(transaction);
			}
			catch
			{
				return new Response<Transaction?>(null, 500, "Não foi possível recuperar sua transação");
			}
		}

		public async Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
		{
			try
			{
				var transaction = await context
					.Transactions
					.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

				return transaction is null
					? new Response<Transaction?>(null, 404, "Transação não encontrada")
					: new Response<Transaction?>(transaction);
			}
			catch
			{
				return new Response<Transaction?>(null, 500, "Não foi possível recuperar sua transação");
			}
		}

		public async Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionsByPeriodRequest request)
		{
			try
			{
				request.StartDate ??= DateTime.Now.GetFirstDay();
				request.EndDate ??= DateTime.Now.GetLastDay();
			}
			catch
			{
				return new PagedResponse<List<Transaction>?>(null, 500,
					"Não foi possível determinar a data de início ou término");
			}

			try
			{
				var query = context
					.Transactions
					.AsNoTracking()
					.Where(x =>
						x.AccountedAt >= request.StartDate &&
						x.AccountedAt <= request.EndDate &&
						x.UserId == request.UserId)
					.OrderBy(x => x.AccountedAt);

				var transactions = await query
					.Skip((request.PageNumber - 1) * request.PageSize)
					.Take(request.PageSize)
					.ToListAsync();

				var count = await query.CountAsync();

				return new PagedResponse<List<Transaction>?>(
					transactions,
					count,
					request.PageNumber,
					request.PageSize);
			}
			catch
			{
				return new PagedResponse<List<Transaction>?>(null, 500, "Não foi possível obter as transações");
			}
		}
	}
}
