using System;
namespace Todos.Contract.v1.Contracts.Response
{
    public class PostResponse
    {
        public bool Success { get; set; }
        public string  ErrorMessage { get; set; }
    }
}
