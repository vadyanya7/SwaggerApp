using SwaggerApp.Models;

namespace Swagger.Models
{
    public class Task : BaseEntity
    {       
        public string TaskDescription { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}