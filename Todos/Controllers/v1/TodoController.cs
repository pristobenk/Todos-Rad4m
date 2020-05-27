using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Todos.Contract.v1.Contracts.Request;
using Todos.Contract.v1.Contracts.Response;
using Todos.Models;
using Todos.Services;
using Todos.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Todos.Controllers.v1
{
    public class TodoController : Controller
    {
        private readonly ITodoService _TodoService;
        public TodoController(ITodoService todoService)
        {
            _TodoService = todoService;
        }

        [HttpGet("api/v1/todos/create")]
        public async Task<IActionResult> Create(TodoCreateRequest todoCreateRequest)
        {
            try
            {
                var todo = new Todo
                {
                    Title = todoCreateRequest.Title,
                    Description = todoCreateRequest.Description,
                    ExpiryDate= todoCreateRequest.ExpiryDate
                };

                await _TodoService.CreateTodoAsync(todo);
            }
            catch(Exception ex)
            {
                return BadRequest(new PostResponse
                {
                    Success = false,
                    ErrorMessage=ex.Message
                });
            }

            return Ok(new PostResponse
            {
                Success=true
            });
        }

        [HttpPut("api/v1/todos/update")]
        public async Task<IActionResult> Update(TodoUpdateRequest todoUpdateRequest)
        {
            try
            {
                await _TodoService.UpdateTodoAsync(todoUpdateRequest);
            }
            catch(Exception ex)
            {
                return BadRequest(new PostResponse
                {
                    Success = false,
                    ErrorMessage = ex.Message
                });
            }

            return Ok(new PostResponse
            {
                Success = true
            });
            
        }

        [HttpDelete("api/v1/todos/update")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _TodoService.DeleteTodoAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new PostResponse
                {
                    Success = false,
                    ErrorMessage = ex.Message
                });
            }

            return Ok(new PostResponse
            {
                Success = true
            });
        }

        [HttpGet("api/v1/todos/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _TodoService.GetAllAsync());
        }

        [HttpGet("api/v1/todos/GetIncomingTaskToday")]
        public async Task<IActionResult> GetIncomingTaskToday()
        {
            return Ok(await _TodoService.GetIncomingTaskToday());
        }

        [HttpGet("api/v1/todos/GetIncomingNextDay")]
        public async Task<IActionResult> GetIncomingNextDay()
        {
            return Ok(await _TodoService.GetIncomingTaskNextDay());
        }

        [HttpGet("api/v1/todos/GetIncomingTaskThisWeek")]
        public async Task<IActionResult> GetIncomingTaskThisWeek()
        {
            return Ok(await _TodoService.GetIncomingTaskThisWeek());
        }

        [HttpPut("api/v1/todos/SetPercentComplete")]
        public async Task<IActionResult> SetPercentComplete(TodoPercentCompleteRequest todoPercentCompleteRequest)
        {
            try
            {
                await _TodoService.SetPercentCompleteAsync(todoPercentCompleteRequest.Id, todoPercentCompleteRequest.PercentComplete);
            }
            catch (Exception ex)
            {
                return BadRequest(new PostResponse
                {
                    Success = false,
                    ErrorMessage = ex.Message
                });
            }

            return Ok(new PostResponse
            {
                Success = true
            });
        }

        [HttpPut("api/v1/todos/MarkAsDone")]
        public async Task<IActionResult> MarkAsDone(TodoIsDoneRequest todoPercentCompleteRequest)
        {
            try
            {
                await _TodoService.SetIsDoneAsync(todoPercentCompleteRequest.Id, todoPercentCompleteRequest.IsDone);
            }
            catch (Exception ex)
            {
                return BadRequest(new PostResponse
                {
                    Success = false,
                    ErrorMessage = ex.Message
                });
            }

            return Ok(new PostResponse
            {
                Success = true
            });
        }

    }
}
