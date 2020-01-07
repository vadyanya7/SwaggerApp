using SwaggerApp.Models;
using System.Collections.Generic;

namespace Swagger.Models
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public  int Age { get; set; }
       
        public int OfficeId { get; set; }
        public Office Office { get; set; }
        public List<Task> Tasks { get; set; }
        
    }
}