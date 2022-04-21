using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using TrocaEndereco.Models;
using System.Threading.Tasks;
using TrocaEndereco.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TrocaEndereco.Common;
using System.Text;
using TrocaEndereco.Sessions;

namespace TrocaEndereco.Controllers
{
    public class MovimentacaoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public FileResult ModeloExcel()
        {
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets.Add("ALTERAÇÕES DE ENDEREÇOS");

                worksheet.Cells["A1"].Value = "MOVIMENTAÇÕES";
                worksheet.Cells["A1:D1"].Merge = true;
                worksheet.Cells["A1:D1"].Style.Font.Bold = true;
                worksheet.Cells["A1:D1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A1:D1"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["A1:D1"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["A1:D1"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["A1:D1"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["A1:D1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["A1:D1"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                worksheet.Cells["A2"].Value = "ENDEREÇO ORIGEM";
                worksheet.Cells["A2"].Style.Font.Bold = true;
                worksheet.Cells["A2"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A2"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["A2"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["A2"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["A2"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["A2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["A2"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                worksheet.Cells["B2"].Value = "PRODUTO";
                worksheet.Cells["B2"].Style.Font.Bold = true;
                worksheet.Cells["B2"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["B2"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["B2"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["B2"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["B2"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["B2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["B2"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                worksheet.Cells["C2"].Value = "QUANTIDADE";
                worksheet.Cells["C2"].Style.Font.Bold = true;
                worksheet.Cells["C2"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["C2"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["C2"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["C2"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["C2"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["C2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["C2"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                worksheet.Cells["D2"].Value = "ENDEREÇO DESTINO";
                worksheet.Cells["D2"].Style.Font.Bold = true;
                worksheet.Cells["D2"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["D2"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["D2"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["D2"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["D2"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["D2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["D2"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);


                worksheet.Protection.IsProtected = false;
                worksheet.Protection.AllowSelectLockedCells = false;
                worksheet.View.ShowGridLines = true;
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                package.Save();
            }
            var xlsResult = stream.ToArray();

            return File(xlsResult, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", string.Format("MODELO_PARA_MOVIMENTAÇÃO_{0}_{1}.xlsx", DateTime.Now.ToString("ddMMyyyy"), DateTime.Now.ToString("hhmmss")));
        }

        [HttpPost]
        public async Task<List<Movimentacao>> ImportarExcel(IFormFile file)
        {
            List<Movimentacao> listarLinhas = new List<Movimentacao>();
            if (file != null)
            {
                using (var stream = new MemoryStream()) 
                {
                    await file.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream)) 
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        var rowcount = worksheet.Dimension.Rows;
                        for (int row = 3; row <= rowcount; row++) 
                        {
                            var enderecoOrigem = worksheet.Cells[row, 1].Value;
                            var produto = worksheet.Cells[row, 2].Value;
                            var quantidade = worksheet.Cells[row, 3].Value;
                            var enderecoDestino = worksheet.Cells[row, 4].Value;

                            listarLinhas.Add(new Movimentacao
                            {
                                Id = row,
                                EnderecoOrigem = Convert.ToString(enderecoOrigem).PadLeft(8, '0').ToUpper(),
                                Produto = Convert.ToString(produto).PadLeft(6, '0'),
                                Quantidade = Convert.ToString(quantidade).PadLeft(6, '0'),
                                EnderecoDestino = Convert.ToString(enderecoDestino).PadLeft(8, '0').ToUpper(),
                                MsgRetorno = "",
                                Hora = DateTime.Now.ToShortTimeString(),
                                Data = DateTime.Now.ToString("dd/mm/yyyy")
                            });
                        }
                    }
                }           
            }
            foreach (var item in listarLinhas)
            {
                var validaEndereco = InserirDados(item);
                if (!(string.IsNullOrEmpty(validaEndereco.ToString())))
                {
                    item.MsgRetorno = validaEndereco.ToString();
                    continue;
                }
            }
             var ssesion = await SetSession(listarLinhas);
            
            return listarLinhas;
        }

        public async Task<Movimentacao> SetSession(List<Movimentacao> sessionValue)
        {
            try
            {
                await HttpContext.Session.LoadAsync();
                ISessionsExtensions.Set(HttpContext.Session, "list", sessionValue);
            }
            catch (Exception ex)
            {

                throw;
            }

            return null;
        }


        private string InserirDados(Movimentacao movi)
        {
            var movimentacao = new Movimentacao
            {
                EnderecoOrigem = movi.EnderecoOrigem,
                EnderecoDestino = movi.EnderecoDestino,
                Produto = movi.Produto,
                Quantidade = movi.Quantidade,
                Hora = movi.Hora,
                Data = movi.Data
            };

            #region "Valida Dados Planilha"

            using var db = new MovimentacaoContext();
            var consultarProduto = db.Produto
                .Where(p => p.Codigo == movi.Produto)
                .AsNoTracking()
                .ToList();

            if (consultarProduto == null) 
            {
               return movi.MsgRetorno = "Código do produto incorreto!";
            }

            var consultarEnderecoO = db.Endereco
              .Where(p => p.Local == movi.EnderecoOrigem);

            if (consultarProduto == null)
            {
               return movi.MsgRetorno = "Endereco de origem não existe!";
            }

            var consultarEnderecoD = db.Endereco
             .Where(p => p.Local == movi.EnderecoDestino);

            if (consultarProduto == null)
            {
                return movi.MsgRetorno = "Endereco de destino não existe!";
            }

            #endregion

            db.Add(movimentacao);
            var registros = db.SaveChanges();

            return movi.MsgRetorno;

        }

        [HttpGet]
        public JsonResult Listar()
        {

            var session = ISessionsExtensions.Get<Movimentacao>(HttpContext.Session, "list");

            List<Movimentacao> listarLinhas = new List<Movimentacao>();
        
            var dataTablesSource = new SourceDataTablesFormat();
            foreach (var item in session)
            {
                var enderecoOrigem = item.EnderecoOrigem;
                var produto = item.Produto;
                var quantidade = item.Quantidade;
                var enderecoDestino = item.EnderecoDestino;

                listarLinhas.Add(new Movimentacao
                {
                    EnderecoOrigem = Convert.ToString(enderecoOrigem).PadLeft(8, '0').ToUpper(),
                    Produto = Convert.ToString(produto).PadLeft(6, '0'),
                    Quantidade = Convert.ToString(quantidade).PadLeft(6, '0'),
                    EnderecoDestino = Convert.ToString(enderecoDestino).PadLeft(8, '0').ToUpper(),
                });
            }


            //foreach (var item in session)
            //{
            //    IList<string> dataRow = new List<Movimentacao>();
            //    var html = new StringBuilder();
            //    dataRow.Add(item.EnderecoOrigem);
            //    dataRow.Add(item.EnderecoDestino);
            //    dataRow.Add(item.Quantidade);
            //    dataRow.Add(item.Data);
            //    dataRow.Add(item.Hora);
            //    dataRow.Add(Convert.ToString(item.Hora));

            //    if (!(item.MsgRetorno == "" || item.MsgRetorno == null))
            //    {
            //        dataRow.Add("PROCESSO CANCELADO!");
            //        html.AppendFormat("<a class=\"btn-info btn-sm\" title=\"Editar Movimentação\"style=\"cursor:pointer\" onclick=\"Editar({0})\" id=\"btnEditar\" ><i class=\"fa fa-edit\"></i></a>&nbsp;", item.Id);
            //        html.AppendFormat("<a class=\"btn-danger btn-sm\" title=\"Excluir Movimentação\"style=\"cursor:pointer\" onclick=\"Excluir(this)({0})\" id=\"btnEditar\" ><i class=\"fa fa-remove\"></i></a>&nbsp;", item.Id);
            //    }
            //    else
            //    {
            //        dataRow.Add("IMPORTADO COM SUCESSO!");
            //    }
            //    dataRow.Add(html.ToString());
            //    dataTablesSource.aaData.Add(dataRow);
            //}
            //var retorno = new ReturnJson
            //{
            //    Sucesso = true,
            //    TipoMensagem = "Sucesso",
            //    Mensagem = "msgOperacaoConcluida",
            //    SourceDataTablesFormat = dataTablesSource
            //};
            return Json(listarLinhas);
        }

        public async Task<Movimentacao[]> GetSession()
        {

            await HttpContext.Session.LoadAsync();
            var result = ISessionsExtensions.Get<Movimentacao[]>(HttpContext.Session, "list");

            return result;
        }

    }
        
}



