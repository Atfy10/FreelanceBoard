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

        public async Task<List<ProposalViewModel>> GetProposalsByJobIdAsync(int id, HttpContext httpContext)
        {
            var token = httpContext.User.GetAccessToken();
            var client = _httpClientFactory.CreateClient("FreelanceApiClient");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync($"/api/Proposal/job/{id}/proposals");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException("Failed to fetch proposals");

            var apiResult = await response.Content.ReadFromJsonAsync<ApiErrorResponse<List<ProposalViewModel>>>();

            if (apiResult is null || !apiResult.IsSuccess)
                throw new ApplicationException(apiResult?.Message ?? "No proposals found");

            return apiResult.Data;
        }
    }
}
