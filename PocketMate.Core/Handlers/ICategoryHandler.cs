using PocketMate.Core.Models;
using PocketMate.Core.Requests.Categories;
using PocketMate.Core.Responses;

namespace PocketMate.Core.Handlers
{
	public interface ICategoryHandler
	{
		Task<Response<Category?>> CreateAsync(CreateCategoryRequest request);
		Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request);
		Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request);
		Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request);
		Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoriesRequest request);
	}
}
