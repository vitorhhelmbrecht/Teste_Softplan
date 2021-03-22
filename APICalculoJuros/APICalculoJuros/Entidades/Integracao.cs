using APICalculoJuros.Interfaces;
using System;
using System.Net.Http;
using Newtonsoft.Json;

namespace APICalculoJuros.Entidades
{
    public class Integracao : IIntegracao
    {
        private HttpClient _cliente;
        private string _uriApiTaxaJuros = Config.Buscar().UriApiTaxaJuros;

        public Integracao(HttpClient client)
        {
            _cliente = client;
        }

        public double BuscarTaxaJuros()
        {
            try
            {
                var retorno = _cliente.GetAsync(_uriApiTaxaJuros).Result;
                string retornoString = retorno.Content.ReadAsStringAsync().Result;

                RetornoTaxaJuros retornoTaxaJuros = JsonConvert.DeserializeObject<RetornoTaxaJuros>(retornoString);

                return retornoTaxaJuros.TaxaJuros;
            }
            catch (Exception e)
            {
                throw new Exception("Ocorreu um erro durante a busca da taxa de juros: " + e.Message);
            }
        }
    }
}
