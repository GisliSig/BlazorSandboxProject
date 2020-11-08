using FluentValidation;
using GrpcTodo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSandboxProject.Web.Shared.Validators
{
    public class AddTodoValidator : AbstractValidator<AddTodoRequest>
    {
        public AddTodoValidator()
        {
            RuleFor(x => x.Text).NotEmpty().WithMessage("Verður að skrá texta");
        }
    }
}
