using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace APITaxaJuros.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class TaxaJurosController : Controller
    {
        private const double TaxaJuros = 0.01;

        [HttpGet]
        public ActionResult RetornarTaxaJuros()
        {
            // Nesse caso foi utilizado um objeto anonimo pois ele já fazia o serviço da maneira mais simples, sem precisar criar uma classe
            // para armazenar apenas um valor e que ainda por cima é fixo
            var taxaJurosObj =  new
            {
                taxaJuros = TaxaJuros
            };

            var retorno = new JsonResult(taxaJurosObj)
            {
                StatusCode = (int)HttpStatusCode.OK
            };

            return retorno;
        }
    }
}
