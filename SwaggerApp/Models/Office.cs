using SwaggerApp.Models;

namespace Swagger.Models
{
    public class Office : BaseEntity
    {
        public string Name { get; set; }
        public User User { get; set; }
    }
}