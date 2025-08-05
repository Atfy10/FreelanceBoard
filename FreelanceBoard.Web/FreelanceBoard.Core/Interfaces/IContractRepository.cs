using FreelanceBoard.Core.Domain.Entities;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Interfaces
{


    // Ensure that the correct Contract type is referenced.
    // Remove: using System.Diagnostics.Contracts;

    public interface IContractRepository
    {
        Task<Contract> GetFullContractWithIdAsync(int contractId);
    }
}
