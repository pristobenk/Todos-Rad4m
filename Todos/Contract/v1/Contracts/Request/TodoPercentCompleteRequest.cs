using System;
using System.ComponentModel.DataAnnotations;

namespace Todos.Contract.v1.Contracts.Request
{
    public class TodoPercentCompleteRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int PercentComplete { get; set; }
    }
}
