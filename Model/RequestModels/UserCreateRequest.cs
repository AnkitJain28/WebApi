using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.RequestModels
{
    public class UserCreateRequest
    {
        public string Name { get; set; } = null!;

        public string? MoblieNumber { get; set; }

        public string? City { get; set; }
    }
}
