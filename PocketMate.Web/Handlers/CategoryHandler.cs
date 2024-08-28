﻿using PocketMate.Core.Handlers;
using PocketMate.Core.Models;
using PocketMate.Core.Requests.Categories;
using PocketMate.Core.Responses;
using System.Net.Http.Json;

namespace PocketMate.Web.Handlers
{
    public class CategoryHandler(IHttpClientFactory httpClientFactory) : ICategoryHandler
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HTTP_CLIENT_NAME);

        public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
        {
            var result = await _client.PostAsJsonAsync("v1/categories", request);
            return await result.Content.ReadFromJsonAsync<Response<Category?>>()
                   ?? new Response<Category?>(null, 400, "Failed to create the category");
        }

        public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
        {
            var result = await _client.PutAsJsonAsync($"v1/categories/{request.Id}", request);
            return await result.Content.ReadFromJsonAsync<Response<Category?>>()
                   ?? new Response<Category?>(null, 400, "Failed to update the category");
        }

        public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
        {
            var result = await _client.DeleteAsync($"v1/categories/{request.Id}");
            return await result.Content.ReadFromJsonAsync<Response<Category?>>()
                   ?? new Response<Category?>(null, 400, "Failed to delete the category");
        }

        public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
            => await _client.GetFromJsonAsync<Response<Category?>>($"v1/categories/{request.Id}")
               ?? new Response<Category?>(null, 400, "Failed to retrieve the category");

        public async Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoriesRequest request)
            => await _client.GetFromJsonAsync<PagedResponse<List<Category>>>("v1/categories")
               ?? new PagedResponse<List<Category>>(null, 400, "Failed querying the categories");
    }
}
