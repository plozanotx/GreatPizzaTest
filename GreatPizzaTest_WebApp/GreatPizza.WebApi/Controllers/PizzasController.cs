using GreatPizza.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GreatPizza.Core;

namespace GreatPizza.WebApi.Controllers
{

    public class PizzasController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetPizzas()
        {
            try
            {
                Core.Core core = new Core.Core();
                var pizzas = core.GetPizzas();

                if (pizzas == null || !pizzas.Any())
                    throw new NullReferenceException("No Pizzas Found");

                return Ok(pizzas);
            }
            catch (NullReferenceException nex)
            {
                return Content(HttpStatusCode.NotFound, new { Code = (int)HttpStatusCode.NotFound, Message = nex.Message });
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new { Code = (int)HttpStatusCode.InternalServerError, Message = ex.Message });
            }
        }
    }
}
