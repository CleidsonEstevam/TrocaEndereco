

using System;

namespace TrocaEndereco.Common
{

    public class ReturnJson<T> where T : class
    {
        #region "Propriedades"

        /// <summary>
        /// Houve sucesso na operação ?
        /// </summary>
        public bool Sucesso { get; set; }

        /// <summary>
        /// Retorna um id para a tela se necessario.
        /// </summary>
        public int ReturnId { get; set; }

        /// <summary>
        /// Retorna uma url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Retorna uma string
        /// </summary>
        public string String { get; set; }

        /// <summary>
        /// Mensagem que irá ser retornada
        /// </summary>
        public string Mensagem { get; set; }

        /// <summary>
        /// Tipo da mensagem que será mostrada (Erro/Sucesso/Informação/Alerta)
        /// </summary>
        public string TipoMensagem { get; set; }

        /// <summary>
        /// Caso exista um erro joga-ló aqui.
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// Usada para retorna uma lista para o Grid do Datatables
        /// </summary>
        public static SourceDataTablesFormat SourceDataTablesFormat { get; set; }

        /// <summary>
        /// Caso queria retornar algum objeto para a tela.
        /// </summary>
        public T Objeto { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string StringSerializada { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FileExtension { get; set; }

        public string FileType { get; set; }

        #endregion

        #region "Construtores"

        /// <summary>
        /// 
        /// </summary>
        public ReturnJson()
        {
            SourceDataTablesFormat = new SourceDataTablesFormat();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sucesso"></param>
        /// <param name="obj"></param>
        /// <param name="tipoMensagem"></param>
        /// <param name="msg"></param>
        public ReturnJson(bool sucesso, T obj, string tipoMensagem, string msg)
        {
            SourceDataTablesFormat = new SourceDataTablesFormat();
            Sucesso = sucesso;
            TipoMensagem = tipoMensagem;
            Mensagem = msg;
            Objeto = obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sucesso"></param>
        /// <param name="stringSerializada"></param>
        /// <param name="tipoMensagem"></param>
        /// <param name="msg"></param>
        public ReturnJson(bool sucesso, string stringSerializada, string tipoMensagem, string msg)
        {
            SourceDataTablesFormat = new SourceDataTablesFormat();
            Sucesso = sucesso;
            TipoMensagem = tipoMensagem;
            Mensagem = msg;
            StringSerializada = stringSerializada;
        }

        #endregion
    }

    public class ReturnJson
    {
        #region "Propriedades"

        /// <summary>
        /// Houve sucesso na operação ?
        /// </summary>
        public bool Sucesso { get; set; }

        /// <summary>
        /// Retorna um id para a tela se necessario.
        /// </summary>
        public decimal ReturnId { get; set; }

        /// <summary>
        /// Retorna uma url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Retorna uma string
        /// </summary>
        public string String { get; set; }

        /// <summary>
        /// Mensagem que irá ser retornada
        /// </summary>
        public string Mensagem { get; set; }

        /// <summary>
        /// Tipo da mensagem que será mostrada (Erro/Sucesso/Informação/Alerta)
        /// </summary>
        public string TipoMensagem { get; set; }

        /// <summary>
        /// Caso exista um erro joga-ló aqui.
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// Usada para retorna uma lista para o Grid do Datatables
        /// </summary>
        public SourceDataTablesFormat SourceDataTablesFormat { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string StringSerializada { get; set; }

        #endregion

        #region "Construtores"

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sucesso"></param>
        public ReturnJson(bool sucesso)
        {
            SourceDataTablesFormat = new SourceDataTablesFormat();
            Sucesso = sucesso;
        }

        /// <summary>
        /// 
        /// </summary>
        public ReturnJson()
        {
            SourceDataTablesFormat = new SourceDataTablesFormat();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sucesso"></param>
        /// <param name="tipoMensagem"></param>
        /// <param name="msg"></param>
        public ReturnJson(bool sucesso, string tipoMensagem, string msg)
        {
            SourceDataTablesFormat = new SourceDataTablesFormat();
            Sucesso = sucesso;
            TipoMensagem = tipoMensagem;
            Mensagem = msg;
        }

        #endregion
    }
}