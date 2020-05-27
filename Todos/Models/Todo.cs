using System;
using System.ComponentModel.DataAnnotations;

namespace Todos.Models
{
    public class Todo
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? EditedAt { get; set; }
        public int Percentage { get; set; } = 0;
        public bool IsDone { get; set; } = false;
    }
}
