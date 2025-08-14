using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Domain.Entities
{
    public class CategoryJob
    {
        public int CategoriesId { get; set; }
        public int JobsId { get; set; }

        // Navigation properties
        public virtual Category Category { get; set; }
        public virtual Job Job { get; set; }
    }
}
