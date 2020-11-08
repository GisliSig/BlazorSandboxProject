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
        string result;

        async Task AddTodo()
        {
            var reply = await todoClient.AddTodoAsync(request);

            result = reply.Text;

            request = new AddTodoRequest();
        }
    }
}
