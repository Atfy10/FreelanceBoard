using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.MVC.Extensions;
using FreelanceBoard.MVC.Models;
using FreelanceBoard.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
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

		public async Task CreateJobAsync(JobCreateViewModel model, HttpContext httpContext)
		{
            var token = httpContext.User.GetAccessToken();
            var client = _httpClientFactory.CreateClient("FreelanceApiClient");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            model.dateCreated = DateTime.UtcNow;
            model.SkillNames = model.SkillName.Split(',').ToList();
			var response = await client.PostAsJsonAsync("/api/Job/create", model);
			if (response.IsSuccessStatusCode)
				return;

			var apiError = await response.Content.ReadFromJsonAsync<ApiErrorResponse<bool?>>();
			var errorMessage = apiError?.Message ?? "An unexpected error occurred.";

			throw new ApplicationException(errorMessage);
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
        public async Task<List<JobViewModel>> GetAllJobsSortedAsync(HttpContext httpContext, string sortBy, bool isAscending = true, int page = 1)
        {
            var token = httpContext.User.GetAccessToken();
            var client = _httpClientFactory.CreateClient("FreelanceApiClient");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync($"/api/job/sort-by?field={sortBy}&sortAscendingly={isAscending}&page={page}");

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

            var response = await client.GetAsync($"/api/job/get?id={id}");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Failed to fetch job with ID {id}");

            var apiResult = await response.Content.ReadFromJsonAsync<ApiErrorResponse<JobViewModel>>();

            return apiResult?.Data ?? new JobViewModel();
        }

        //List<string> category, int page
        public async Task<List<JobViewModel>> GetJobByCategory(HttpContext httpConetext, List<string> categories, int page = 1)
        {
            var token = httpConetext.User.GetAccessToken();
            var client = _httpClientFactory.CreateClient("FreelanceApiClient");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            var response = await client.GetAsync($"/api/job/filter-by-category?category={categories}&page={page}");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Error occured while getting jobs with specified category");

            var apiResult = await response.Content.ReadFromJsonAsync<ApiErrorResponse<List<JobViewModel>>>();

            return apiResult.Data ?? new List<JobViewModel>();
        }

        //GetJobsFilteredSkills([FromQuery] List<string> skill, int page = 1)
        public async Task<List<JobViewModel>> GetJobBySkill(HttpContext httpConetext, List<string> skillNames, int page = 1)
        {
            var token = httpConetext.User.GetAccessToken();
            var client = _httpClientFactory.CreateClient("FreelanceApiClient");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            var response = await client.GetAsync($"/api/job/filter-by-skills?skill={skillNames}&page={page}");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Error occured while getting jobs with specified skills");

            var apiResult = await response.Content.ReadFromJsonAsync<ApiErrorResponse<List<JobViewModel>>>();

            return apiResult.Data ?? new List<JobViewModel>();
        }

        public async Task<List<JobViewModel>> GetJobByBudget(HttpContext httpConetext,int min, int max, int page = 1)
        {
            var token = httpConetext.User.GetAccessToken();
            var client = _httpClientFactory.CreateClient("FreelanceApiClient");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            var response = await client.GetAsync($"/api/job/filter-by-budget?min={min}&max={max}&page={page}");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Error occured while getting jobs with specified skills");

            var apiResult = await response.Content.ReadFromJsonAsync<ApiErrorResponse<List<JobViewModel>>>();

            return apiResult.Data ?? new List<JobViewModel>();
        }
    }


}
