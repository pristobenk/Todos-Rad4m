using System;
using System.ComponentModel.DataAnnotations;

namespace Todos.Contract.v1.Contracts.Request
{
    public class TodoIsDoneRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public bool IsDone { get; set; }
    }
}
