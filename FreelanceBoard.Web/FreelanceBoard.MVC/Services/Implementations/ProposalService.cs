using FreelanceBoard.Core.Queries.Interfaces;
using FreelanceBoard.MVC.Extensions;
using FreelanceBoard.MVC.Models;
using FreelanceBoard.MVC.Services.Interfaces;
using System.Net.Http.Headers;

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
            jobProposals.JobDateCreated = jobApiResult.Data.PostedDate;
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
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<ApiErrorResponse<CreateProposalViewModel>>();
                throw new ApplicationException($"Failed to create proposal: {error.Message}");
            }

            var newId = await response.Content.ReadFromJsonAsync<ApiErrorResponse<int>>();
            if (newId.Data <= 0)
                throw new ApplicationException("Proposal creation returned an invalid ID.");

            return newId.Data;
        }

    }
}
