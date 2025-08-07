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
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public string UserId { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime DateCreated { get; set; }
        public List<string> SkillNames { get; set; } = new List<string>();



        public CreateJobCommand(string title, string description, string category, decimal price, string userId,
            DateTime dateCreated,List<string> skillNames, DateTime deadline)
        {
            Title = title;
            Description = description;
            Category = category;
            Price = price;
            Deadline = deadline;
            DateCreated = dateCreated;
            UserId = userId;
            SkillNames = skillNames ?? new List<string>();
        }

    }
}
