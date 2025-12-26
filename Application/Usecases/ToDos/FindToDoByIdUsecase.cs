using Domain.Entity;
using Domain.Gateway;

namespace Application.Usecases.ToDos
{
    public interface IFindToDoByIdUsecase
    {
        Task<ToDo> Execute(int id);
    }

    public class FindToDoByIdUsecase(ITodoGateway todoGateway) : IFindToDoByIdUsecase
    {
        private readonly ITodoGateway todoGateway = todoGateway;

        public async Task<ToDo> Execute(int id) {
            ToDo? toDo = await todoGateway.FindById(id) ?? throw new Shared.Exceptions.AppEntityNotFoundException(Shared.Constants.AppMessages.ToDoValidations.TODO_NOT_FOUND);
            return toDo;
        }
    }
}
