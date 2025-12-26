using AutoMapper;
using Domain.Entity;
using Presentation.Dto.Request.ToDos;

namespace Presentation.Mapping
{
    public class ToDoMappingProfile : Profile
    {
        public ToDoMappingProfile() {
            CreateMap<InsertToDoRequestDto, ToDo>();
            CreateMap<UpdateToDoDto, ToDo>();
        }
    }
}
