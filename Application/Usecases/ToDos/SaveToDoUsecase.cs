using Domain.Entity;
using Domain.Gateway;
using Shared.Constants;

namespace Application.Usecases.ToDos
{
    public interface ISaveToDoUsecase
    {
        Task<ToDo> Execute(ToDo toDo);
    }

    public class SaveToDoUsecase(ITodoGateway todoGateway) : ISaveToDoUsecase
    {
        private readonly ITodoGateway todoGateway = todoGateway;

        public async Task<ToDo> Execute(ToDo toDo)
        {
            if(toDo is null)
                throw new ApplicationException(AppMessages.ToDoValidations.TODO_IS_REQUIRED);

            toDo.EnsureIsValid();

            if (toDo.Id == 0)
                return await todoGateway.Insert(toDo);

            ToDo? savedtoDo = await todoGateway.Update(toDo) ?? throw new ApplicationException(AppMessages.ToDoValidations.TODO_NOT_FOUND);
            return savedtoDo;
        }
    }
}
