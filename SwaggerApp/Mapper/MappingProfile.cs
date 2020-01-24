using AutoMapper;
using Swagger.Models;
using SwaggerApp.Models.ViewModels;


namespace SwaggerApp.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserModel, User>();
            CreateMap<User, UserModel>();
            CreateMap<UpdateUserModel, User>();
            CreateMap<User, UpdateUserModel>();
            CreateMap<Office, UpdateOfficeModel>();
            CreateMap<Office, OfficeModel>();
            CreateMap<UpdateOfficeModel, Office>();
            CreateMap<OfficeModel,Office >();
            CreateMap<Task, TaskModel>();
            CreateMap<Task, UpdateTaskModel>();
            CreateMap< UpdateTaskModel, Task>();
            CreateMap< TaskModel,Task>();
        }
    }
}
