using System;
using System.ComponentModel.DataAnnotations;

namespace Todos.Contract.v1.Contracts.Request
{
    public class TodoCreateRequest
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        
        public DateTime ExpiryDate { get; set; } = DateTime.UtcNow.AddDays(7); 
    }
}
