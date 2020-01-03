using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Swagger.Models
{
    public class Task
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string TaskDescription { get; set; }
        public User User { get; set; }
    }
}