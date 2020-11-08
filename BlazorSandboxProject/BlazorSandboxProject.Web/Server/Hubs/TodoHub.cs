using GrpcTodo;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSandboxProject.Web.Server.Hubs
{
    public class TodoHub : Hub
    {
        public async Task SendTodo(TodoItem todo)
        {
            await Clients.All.SendAsync("ReceiveTodo", todo);
        }
    }
}
