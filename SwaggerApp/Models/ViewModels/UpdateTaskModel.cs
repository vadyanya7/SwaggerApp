using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerApp.Models.ViewModels
{
    public class UpdateTaskModel:BaseEntity
    {
        public string TaskDescription { get; set; }
        public string UserId { get; set; }
        public UpdateUserModel User { get; set; }
    }
}
