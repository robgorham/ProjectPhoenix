using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectPhoenix.Models
{
    public class BaseModel : IBaseModel
    {
        public BaseModel() { }
        public BaseModel( Guid id) { }

        public virtual Guid id { get; set; }
        public DateTime updateDate { get; set; }
        public DateTime createDate { get; set; }
        public DateTime modifyDate { get; set ; }
    }
}
