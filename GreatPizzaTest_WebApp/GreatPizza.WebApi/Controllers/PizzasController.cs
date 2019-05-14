using GreatPizza.Models;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using GreatPizza.WebApi.CustomExceptions;

namespace GreatPizza.WebApi.Controllers
{
    [RoutePrefix("api/Pizzas")]
    public class PizzasController : ApiController
    {
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetPizzas()
        {
            try
            {
                Core.Core core = new Core.Core();
                var pizzas = core.GetPizzas();

                if (pizzas == null || !pizzas.Any())
                    throw new NullReferenceException("No Pizzas Found");

                return Json(pizzas);
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

        [HttpGet]
        [Route("{name}")]
        public IHttpActionResult GetPizza(string name)
        {
            try
            {
                Core.Core core = new Core.Core();
                var pizza = core.GetPizza(name);

                if (pizza == null)
                    throw new NullReferenceException("'" + name + "' Pizza Not Found");

                return Json(pizza);
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
        public IHttpActionResult AddPizza(string name)
        {
            try
            {
                Core.Core core = new Core.Core();
                var pizza = core.GetPizza(name);
                bool result;

                if (pizza == null)
                    result = core.AddPizza(new Pizza() { Name = name });
                else
                    throw new DuplicateElementException(name);

                return Content(HttpStatusCode.Created, new { Code = (int)HttpStatusCode.Created, Message = "Pizza successfully added" });
            }
            catch (DuplicateElementException dex)
            {
                return Content(HttpStatusCode.Conflict, new { Code = (int)HttpStatusCode.Conflict, Message = dex.Message });
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new { Code = (int)HttpStatusCode.InternalServerError, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("{pizzaName/toppingName}")]
        public IHttpActionResult AddToppingToPizza(string pizzaName, string toppingName)
        {
            try
            {
                Core.Core core = new Core.Core();
                var pizza = core.GetPizza(pizzaName);

                if (pizza == null)
                    throw new NullReferenceException("'" + pizzaName + "' Pizza Not Found");
                else
                {
                    var topping = core.GetTopping(toppingName);

                    if (topping == null)
                        throw new NullReferenceException("'" + toppingName + "' Topping Not Found");
                    else
                    {
                        bool result = core.AddTopping(topping, pizza);

                        if (!result)
                            throw new DuplicateElementException("'" + pizzaName + "' Pizza already contains '" + toppingName + "'");
                    }
                }

                return Content(HttpStatusCode.Created, new { Code = (int)HttpStatusCode.Created, Message = "Topping successfully added" });
            }
            catch (DuplicateElementException dex)
            {
                return Content(HttpStatusCode.Conflict, new { Code = (int)HttpStatusCode.Conflict, Message = dex.Message });
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new { Code = (int)HttpStatusCode.InternalServerError, Message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("{pizzaName/toppingName}")]
        public IHttpActionResult DeleteToppingFromPizza(string pizzaName, string toppingName)
        {
            try
            {
                Core.Core core = new Core.Core();
                var pizza = core.GetPizza(pizzaName);

                if (pizza == null)
                    throw new NullReferenceException("'" + pizzaName + "' Pizza Not Found");
                else
                {
                    var topping = core.GetTopping(toppingName);

                    if (topping == null)
                        throw new NullReferenceException("'" + toppingName + "' Topping Not Found");
                    else
                    {
                        bool result = core.DeleteToppingFromPizza(topping, pizza);

                        if (!result)
                            throw new Exception("'" + toppingName + "' Topping could not be deleted");
                    }
                }

                return Content(HttpStatusCode.OK, new { Code = (int)HttpStatusCode.Created, Message = "Topping successfully deleted" });
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new { Code = (int)HttpStatusCode.InternalServerError, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("{pizzaName}/Toppings")]
        public IHttpActionResult GetToppingsForPizza(string pizzaName)
        {
            try
            {
                Core.Core core = new Core.Core();
                var pizza = core.GetPizza(pizzaName);

                if (pizza == null)
                    throw new NullReferenceException("'" + pizzaName + "' Pizza Not Found");

                var toppings = core.GetToppings(pizza);

                return Json(toppings);
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
