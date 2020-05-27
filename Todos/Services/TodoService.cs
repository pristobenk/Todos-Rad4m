using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Todos.Database;
using Todos.Models;
using Todos.ViewModels;

namespace Todos.Services
{
    

    public interface ITodoService
    {
        Task<bool> CreateTodoAsync(Todo todo);
        Task<Todo> GetTodoByIdAsync(int id);
        Task<bool> UpdateTodoAsync(TodoUpdateRequest todo);
        Task<bool> DeleteTodoAsync(int id);
        Task<List<Todo>> GetAllAsync();
        Task<bool> SetPercentCompleteAsync(int id, int procentage);
        Task<bool> SetIsDoneAsync(int id, bool isDone);
        Task<List<Todo>> GetIncomingTaskToday();
        Task<List<Todo>> GetIncomingTaskNextDay();
        Task<List<Todo>> GetIncomingTaskThisWeek();
    }

    public class TodoService:ITodoService
    {
        private DataContext _Context;

        public TodoService(DataContext dbContext)
        {
            _Context = dbContext;
        }

        public async Task<bool> CreateTodoAsync(Todo todo)
        {
            await _Context.Todos.AddAsync(todo);
            var created = await _Context.SaveChangesAsync();
            return created > 0;

        }


        public async Task<Todo> GetTodoByIdAsync(int id)
        {
            return await _Context.Todos.SingleOrDefaultAsync(x => x.Id == id);

        }

        public async Task<bool> UpdateTodoAsync(TodoUpdateRequest todo)
        {
            Todo todoToUpdate = await GetTodoByIdAsync(todo.Id);

            if (todoToUpdate == null)
                return false;


            todoToUpdate.Description = todo.Description;
            todoToUpdate.Title = todo.Title;
            todoToUpdate.EditedAt = DateTime.UtcNow;


            _Context.Todos.Update(todoToUpdate);
            var updated = await _Context.SaveChangesAsync();
            return updated > 0;

        }

        public async Task<bool> DeleteTodoAsync(int id)
        {
            var todoToDeleted = await GetTodoByIdAsync(id);

            if (todoToDeleted == null)
                return false;

            _Context.Todos.Remove(todoToDeleted);
            var deleted = await _Context.SaveChangesAsync();
            return deleted > 0;
        }

        public Task<List<Todo>> GetAllAsync()
        {
            var queryable = _Context.Todos.AsQueryable();
            return queryable.ToListAsync();
        }

        public async Task<bool> SetPercentCompleteAsync(int id,int procentage)
        {
            var todoToUpdated = await GetTodoByIdAsync(id);

            if (todoToUpdated == null)
                return false;

            todoToUpdated.Percentage = procentage;
            todoToUpdated.EditedAt = DateTime.UtcNow;

            _Context.Todos.Update(todoToUpdated);

            var deleted = await _Context.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<bool> SetIsDoneAsync(int id, bool isDone)
        {
            var todoToUpdated = await GetTodoByIdAsync(id);

            if (todoToUpdated == null)
                return false;

            todoToUpdated.IsDone  = isDone;
            todoToUpdated.EditedAt = DateTime.UtcNow;

            _Context.Todos.Update(todoToUpdated);

            var deleted = await _Context.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<List<Todo>> GetIncomingTaskToday()
        {
            return await _Context.Todos
                .Where(x => x.ExpiryDate == DateTime.UtcNow)
                .ToListAsync();
        }

        public async Task<List<Todo>> GetIncomingTaskNextDay()
        {
            return await _Context.Todos
                .Where(x => x.ExpiryDate == DateTime.UtcNow.AddDays(1))
                .ToListAsync();
        }

        public async Task<List<Todo>> GetIncomingTaskThisWeek()
        {
            //look for date ranges this week
            DateTime now = DateTime.UtcNow;
            int delta = DayOfWeek.Monday - now.DayOfWeek;
            DateTime start = now.AddDays(delta);
            DateTime end = now.AddDays(5);
           
            return await _Context.Todos
                .Where(x => x.ExpiryDate >= start)
                .Where(x => x.ExpiryDate <= end)
                .ToListAsync();
        }
    }
}
