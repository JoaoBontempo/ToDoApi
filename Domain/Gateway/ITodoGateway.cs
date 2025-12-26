using Domain.Entity;

namespace Domain.Gateway
{
    public interface ITodoGateway
    {
        Task<ToDo?> FindById(int id);
        Task<IEnumerable<ToDo>?> FindAll();
        Task<ToDo> Insert(ToDo toDo);
        Task<ToDo?> Update(ToDo toDo);
        Task<bool> Delete(int id);
    }
}
