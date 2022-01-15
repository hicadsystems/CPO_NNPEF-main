

using Newtonsoft.Json;
using System;

namespace NNPEFWEB.Models
{
    public class RoleMenu
    {
        public int Id { get; set; }

        public Menu Menu { get; set; }
        public int MenuId { get; set; }

        [JsonIgnore]
        public Role Role { get; set; }
        public int RoleId { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }
    }
}
