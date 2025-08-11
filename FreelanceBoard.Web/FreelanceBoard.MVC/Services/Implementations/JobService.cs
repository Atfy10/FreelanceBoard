using FreelanceBoard.MVC.Extensions;
using FreelanceBoard.MVC.Models;
using FreelanceBoard.MVC.Services.Interfaces;
using System.Net.Http.Headers;

namespace FreelanceBoard.MVC.Services.Implementations
{
    public class JobService : IJobService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public JobService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        //get all jobs using the user id
        public async Task<List<ClientDashboardViewModel>> GetAllJobsAsync(HttpContext httpContext)
        {
            var token = httpContext.User.GetAccessToken();
            var client = _httpClientFactory.CreateClient("FreelanceApiClient");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var userId = httpContext.User?.GetUserId();
            var response = await client.GetAsync($"/api/jobs/user/{userId}/jobs");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException("Failed to fetch jobs");

            var apiResult = await response.Content.ReadFromJsonAsync<ApiErrorResponse<List<ClientDashboardViewModel>>>();

            if (apiResult is null || !apiResult.IsSuccess)
                throw new ApplicationException(apiResult?.Message ?? "No jobs found");

            return apiResult.Data;
        }

        public async Task<List<JobViewModel>> GetAllJobsSortedAsync(HttpContext httpContext, string sortBy)
        {
            var token = httpContext.User.GetAccessToken();
            var client = _httpClientFactory.CreateClient("FreelanceApiClient");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync($"/api/jobs/Sorted by date or budget?sortBy={sortBy}");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException("Failed to fetch sorted jobs");

            var apiResult = await response.Content.ReadFromJsonAsync<ApiErrorResponse<List<JobViewModel>>>();

            return apiResult?.Data ?? new List<JobViewModel>();
        }


    }


}
