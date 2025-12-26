using Domain.Gateway;

namespace Application.Usecases.ToDos
{
    public interface IDeleteToDoUsecase
    {
        Task Execute(int id);
    }

    public class DeleteToDoUsecase(ITodoGateway todoGateway) : IDeleteToDoUsecase
    {
        private readonly ITodoGateway todoGateway = todoGateway;

        public async Task Execute(int id) => await todoGateway.Delete(id);
    }
}
