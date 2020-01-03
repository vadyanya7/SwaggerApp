using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Swagger.Models
{
    public class Office
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}