using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectPhoenix.Models
{
    interface IBoard : IBaseModel
    {
        string name { get; set; }
        IList<Column> Columns { get; set; }
    }
    
}
