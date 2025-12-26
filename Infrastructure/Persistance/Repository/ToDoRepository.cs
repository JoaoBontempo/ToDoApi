using Domain.Entity;
using Domain.Gateway;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Repository
{
    public class ToDoRepository(AppDbContext context) : ITodoGateway
    {
        private readonly AppDbContext _context = context;

        private async Task<ToDo?> FindTrackedById(int id) => await
            _context
            .ToDos
            .FindAsync(id);

        private async Task SaveState() => await _context.SaveChangesAsync();

        public async Task<IEnumerable<ToDo>?> FindAll() => await _context.ToDos.AsNoTracking().ToListAsync();
        public async Task<ToDo?> FindById(int id) => await _context
            .ToDos.AsNoTracking()
            .FirstOrDefaultAsync(todo => todo.Id == id);

        public async Task<ToDo> Insert(ToDo toDo)
        {
            await _context.ToDos.AddAsync(toDo);
            await SaveState();
            return toDo;
        }

        public async Task<ToDo?> Update(ToDo toDo)
        {
            ToDo? currentTodo = await this.FindTrackedById(toDo.Id);
            if (currentTodo is null)
                return currentTodo;

            currentTodo.Title = toDo.Title;
            currentTodo.FinishedAt = toDo.FinishedAt;
            currentTodo.Description = toDo.Description;
            currentTodo.Status = toDo.Status;

            await SaveState();
            return currentTodo;
        }

        public async Task<bool> Delete(int id)
        {
            ToDo? currentTodo = await this.FindTrackedById(id);
            if (currentTodo is null)
                return false;

            _context.ToDos.Remove(currentTodo);
            await SaveState();
            return true;
        }
    }
}
