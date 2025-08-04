using AutoMapper;
using AutoMapper.Execution;
using FreelanceBoard.Core.Commands;
using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.CommandHandlers
{
    public class CreateFileCommandHandler : IRequestHandler<CreateFileCommand, bool>
    {
        private readonly IMapper mapper;
        private readonly IBaseRepository<Project> baseRepository;
        public CreateFileCommandHandler(IMapper mapper, IBaseRepository<Project> baseRepository)
        {
            this.mapper = mapper;
            this.baseRepository = baseRepository;
        }
        public async Task<bool> Handle(CreateFileCommand request, CancellationToken cancellationToken)
        {
            if (request.File != null && request.File.Length > 0)
            {
                // Generate unique file name
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(request.File.FileName);
                var filePath = Path.Combine("wwwroot/uploads", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.File.CopyToAsync(stream);
                }

                var mapProject = mapper.Map<Project>(request);
                mapProject.Attachments = filePath;
                await baseRepository.AddAsync(mapProject);
                return true;
            }
            return false;

        }
    }
}
