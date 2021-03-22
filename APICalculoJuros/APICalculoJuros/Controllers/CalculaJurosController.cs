using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using APICalculoJuros.Interfaces;
using Newtonsoft.Json;

namespace APICalculoJuros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculaJurosController : ControllerBase
    {
        private readonly IIntegracao _integracao;

        public CalculaJurosController(IIntegracao integracao)
        {
            _integracao = integracao;
        }

        // GET: api/<CalculaJurosController>
        [HttpGet]
        public IActionResult CalcularJuros(double valorInicial, int meses)
        {
            if (valorInicial <= 0 || meses <= 0)
                return new BadRequestObjectResult("Os parâmetros 'valor inicial' e 'meses' devem possuir um valor maior que 0!");

            try
            {
                var taxaJuros = _integracao.BuscarTaxaJuros();

                // Calcular os valor final com a fórmula ValorInicial * (1 + taxajuros) ^ meses
                var valorJuros = valorInicial * Math.Pow(1 + taxaJuros, meses);
                valorJuros = Math.Round(valorJuros, 2, MidpointRounding.ToZero);

                var resultadoObj = new
                {
                    Resultado = valorJuros
                };

                var retorno = new JsonResult(resultadoObj)
                {
                    StatusCode = (int)HttpStatusCode.OK
                };

                return retorno;
            }
            catch (Exception e)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), e.Message);
            }
        }
    }
}
