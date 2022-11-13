using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.RequestModels
{
    public class UserUpdateRequest
    {
        public int Id { get; set; }

        public string? Name { get; set; } 

        public string? MoblieNumber { get; set; }

        public string? City { get; set; }
    }
}
