using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Dtos.JobDtos;
using FreelanceBoard.Core.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Commands.JobCommands
{
    public class UpdateJobCommand : IRequest<Result<JobDto>>
    {
        public int Id { get; }
        public string Title { get; }
        public string Description { get; }
        public string Category { get; }
        public decimal Price { get; }
        public string UserId { get; }
        public int ContractId { get; }
        public DateTime Deadline { get; }
        public List<string> SkillNames { get; } = [];
        public List<int> ProposalIds { get; } = [];

        public UpdateJobCommand(int id, string title, string description, string category, decimal price, string userId, int contractId,
            DateTime deadline, List<string> skillNames, List<int> proposalIds)
        {
            Id = id;
            Title = title;
            Description = description;
            Category = category;
            Price = price;
            UserId = userId;
            ContractId = contractId;
            Deadline = deadline;
            SkillNames = skillNames ?? [];
            ProposalIds = proposalIds ?? [];
        }
    }
}
