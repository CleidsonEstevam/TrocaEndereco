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

        #region "Modelo"

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
        #endregion
        
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
                        for (int row = 3; row < rowcount; row++) 
                        {
                            if (worksheet.Cells[row, 1].Value == null) 
                            {
                                continue;
                            }
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

        [HttpGet]
        public JsonResult Listar()
        {
            var session = ISessionsExtensions.Get<Movimentacao>(HttpContext.Session, "list");
            List<Movimentacao> listarLinhas = new List<Movimentacao>();
            foreach (var item in session)
            {
                var enderecoOrigem = item.EnderecoOrigem;
                var produto = item.Produto;
                var quantidade = item.Quantidade;
                var enderecoDestino = item.EnderecoDestino;
                var msgRetorno = item.MsgRetorno;

                if (string.IsNullOrEmpty(msgRetorno))
                {
                    msgRetorno = "IMPORTADO COM SUCESSO!";
                }

                listarLinhas.Add(new Movimentacao
                {
                    EnderecoOrigem = Convert.ToString(enderecoOrigem).PadLeft(8, '0').ToUpper(),
                    Produto = Convert.ToString(produto).PadLeft(6, '0'),
                    Quantidade = Convert.ToString(quantidade),
                    EnderecoDestino = Convert.ToString(enderecoDestino).PadLeft(8, '0').ToUpper(),
                    MsgRetorno = msgRetorno
                });
            }
            return Json(listarLinhas);
        }

        #region "Valida Dados Planilha e da Insert"
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

            using var db = new MovimentacaoContext();
            var consultarProduto = db.Produto
                .Where(p => p.Codigo == movi.Produto)
                .AsNoTracking()
                .ToList();
            if (consultarProduto.Count <= 0) 
            {
               return movi.MsgRetorno = "Erro! Código do produto incorreto!";
            }

            var consultarEnderecoO = db.Endereco
              .Where(p => p.Local == movi.EnderecoOrigem)
              .AsNoTracking()
              .ToList();
            if (consultarEnderecoO.Count <= 0)
            {
               return movi.MsgRetorno = "Erro! Endereco de origem não existe!";
            }

            var consultarEnderecoD = db.Endereco
             .Where(p => p.Local == movi.EnderecoDestino)
              .AsNoTracking()
              .ToList();
            if (consultarEnderecoD.Count <= 0)
            {
                return movi.MsgRetorno = "Erro! Endereco de destino não existe!";
            }

            var consultarSaldo = db.Endereco
            .Where(p => p.Produto == movi.Produto && p.Saldo == movi.Quantidade)
             .AsNoTracking()
             .ToList();
            if (consultarEnderecoD.Count <= 0)
            {
                return movi.MsgRetorno = "Erro! Saldo incorreto!";
            }

            db.Add(movimentacao);
            var registros = db.SaveChanges();

            return movi.MsgRetorno;
        }
        #endregion

        #region "Métodos de session"
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
        public async Task<Movimentacao[]> GetSession()
        {

            await HttpContext.Session.LoadAsync();
            var result = ISessionsExtensions.Get<Movimentacao[]>(HttpContext.Session, "list");

            return result;
        }
        #endregion
    }
        
}



