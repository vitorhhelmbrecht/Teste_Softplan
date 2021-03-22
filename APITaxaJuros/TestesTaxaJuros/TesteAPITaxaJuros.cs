using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Net;
using Newtonsoft.Json;

namespace TestesTaxaJuros
{
    class TesteAPITaxaJuros
    {
        private APITaxaJuros.Controllers.TaxaJurosController _controlerTaxaJuros;

        [SetUp]
        public void Setup()
        {
            _controlerTaxaJuros = new APITaxaJuros.Controllers.TaxaJurosController();
        }

        [Test]
        public void TestarRetornoTaxaJuros()
        {
            double retornoEsperado = 0.01;

            var retorno = _controlerTaxaJuros.RetornarTaxaJuros();

            retorno.Should().BeOfType<JsonResult>().
                Which.StatusCode.Should().Be((int)HttpStatusCode.OK);

            var retornoResult = (JsonResult)retorno;
            var objetoRetornoString = JsonConvert.SerializeObject(retornoResult.Value);

            var retornoTaxaJuros = JsonConvert.DeserializeObject<RetornoTaxaJuros>(objetoRetornoString);
            var valorTaxaJuros = retornoTaxaJuros.TaxaJuros;

            Assert.AreEqual(retornoEsperado, valorTaxaJuros);
        }
    }
}
