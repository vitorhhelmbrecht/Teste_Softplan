using APICalculoJuros.Entidades;
using NUnit.Framework;
using RichardSzalay.MockHttp;
using System.Net;

namespace TesteCalculoJuros
{
    class TesteEntidades
    {
        private Integracao _integracao;

        [SetUp]
        public  void Setup()
        {
            // Utilizando a biblioteca RichardSzalay.MockHttp para fazer o mock de uma requisição. Recomendo muito, facilita demais o trabalho.
            var mockHttp = new MockHttpMessageHandler();

            var uriApiTaxaJuros = Config.Buscar().UriApiTaxaJuros;

            mockHttp.When(uriApiTaxaJuros).Respond(HttpStatusCode.OK, "application/json", "{ \"taxaJuros\" : \"0.01\" }");

            var mockClient = mockHttp.ToHttpClient();
            
            _integracao = new Integracao(mockClient);
        }

        [Test]
        public void TestarIntegracao()
        {
            double resultadoEsperado = 0.01;

            double taxaJuros = _integracao.BuscarTaxaJuros();

            Assert.AreEqual(resultadoEsperado, taxaJuros);
        }
    }
}
