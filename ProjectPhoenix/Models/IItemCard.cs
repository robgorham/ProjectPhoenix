using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectPhoenix.Models
{
    interface IItemCard: IBaseModel
    {
        string Name { get; set; }
        string Description { get; set; }
        int Order { get; set; }
        Guid BoardId { get; set; }
        Guid ColumnId { get; set; }
        ApplicationUser User { get; set; }

    }
}
