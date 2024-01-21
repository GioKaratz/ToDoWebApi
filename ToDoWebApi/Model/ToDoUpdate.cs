using System.ComponentModel.DataAnnotations;

namespace ToDoWebApi.Model
{
    public class ToDoUpdate
    {
        public string Description { get; set; }
        public bool Completed { get; set; }
    }
}