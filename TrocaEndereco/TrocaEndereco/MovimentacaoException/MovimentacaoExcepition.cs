using System;
using System.Collections.Generic;

namespace TrocaEndereco.MovimentacaoException
{
    public class MovimentacaoExcepition : Exception
    {
        internal List<string> _errors;
        public List<string> Erros => _errors;
        public MovimentacaoExcepition()
        { }

        public MovimentacaoExcepition(string message, List<string> errors) : base(message)
        {
            _errors = errors;
        }

        public MovimentacaoExcepition(string message) : base(message)
        { }

        public MovimentacaoExcepition(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
