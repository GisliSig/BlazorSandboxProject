using GrpcTodo;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSandboxProject.Web.Client.Pages
{
    public partial class GrpcTodo : ComponentBase
    {
        [Inject]
        public Todo.TodoClient todoClient { get; set; }

        AddTodoRequest request = new AddTodoRequest();
        IList<TodoItem> todos;
        string result;

        async Task AddTodo()
        {
            var reply = await todoClient.AddTodoAsync(request);

            result = reply.Text;

            request = new AddTodoRequest();

            await GetTodos();
        }

        private async Task GetTodos()
        {
            var todoitems = await todoClient.GetTodosAsync(new GetTodosRequest());
            todos = todoitems.Todos.ToList();
        }

        protected override async Task OnInitializedAsync()
        {
            await GetTodos();
        }
    }
}
