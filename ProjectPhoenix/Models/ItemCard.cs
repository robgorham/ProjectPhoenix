using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectPhoenix.Models
{
    public class ItemCard :BaseModel, IItemCard
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public virtual Guid BoardId{ get; set; }
        public virtual Guid ColumnId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
