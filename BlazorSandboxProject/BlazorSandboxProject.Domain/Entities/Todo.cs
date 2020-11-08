using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSandboxProject.Domain.Entities
{
    public class Todo
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
    }
}
