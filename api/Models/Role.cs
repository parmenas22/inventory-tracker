using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Role : BaseModel
    {
        public Guid RoleId { get; set; }
        public string Name { get; set; }
    }
}