using BlazorSandboxProject.Web.Client.Factories;
using GrpcTodo;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Http.Connections.Client;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BlazorSandboxProject.Web.Client.Pages
{
    public partial class GrpcTodo : ComponentBase
    {
        [Inject]
        public Todo.TodoClient todoClient { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private HubConnection hubConnection;

        AddTodoRequest request = new AddTodoRequest();
        IList<TodoItem> todos;

        async Task AddTodo()
        {
            await todoClient.AddTodoAsync(request);

            request = new AddTodoRequest();
        }

        private async Task GetTodos()
        {
            var todoitems = await todoClient.GetTodosAsync(new GetTodosRequest());
            todos = todoitems.Todos.ToList();
        }

        protected override async Task OnInitializedAsync()
        {
            await GetTodos();
            await SetupSignalRConnection();
        }

        private async Task SetupSignalRConnection()
        {
            //https://github.com/dotnet/aspnetcore/issues/25259#issuecomment-683143179
            var builder = new HubConnectionBuilder();
            var httpConnectionOptions = HttpConnectionFactoryInternal.createHttpConnectionOptions(); // work around constructor call
            httpConnectionOptions.Url = NavigationManager.ToAbsoluteUri("/todohub");
            builder.Services.AddSingleton<EndPoint>(new UriEndPoint(httpConnectionOptions.Url));
            var opt = Microsoft.Extensions.Options.Options.Create(httpConnectionOptions);
            builder.Services.AddSingleton(opt);
            builder.Services.AddSingleton<IConnectionFactory, HttpConnectionFactoryInternal>();

            hubConnection = builder.Build();

            hubConnection.On<TodoItem>("ReceiveTodo", (item) =>
            {
                todos.Add(item);
                StateHasChanged();
            });

            await hubConnection.StartAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await hubConnection.DisposeAsync();
        }
    }
}
