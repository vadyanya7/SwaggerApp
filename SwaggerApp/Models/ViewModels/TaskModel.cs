using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerApp.Models.ViewModels
{
    public class TaskModel
    {
        public string TaskDescription { get; set; }
        public int UserId { get; set; }
        public  UserModel User { get; set; }
    }
}
