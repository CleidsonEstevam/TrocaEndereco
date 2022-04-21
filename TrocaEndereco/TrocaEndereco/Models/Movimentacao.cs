using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrocaEndereco.MovimentacaoException;
using TrocaEndereco.Validations;

namespace TrocaEndereco.Models
{
    public class Movimentacao : Base
    {
        public string Produto { get; set; }
        public string EnderecoOrigem { get; set; }
        public string EnderecoDestino { get; set; }
        public string Quantidade { get; set; }
        public string MsgRetorno { get; set; }
        public string Data { get; set; }
        public string Hora { get; set; }


        public Movimentacao() { }

        public Movimentacao(string produto, string enderecoOrigem, string enderecoDestino, string quantidade, string msgRetorno, string data, string hora)
        {
            Produto = produto;
            EnderecoOrigem = enderecoOrigem;
            EnderecoDestino = enderecoDestino;
            Quantidade = quantidade;
            MsgRetorno = msgRetorno;
            Data = data;
            Hora = hora;

            Validate();
        }

        public void ValidaNome(string produto) 
        {
            this.Produto = produto;
            Validate();
        }

        public void ValidaEnderecoOrigem(string enderecoOrigem)
        {
            this.EnderecoOrigem = enderecoOrigem;
            Validate();
        }

        public void ValidEnderecoDestino(string enderecoDestino)
        {
            this.EnderecoDestino = enderecoDestino;
            Validate();
        }

        public void ValidaQuantidade(string quantidade)
        {
            this.Quantidade = quantidade;
            Validate();
        }

        public override bool Validate()
        {
            var validator = new MoviValidator();
            var validation = validator.Validate(this);

            if (!validation.IsValid)
            {
                foreach (var error in validation.Errors)
                {
                    _errors.Add(error.ErrorMessage);

                    throw new MovimentacaoExcepition("Alguns campos inválidos", _errors);
                }
            }
            //Se a entidade tiver ok ele retorna true, se não retorna a exeção
            return true;
        }
    }
}
