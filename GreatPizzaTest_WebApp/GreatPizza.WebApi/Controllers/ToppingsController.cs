using GreatPizza.Models;
using GreatPizza.WebApi.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GreatPizza.WebApi.Controllers
{
    [RoutePrefix("api/Toppings")]
    public class ToppingsController : ApiController
    {
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetToppings()
        {
            try
            {
                Core.Core core = new Core.Core();
                var toppings = core.GetToppings();

                if (toppings == null || !toppings.Any())
                    throw new NullReferenceException("No Toppings Found");

                return Json(toppings.Distinct());
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

        [HttpPost]
        [Route("{name}")]
        public IHttpActionResult AddTopping(string name)
        {
            try
            {
                Core.Core core = new Core.Core();
                var topping = core.GetTopping(name);
                bool result;

                if (topping == null)
                    result = core.AddTopping(new Topping() { Name = name });
                else
                    throw new DuplicateElementException(name);
                
                if (result)
                    return Content(HttpStatusCode.OK, new { Code = (int)HttpStatusCode.OK, Message = "Topping successfully deleted" });
                else
                    return Content(HttpStatusCode.Conflict, new { Code = (int)HttpStatusCode.Conflict, Message = "Topping could not be added" });
            }
            catch (DuplicateElementException dex)
            {
                return Content(HttpStatusCode.Conflict, new { Code = (int)HttpStatusCode.Conflict, Message = dex.Message });
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

        [HttpDelete]
        [Route("{name}")]
        public IHttpActionResult DeleteTopping(string name)
        {
            try
            {
                Core.Core core = new Core.Core();
                var topping = core.GetTopping(name);

                if (topping == null)
                    throw new NullReferenceException("Topping Not Found");

                bool result = core.DeleteTopping(topping);
                if (result)
                    return Content(HttpStatusCode.OK, new { Code = (int)HttpStatusCode.OK, Message = "Topping successfully deleted" });
                else
                    return Content(HttpStatusCode.Conflict, new { Code = (int)HttpStatusCode.Conflict, Message = "Topping could not be deleted" });
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
