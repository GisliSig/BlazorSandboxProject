using BlazorSandboxProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSandboxProject.Web.Server.EFContext
{
    public class TodoDBContext : DbContext
    {
        public TodoDBContext(DbContextOptions<TodoDBContext> options) : base(options) { }

        public DbSet<Todo> Todo { get; set; }
    }
}
