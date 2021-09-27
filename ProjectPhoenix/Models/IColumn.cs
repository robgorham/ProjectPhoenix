using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectPhoenix.Models
{
    interface IColumn : IBaseModel
    {
        string name { get; set; }
        int order { get; set; }
        Guid BoardId { get; set; }
        IList<ItemCard> ItemCards { get; set; }
    }
}
