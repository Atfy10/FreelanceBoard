using FreelanceBoard.MVC.Extensions;
using FreelanceBoard.MVC.Models;
using FreelanceBoard.MVC.Services.Interfaces;
using System.Net.Http.Headers;

namespace FreelanceBoard.MVC.Services.Implementations
{
    public class ReviewService(IHttpClientFactory httpClientFactory)
        : IReviewService
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

        public async Task<ReviewViewModel[]> GetTopThreeReviewsAsync(HttpContext httpContext)
        {
            var token = httpContext.User.GetAccessToken();

            var client = _httpClientFactory.CreateClient("FreelanceApiClient");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync($"/api/review/get-top-three");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException("Failed to fetch review");

            var apiResult = await response.Content.ReadFromJsonAsync<ApiErrorResponse<ReviewViewModel[]>>();

            if (apiResult is null || !apiResult.IsSuccess)
                throw new ApplicationException(apiResult?.Message ?? "No review found");

            return apiResult?.Data ?? [];
        }
    }
}
