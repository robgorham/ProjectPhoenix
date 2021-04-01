using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectPhoenix.Models
{
    interface IBoard : IBaseModel
    {
        String name { get; set; }
    }
    
}
