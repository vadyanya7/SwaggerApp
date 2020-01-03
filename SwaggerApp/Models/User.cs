using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Swagger.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public  int Age { get; set; }
        public Office Office { get; set; }
        public ICollection<Task> Tasks { get; set; }
        
        public User()
        {
            Tasks = new List<Task>();
        }
    }
}