using System;
namespace Todos.ViewModels
{
    public class TodoUpdateRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
