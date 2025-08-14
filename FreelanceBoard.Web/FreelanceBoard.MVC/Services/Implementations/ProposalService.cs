using System.Net.Http.Headers;
using System.Text.Json;
using FreelanceBoard.Core.Queries.Interfaces;
using FreelanceBoard.MVC.Extensions;
using FreelanceBoard.MVC.Models;
using FreelanceBoard.MVC.Services.Interfaces;

namespace FreelanceBoard.MVC.Services.Implementations
{
    public class ProposalService : IProposalService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProposalService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        //public async Task<List<ProposalViewModel>> GetProposalsByJobIdAsync(int id, HttpContext httpContext)
        public async Task<JobProposalsViewModel> GetProposalsByJobIdAsync(int id, HttpContext httpContext)
        {
            var token = httpContext.User.GetAccessToken();
            var client = _httpClientFactory.CreateClient("FreelanceApiClient");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var proposalResponse = await client.GetAsync($"/api/Proposal/job/{id}/proposals");
            var jobResponse = await client.GetAsync($"/api/Job/get?id={id}");

            if (!proposalResponse.IsSuccessStatusCode)
                throw new ApplicationException("Failed to fetch proposals");

            if (!jobResponse.IsSuccessStatusCode)
                throw new ApplicationException("Failed to fetch job");

            var apiResult = await proposalResponse.Content.ReadFromJsonAsync<ApiErrorResponse<List<ProposalViewModel>>>();
            var jobApiResult = await jobResponse.Content.ReadFromJsonAsync<ApiErrorResponse<JobViewModel>>();

            if (apiResult is null || !apiResult.IsSuccess)
                throw new ApplicationException(apiResult?.Message ?? "No proposals found");

            var jobProposals = new JobProposalsViewModel();
            jobProposals.Proposals = apiResult.Data;
            jobProposals.JobPrice = jobApiResult.Data.Price;
            jobProposals.JobTitle = jobApiResult.Data.Title;
            jobProposals.JobDateCreated = jobApiResult.Data.DateCreated;
            return jobProposals;
        }

        public async Task<List<JobWithProposalsViewModel>> GetProposalsByFreelancerIdAsync(string freelancerId, HttpContext httpContext)
        {
            var token = httpContext.User.GetAccessToken();
            var client = _httpClientFactory.CreateClient("FreelanceApiClient");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync($"/api/Proposal/freelancer/{freelancerId}/proposals");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException("Failed to fetch proposals");

            var apiResult = await response.Content.ReadFromJsonAsync<ApiErrorResponse<List<JobWithProposalsViewModel>>>();

            if (apiResult is null || !apiResult.IsSuccess)
                throw new ApplicationException(apiResult?.Message ?? "No proposals found");

            return apiResult.Data;
        }

		public async Task<int> CreateProposalAsync(CreateProposalViewModel model, HttpContext httpContext)
		{
			var token = httpContext.User.GetAccessToken();
			if (string.IsNullOrEmpty(token))
				throw new ApplicationException("User not logged in.");

			var client = _httpClientFactory.CreateClient("FreelanceApiClient");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var response = await client.PostAsJsonAsync("/api/Proposal/create", model);

			// Deserialize once as ApiErrorResponse<int>
			var apiResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse<int>>(
				new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			if (!response.IsSuccessStatusCode || apiResponse == null || !apiResponse.IsSuccess)
			{
				var errorMessage = apiResponse?.Message ?? "An unexpected error occurred.";
				throw new ApplicationException(errorMessage);
			}

			if (apiResponse.Data <= 0)
				throw new ApplicationException("Proposal creation returned an invalid ID.");

			return apiResponse.Data;
		}


	}
}
