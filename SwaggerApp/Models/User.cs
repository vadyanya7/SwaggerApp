using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Swagger.Models
{
    public class User : IdentityUser<int>
    {
        public string SurName { get; set; }
        public  int Age { get; set; }
       
        public int OfficeId { get; set; }
        public Office Office { get; set; }
        public List<Task> Tasks { get; set; }

    }
}