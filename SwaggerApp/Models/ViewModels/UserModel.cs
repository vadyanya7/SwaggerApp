using Swagger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerApp.Models.ViewModels
{
    public class UserModel
    {
        public string UserName { get; set; }
        public string SurName { get; set; }
        public int Age { get; set; }
        public int OfficeId { get; set; }
        public OfficeModel Office { get; set; }
        public List<TaskModel> Tasks { get; set; }
    }
}
