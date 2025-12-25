using Application.Usecases.ToDos;
using AutoMapper;
using Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Dto.Request.ToDos;
using Presentation.Dto.Response;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoController(
        ISaveToDoUsecase saveToDoUsecase,
        IFindToDoByIdUsecase findToDoByIdUsecase,
        IFindAllToDosUsecase findAllToDosUsecase,
        IDeleteToDoUsecase deleteToDoUsecase,
        IMapper mapper
    ) : Controller
    {
        private readonly ISaveToDoUsecase saveToDoUsecase = saveToDoUsecase;
        private readonly IFindToDoByIdUsecase findToDoByIdUsecase = findToDoByIdUsecase;
        private readonly IFindAllToDosUsecase findAllToDosUsecase = findAllToDosUsecase;
        private readonly IDeleteToDoUsecase deleteToDoUsecase = deleteToDoUsecase;
        private readonly IMapper mapper = mapper;

        [HttpPost]
        public async Task<IActionResult> AddNewToDo([FromBody] InsertToDoRequestDto insertToDoDto)
        {
            ToDo toDo = mapper.Map<ToDo>(insertToDoDto);
            toDo = await saveToDoUsecase.Execute(toDo);
            return Ok(new AppDefaultResponse(toDo));
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateTodo([FromBody] UpdateToDoDto updateTodoDto)
        {
            ToDo toDo = mapper.Map<ToDo>(updateTodoDto);
            toDo = await saveToDoUsecase.Execute(toDo);
            return Ok(new AppDefaultResponse(toDo));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindToDoById([FromRoute] int id)
        {
            ToDo toDo = await findToDoByIdUsecase.Execute(id);
            return Ok(new AppDefaultResponse(toDo));
        }

        [HttpGet]
        public async Task<IActionResult> FindAllTodos()
        {
            IEnumerable<ToDo>? toDos = await findAllToDosUsecase.Execute();
            return Ok(new AppDefaultResponse(toDos));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDo([FromRoute] int id)
        {
            await deleteToDoUsecase.Execute(id);
            return Ok(new AppDefaultResponse(true));
        }
    }
}
