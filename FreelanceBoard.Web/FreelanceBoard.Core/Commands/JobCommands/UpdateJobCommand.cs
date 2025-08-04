using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Commands.JobCommands
{
    public class UpdateJobCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public string UserId { get; set; }
        public int ContractId { get; set; }
        public List<string> SkillNames { get; set; } = new List<string>();
        public List<int> ProposalIds { get; set; } = new List<int>();


        public UpdateJobCommand(int id,string title, string description, string category, decimal price, string userId, int contractId, List<string> skillNames, List<int> proposalIds)
        {
            Id = id;
            Title = title;
            Description = description;
            Category = category;
            Price = price;
            UserId = userId;
            ContractId = contractId;
            SkillNames = skillNames ?? new List<string>();
            ProposalIds = proposalIds ?? new List<int>();
        }
    }
}
