using AutoMapper;
using FreelanceBoard.Core.Commands.JobCommands;
using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Dtos;
using FreelanceBoard.Core.Interfaces;
using FreelanceBoard.Core.Validators.JobValidators;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.CommandHandlers.JobHandlers
{
    public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand,int>
    {
        private readonly IJobRepository _jobRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly IMapper _mapper;


        public CreateJobCommandHandler(IJobRepository jobRepo, ISkillRepository skillRepository, IMapper mapper)
        {
            _jobRepository = jobRepo ?? throw new ArgumentNullException(nameof(jobRepo));
            _skillRepository = skillRepository ?? throw new ArgumentNullException(nameof(skillRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


         public async Task<int> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            var newJob = _mapper.Map<Job>(request);
            var skills = await _skillRepository.GetByNamesAsync(request.SkillNames);
            newJob.Skills = skills;


            if (skills.Count != request.SkillNames.Count)
            {
                return -1; // Indicating that one or more skills were not found
            }
            await _jobRepository.AddAsync(newJob);
            return newJob.Id;

        }
    }
}
