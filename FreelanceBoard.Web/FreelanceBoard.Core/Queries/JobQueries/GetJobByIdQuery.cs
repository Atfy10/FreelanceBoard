using FreelanceBoard.Core.Dtos.JobDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Queries.JobQueries
{
    public  record GetJobByIdQuery : IRequest<JobDto>
    {
        public int Id { get; init; }
        public GetJobByIdQuery(int id)
        {
            Id = id;
        }
    }
}
