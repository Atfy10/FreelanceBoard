using FreelanceBoard.MVC.Extensions;
using FreelanceBoard.MVC.Models;
using FreelanceBoard.MVC.Services.Interfaces;
using Newtonsoft.Json;
using System.Net.Http;
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
        public async Task<List<ClientDashboardViewModel>> GetAllJobsAsync(HttpContext httpContext, int page = 1)
        {
            var token = httpContext.User.GetAccessToken();
            var client = _httpClientFactory.CreateClient("FreelanceApiClient");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var userId = httpContext.User?.GetUserId();
            var response = await client.GetAsync($"/api/job/user/{userId}/jobs");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException("Failed to fetch jobs");

            var apiResult = await response.Content.ReadFromJsonAsync<ApiErrorResponse<List<ClientDashboardViewModel>>>();

            if (apiResult is null || !apiResult.IsSuccess)
                throw new ApplicationException(apiResult?.Message ?? "No jobs found");

            return apiResult.Data;
        }
        public async Task<List<JobViewModel>> GetAllJobsSortedAsync(HttpContext httpContext, string sortBy, int page = 1)
        {
            var token = httpContext.User.GetAccessToken();
            var client = _httpClientFactory.CreateClient("FreelanceApiClient");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync($"/api/job/sort-by?field={sortBy}&page={page}");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException("Failed to fetch sorted jobs");

            var apiResult = await response.Content.ReadFromJsonAsync<ApiErrorResponse<List<JobViewModel>>>();

            return apiResult?.Data ?? new List<JobViewModel>();
        }
        public async Task<JobViewModel> GetJobByIdAsync(HttpContext httpContext, int id)
        {
            var token = httpContext.User.GetAccessToken();
            var client = _httpClientFactory.CreateClient("FreelanceApiClient");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync($"/api/job/get/{id}");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Failed to fetch job with ID {id}");

            var apiResult = await response.Content.ReadFromJsonAsync<ApiErrorResponse<JobViewModel>>();

            return apiResult?.Data ?? new JobViewModel();
        }

    }


}
