using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectPhoenix.Models
{
    interface IBaseModel
    {
        Guid id { get; set; }
        DateTime updateDate { get; set; }
        DateTime createDate { get; set; }
        DateTime modifyDate { get; set; }
    }
}
