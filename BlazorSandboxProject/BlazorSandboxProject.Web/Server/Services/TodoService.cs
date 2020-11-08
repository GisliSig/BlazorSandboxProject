using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorSandboxProject.Web.Server.EFContext;
using BlazorSandboxProject.Web.Server.Hubs;
using Grpc.Core;
using GrpcTodo;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace BlazorSandboxProject.Web.Server.Services
{
    public class TodoService : Todo.TodoBase
    {
        private readonly TodoDBContext dbContext;
        private readonly IHubContext<TodoHub> _todoHub;

        public TodoService(TodoDBContext context,IHubContext<TodoHub> todoHub)
        {
            this.dbContext = context;
            _todoHub = todoHub;
        }
        public override async Task<AddTodoReply> AddTodo(AddTodoRequest request, ServerCallContext context)
        {
            var reply = new AddTodoReply() { Text = request.Text + " added" };

            var todo = new Domain.Entities.Todo();
            todo.Text = request.Text;

            dbContext.Todo.Add(todo);
            await dbContext.SaveChangesAsync();

            await _todoHub.Clients.All.SendAsync("ReceiveTodo",new TodoItem() { Id = todo.Id.ToString(), Text = todo.Text });

            return reply;
        }

        public override async Task<GetTodosResponse> GetTodos(GetTodosRequest request, ServerCallContext context)
        {
            var todos = await dbContext.Todo.Select(x => new TodoItem() { Id = x.Id.ToString(), Text = x.Text }).ToListAsync();
            var response = new GetTodosResponse();
            response.Todos.AddRange(todos);


            return response;
        }
    }
}
