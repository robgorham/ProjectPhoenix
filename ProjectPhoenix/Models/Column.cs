using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectPhoenix.Models
{
    public class Column : BaseModel, IColumn
    {
        public string name { get; set; }
        public int order { get; set; }
        public virtual Guid BoardId {get; set;}
        public virtual IList<ItemCard> ItemCards { get; set; }
        public ApplicationUser user { get; set; }
    }
}
