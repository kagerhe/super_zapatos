using System.Web.Http;

using WebApiAuthenticate.Filters;

namespace WebApi.Controllers
{
    
    [RoutePrefix("services/autenticationbasic")]
    [BasicAuthentication]
    public class AutenticationBasicController : ApiController
    {
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult Get()
        {
            return Ok("Hola ");
        }
    }
}
