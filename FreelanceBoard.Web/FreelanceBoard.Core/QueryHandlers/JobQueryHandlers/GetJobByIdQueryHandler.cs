using AutoMapper;
using FreelanceBoard.Core.Dtos.JobDtos;
using FreelanceBoard.Core.Interfaces;
using FreelanceBoard.Core.Queries.JobQueries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.QueryHandlers.JobQueryHandlers
{
    internal class GetJobByIdQueryHandler : IRequestHandler<GetJobByIdQuery, JobDto>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IMapper _mapper;

        public GetJobByIdQueryHandler(IJobRepository jobRepo, IMapper mapper)
        {
            _jobRepository = jobRepo;
            _mapper = mapper;
        }
        public async Task<JobDto> Handle(GetJobByIdQuery request, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetFullJobWithIdAsync(request.Id);
            if (job == null)
                return null;              // controller will turn this into 404

            return _mapper.Map<JobDto>(job);
        }
    }
}
