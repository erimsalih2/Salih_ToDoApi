using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Salih_ToDoApi.Controllers;
using Salih_ToDoApi.Data;
using Salih_ToDoApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApi.Tests
{
    public class TodoControllerTests
    {
        private static TodoDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<TodoDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new TodoDbContext(options);
        }

        [Fact]
        public async Task GetTodos_ReturnsAllItems()
        {
            // Arrange
            var context = GetDbContext();
            context.Todos.Add(new TodoItem { Title = "Task 1", Description = "Desc" });
            context.Todos.Add(new TodoItem { Title = "Task 2", Description = "Desc" });
            await context.SaveChangesAsync();

            var controller = new TodoController(context);

            // Act
            var result = await controller.GetTodos();

            // Assert
            var items = Assert.IsType<List<TodoItem>>(result.Value);
            Assert.Equal(2, items.Count);
        }
        [Fact]
        public async Task GetTodoById_ReturnsTodo_WhenExists()
        {
            var context = GetDbContext();
            var todo = new TodoItem { Title = "Sample", Description = "Test" };
            context.Todos.Add(todo);
            await context.SaveChangesAsync();

            var controller = new TodoController(context);
            var result = await controller.GetTodoById(todo.Id);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnTodo = Assert.IsType<TodoItem>(okResult.Value);
            Assert.Equal(todo.Title, returnTodo.Title);
        }
        [Fact]
        public async Task GetTodoById_ReturnsNotFound_WhenMissing()
        {
            var controller = new TodoController(GetDbContext());
            var result = await controller.GetTodoById(999);
            Assert.IsType<NotFoundResult>(result.Result);
        }
        [Fact]
        public async Task CreateTodo_AddsItem_ReturnsCreated()
        {
            var context = GetDbContext();
            var controller = new TodoController(context);
            var newTodo = new TodoItem { Title = "New", Description = "Test" };

            var result = await controller.CreateTodo(newTodo);

            var created = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnTodo = Assert.IsType<TodoItem>(created.Value);
            Assert.Equal("New", returnTodo.Title);

            Assert.Single(context.Todos);
        }
        [Fact]
        public async Task UpdateTodo_UpdatesData_WhenExists()
        {
            var context = GetDbContext();
            var existing = new TodoItem { Title = "Old", Description = "Old" };
            context.Todos.Add(existing);
            await context.SaveChangesAsync();

            var controller = new TodoController(context);
            var updated = new TodoItem { Id = existing.Id, Title = "New", Description = "New" };

            var result = await controller.UpdateTodo(existing.Id, updated);
            Assert.IsType<NoContentResult>(result);

            var dbItem = await context.Todos.FirstAsync();
            Assert.Equal("New", dbItem.Title);
        }

        [Fact]
        public async Task UpdateTodo_ReturnsBadRequest_WhenIdMismatch()
        {
            var controller = new TodoController(GetDbContext());
            var result = await controller.UpdateTodo(1, new TodoItem { Id = 2, Title = "X", Description = "Y" });
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task UpdateTodo_ReturnsNotFound_WhenMissing()
        {
            var controller = new TodoController(GetDbContext());
            var result = await controller.UpdateTodo(1, new TodoItem { Id = 1, Title = "X", Description = "Y" });
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public async Task DeleteTodo_RemovesItem_WhenExists()
        {
            var context = GetDbContext();
            var todo = new TodoItem { Title = "ToDelete", Description = "..." };
            context.Todos.Add(todo);
            await context.SaveChangesAsync();

            var controller = new TodoController(context);
            var result = await controller.DeleteTodo(todo.Id);

            Assert.IsType<NoContentResult>(result);
            Assert.Empty(context.Todos);
        }

        [Fact]
        public async Task DeleteTodo_ReturnsNotFound_WhenMissing()
        {
            var controller = new TodoController(GetDbContext());
            var result = await controller.DeleteTodo(999);
            Assert.IsType<NotFoundResult>(result);
        }
    }

}
