
namespace SwaggerApp.Models.ViewModels
{
    public class UpdateOfficeModel : BaseEntity
    {
        public string Name { get; set; }
        public UserModel User { get; set; }
    }
}
