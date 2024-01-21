using ToDoWebApi.Model;

namespace ToDoWebApi.Data.Service
{
    public interface IToDoRepository
    {
        Task<IEnumerable<ToDo>> GetTodos(string userId);
        Task<ToDo> GetTodoById(string userId, int id);
        Task<ToDo> CreateTodo(string userId, ToDoCreate todoCreate);
        Task<ToDo> UpdateTodo(string userId, int id, ToDoUpdate toDoUpdate);
        Task<ToDo> DeleteTodo(string userId, int id);
    }
}
