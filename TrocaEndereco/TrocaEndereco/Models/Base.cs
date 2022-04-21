using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrocaEndereco.Models
{
  
        public abstract class Base
        {
            public int Id { get; set; }

            internal List<string> _errors;

            //--Ao acessar os erros o programador só vai poder ler e não manipular os dados ex: _erros.Exemplo--
            public IReadOnlyCollection<string> Erros => _errors;
            public abstract bool Validate();
        }
    
}
