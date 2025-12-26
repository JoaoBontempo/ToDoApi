using Domain.Entity;
using Domain.Gateway;

namespace Application.Usecases.ToDos
{
    public interface IFindAllToDosUsecase
    {
        Task<IEnumerable<ToDo>?> Execute();
    }

    public class FindAllToDosUsecase(ITodoGateway todoGateway) : IFindAllToDosUsecase
    {
        private readonly ITodoGateway todoGateway = todoGateway;

        public async Task<IEnumerable<ToDo>?> Execute() => await todoGateway.FindAll();
    }
}
