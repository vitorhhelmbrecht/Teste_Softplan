using APICalculoJuros.Entidades;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace APICalculoJuros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowMeTheCode : ControllerBase
    {
        

        [HttpGet]
        public IActionResult ShowThecode()
        {
            string CaminhoRepositorioGit = Config.Buscar().UriRepositorio;

            var caminhoObj = new
            {
                CaminhoRepositorio = CaminhoRepositorioGit
            };

            var retorno = new JsonResult(caminhoObj)
            {
                StatusCode = (int)HttpStatusCode.OK
            };

            return retorno;
        }
    }
}
