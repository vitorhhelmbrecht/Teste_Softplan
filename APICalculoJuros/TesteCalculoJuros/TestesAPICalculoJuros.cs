using NUnit.Framework;
using APICalculoJuros.Controllers;
using FluentAssertions;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using APICalculoJuros.Interfaces;
using Moq;
using Newtonsoft.Json;
using APICalculoJuros.Entidades;

namespace TesteCalculoJuros
{
    public class TesteAPICalculoJuros
    {
        private CalculaJurosController _calcularJuros;
        private ShowMeTheCode _caminhoCodigo;
        private CultureInfo _cultura = CultureInfo.CreateSpecificCulture("en-US");

        [SetUp]
        public void Setup()
        {
            var integracao = new Mock<IIntegracao>();
            integracao.Setup(r => r.BuscarTaxaJuros()).Returns(0.01);

            _calcularJuros = new CalculaJurosController(integracao.Object);
            _caminhoCodigo = new ShowMeTheCode();
        }

        [TestCase(100, 5, 105.10)]
        [TestCase(1000, 10, 1104.62)]
        [TestCase(500, 8, 541.42)]
        [TestCase(500, 11, 557.83)]
        [TestCase(105.4, 6, 111.88)]
        public void DeveriaCalcularJurosCompostos(double valorInicial, int tempo, double resultadoEsperado)
        {
            var retorno = _calcularJuros.CalcularJuros(valorInicial, tempo);
            retorno.Should().BeOfType<JsonResult>().
                Which.StatusCode.Should().Be((int)HttpStatusCode.OK);

            var retornoResult = (JsonResult) retorno;
            var objetoRetornoString = JsonConvert.SerializeObject(retornoResult.Value);

            var retornoCalculoJuros = JsonConvert.DeserializeObject<RetornoCalculoJuros>(objetoRetornoString);
            double resultado = retornoCalculoJuros.Resultado;

            Assert.AreEqual(resultadoEsperado, resultado);
        }

        [TestCase(-100, 5)]
        [TestCase(100, -5)]
        [TestCase(-100, -5)]
        public void DeveriaRetornarErro(double valorInicial, int tempo)
        {
            var retorno = _calcularJuros.CalcularJuros(valorInicial, tempo);

            retorno.Should().BeOfType<BadRequestObjectResult>().
                Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Test]
        public void DeveriaRetornarCaminhoCodigo()
        {
            var valorEsperado = "https://github.com/vitorhhelmbrecht/Teste_Softplan";

            var retorno = _caminhoCodigo.ShowThecode();
            retorno.Should().BeOfType<JsonResult>().
                Which.StatusCode.Should().Be((int)HttpStatusCode.OK);

            var retornoResult = (JsonResult)retorno;
            var objetoRetornoString = JsonConvert.SerializeObject(retornoResult.Value);

            var retornoCaminho = JsonConvert.DeserializeObject<RetornoCaminho>(objetoRetornoString);
            var caminho = retornoCaminho.CaminhoRepositorio;

            Assert.AreEqual(valorEsperado, caminho);
        }
    }
}