using AutoMapper;
using AutoMapper.Execution;
using FreelanceBoard.Core.Commands;
using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Domain.Enums;
using FreelanceBoard.Core.Helpers;
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
    public class CreateFileCommandHandler : IRequestHandler<CreateFileCommand, Result<bool>>
    {
        private readonly IMapper mapper;
        private readonly IBaseRepository<Project> baseRepository;
        private readonly IUserAccessor _userAccessor;
        OperationExecutor _executor;
        public CreateFileCommandHandler(IMapper mapper, IBaseRepository<Project> baseRepository, OperationExecutor executor, IUserAccessor userAccessor)
        {
            this.mapper = mapper;
            this.baseRepository = baseRepository;
            _executor = executor;
            _userAccessor = userAccessor;
        }
        public async Task<Result<bool>> Handle(CreateFileCommand request, CancellationToken cancellationToken)
            => await _executor.Execute(async () =>
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

                    var userId = _userAccessor.GetUserId();
                    var mapProject = mapper.Map<Project>(request);
                    mapProject.Attachments = filePath;
                    mapProject.UserId = userId;
                    await baseRepository.AddAsync(mapProject);

                    return Result<bool>.Success(true, OperationType.Add.ToString());
                }

                return Result<bool>.Failure(OperationType.Add.ToString(), "Failed to add file");
            }, OperationType.Add);
    }
}
