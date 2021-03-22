using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICalculoJuros.Entidades
{
    public class Config
    {
        public string UriApiTaxaJuros { get; set; }
        public string UriRepositorio { get; set; }


        private static Config _config;
        private static IConfiguration _configuration;

        private Config(IConfiguration configuration)
        {
            _configuration = configuration;
            UriApiTaxaJuros = configuration.GetValue<string>("UriApiTaxaJuros");
            UriRepositorio = configuration.GetValue<string>("UriRepositorio");
        }

        public static Config Buscar()
        {
            if (_config == null)
            {
                IConfiguration configuracao = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false).Build();

                _config = new Config(configuracao);
            }

            return _config;
        }
    }
}
