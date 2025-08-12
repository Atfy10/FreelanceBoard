using FreelanceBoard.Core.Dtos;
using FreelanceBoard.Core.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Commands.JobCommands
{
    public class CreateJobCommand : IRequest<Result<int>>
    {
        public string Title { get; }
        public string Description { get; }
        public string Category { get; }
        public decimal Price { get; }
        public string UserId { get; }
        public DateTime Deadline { get; }
        public DateTime DateCreated { get; }
        public List<string> SkillNames { get; } = [];

        public CreateJobCommand(string title, string description, string category, decimal price, string userId,
            DateTime dateCreated, List<string> skillNames, DateTime deadline)
        {
            Title = title;
            Description = description;
            Category = category;
            Price = price;
            Deadline = deadline;
            DateCreated = dateCreated;
            UserId = userId;
            SkillNames = skillNames ?? [];
        }

    }
}
