using Microsoft.EntityFrameworkCore;
using ToDoWebApi.Model;

namespace ToDoWebApi.Data.Service
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly AppDbContext _context;

        public ToDoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ToDo> CreateTodo(string userId, ToDoCreate todoCreate)
        {
            var todo = new ToDo
            {
                Description = todoCreate.Description,
                Completed = false,
                UserId = userId
            };

            await _context.ToDos.AddAsync(todo);
            await _context.SaveChangesAsync();
            return todo;
        }

        public async Task<ToDo> DeleteTodo(string userId, int id)
        {
            var todo = await _context.ToDos.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
            
            _context.ToDos.Remove(todo);
            await _context.SaveChangesAsync();
            return todo;
        }

        public async Task<ToDo> GetTodoById(string userId, int id)
        {
            var todo = await _context.ToDos.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
            
            return todo;

        }

        public async Task<IEnumerable<ToDo>> GetTodos(string userId)
        {
            return await _context.ToDos
            .Where(u => u.UserId == userId)
            .ToListAsync();
        }

        public async Task<ToDo> UpdateTodo(string userId, int id, ToDoUpdate toDoUpdate)
        {
            var todo = await _context.ToDos.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
            
            todo.Description = toDoUpdate.Description;
            todo.Completed = toDoUpdate.Completed;

            _context.ToDos.Update(todo);
            await _context.SaveChangesAsync();

            return todo;
        }
    }

}
