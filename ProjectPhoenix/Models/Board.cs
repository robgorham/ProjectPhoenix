using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectPhoenix.Models
{
    public class Board : BaseModel, IBoard
    {
        public Board() { }
        public virtual string name { get; set; }
        public ApplicationUser user { get; set; }
    }
}
