using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrocaEndereco.Models;

namespace TrocaEndereco.Validations
{
    public class MoviValidator : AbstractValidator<Movimentacao>
    {
        public MoviValidator()
        {
            //Validação para a entidade num geral 
            RuleFor(x => x)
                .NotEmpty()
                .WithMessage("A entidade não pode ser vazia.")

                .NotNull()
                .WithMessage("A Entidade não pode ser nula.");
        }
    }
}
