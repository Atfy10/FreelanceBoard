using FluentValidation;
using FreelanceBoard.Core.Commands.JobCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Validators.JobValidators
{
    public class DeleteJobCommandValidator : AbstractValidator<DeleteJobCommand>
    {
        public DeleteJobCommandValidator()
        {
            RuleFor(x => x.JobId).GreaterThan(0).NotEmpty();
        }
    }
}
