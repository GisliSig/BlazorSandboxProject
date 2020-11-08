using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorSandboxProject.Web.Server.EFContext;
using Grpc.Core;
using GrpcTodo;
using Microsoft.EntityFrameworkCore;

namespace BlazorSandboxProject.Web.Server.Services
{
    public class TodoService : Todo.TodoBase
    {
        private readonly TodoDBContext dbContext;

        public TodoService(TodoDBContext context)
        {
            this.dbContext = context;
        }
        public override async Task<AddTodoReply> AddTodo(AddTodoRequest request, ServerCallContext context)
        {
            var reply = new AddTodoReply() { Text = request.Text + " added" };

            var todo = new Domain.Entities.Todo();
            todo.Text = request.Text;

            dbContext.Todo.Add(todo);
            await dbContext.SaveChangesAsync();

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
