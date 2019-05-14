using DAL = GreatPizza.Dal;
using GreatPizza.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GreatPizza.Core
{
    public class Core
    {
        public IEnumerable<Pizza> GetPizzas()
        {
            IEnumerable<DAL.Pizza> pizzas;
            List<Pizza> results = new List<Pizza>();

            using (DAL.DbContext db = new DAL.DbContext())
            {
                pizzas = db.Pizzas;

                foreach (var pizza in pizzas)
                {
                    results.Add(new Pizza()
                    {
                        Name = pizza.Name,
                        Toppings = db.Toppings.Where(u => u.PizzaId == pizza.Id).Select(u => new Topping() { Name = u.Name }).ToList()
                    });
                }
            }

            return results;
        }

        public Pizza GetPizza(string Name)
        {
            Pizza result = null;

            using (DAL.DbContext db = new DAL.DbContext())
            {
                var pizza = db.Pizzas.FirstOrDefault(u => u.Name.Equals(Name, StringComparison.OrdinalIgnoreCase));

                if (pizza != null)
                {
                    result = new Pizza() { Name = pizza.Name };
                    result.Toppings = db.Toppings.Where(u => u.PizzaId == pizza.Id).Select(u => new Topping() { Name = u.Name }).ToList();
                }
            }

            return result;
        }

        public IEnumerable<Topping> GetToppings()
        {
            IEnumerable<Topping> results;

            using (DAL.DbContext db = new DAL.DbContext())
            {
                results = db.Toppings.Select(u => new Topping() { Name = u.Name }).ToList();
            }

            return results;
        }

        public Topping GetTopping(string name)
        {
            Topping result = null;

            using (DAL.DbContext db = new DAL.DbContext())
            {
                var topping = db.Toppings.FirstOrDefault(u => u.Name == name);

                if (topping != null)
                    result = new Topping() { Name = topping.Name };
            }

            return result;
        }

        public bool AddTopping(Topping topping)
        {
            bool result = false;

            using (DAL.DbContext db = new DAL.DbContext())
            {
                db.Toppings.Add(new DAL.Topping() { Name = topping.Name });
                result = db.Save();
            }

            return result;
        }

        public bool DeleteTopping(Topping topping)
        {
            bool result = false;

            using (DAL.DbContext db = new DAL.DbContext())
            {
                var dbTopping = db.Toppings.FirstOrDefault(u => u.Name.Equals(topping.Name));

                db.Toppings.Remove(dbTopping);
                result = db.Save();
            }

            return result;
        }

        public bool DeletePizza(Pizza pizza)
        {
            bool result = false;

            using (DAL.DbContext db = new DAL.DbContext())
            {
                var dbPizza = db.Pizzas.FirstOrDefault(u => u.Name.Equals(pizza.Name));

                db.Pizzas.Remove(dbPizza);
                result = db.Save();
            }

            return result;
        }

        public bool DeleteToppingFromPizza(Topping topping, Pizza pizza)
        {
            bool result = false;

            using (DAL.DbContext db = new DAL.DbContext())
            {
                var dbPizza = db.Pizzas.FirstOrDefault(u => u.Name.Equals(pizza.Name));

                var dbTopping = db.Toppings.FirstOrDefault(u => u.PizzaId == dbPizza.Id && u.Name == topping.Name);

                if (dbTopping != null)
                {
                    dbTopping.PizzaId = null;
                    result = db.Save();
                }
            }

            return result;
        }

        public bool AddPizza(Pizza pizza)
        {
            bool result = false;

            using (DAL.DbContext db = new DAL.DbContext())
            {
                var newPizza = new DAL.Pizza() { Name = pizza.Name };
                db.Pizzas.Add(newPizza);
                result = db.Save();
            }

            return result;
        }

        public bool AddTopping(Topping topping, Pizza pizza)
        {
            bool result = false;

            using (DAL.DbContext db = new DAL.DbContext())
            {
                var dbPizza = db.Pizzas.FirstOrDefault(u => u.Name == pizza.Name);

                if (dbPizza != null)
                {
                    if (db.Toppings.Any(u => u.PizzaId == dbPizza.Id && u.Name == topping.Name))
                        result = false;
                    else
                    {
                        var dbTopping = db.Toppings.FirstOrDefault(u => u.Name == topping.Name);

                        if (dbTopping == null)
                            db.Toppings.Add(new DAL.Topping() { Name = topping.Name, PizzaId = dbPizza.Id });
                        else
                            dbTopping.PizzaId = dbPizza.Id;

                        result = db.Save();
                    }
                }
            }

            return result;
        }

        public IEnumerable<Topping> GetToppings(Pizza pizza)
        {
            IEnumerable<Topping> results = null;

            using (DAL.DbContext db = new DAL.DbContext())
            {
                var dbPizza = db.Pizzas.FirstOrDefault(u => u.Name == pizza.Name);

                if (dbPizza != null)
                {
                    results = db.Toppings.Where(u => u.PizzaId == dbPizza.Id).Select(u => new Topping() { Name = u.Name }).ToList();
                }
            }

            return results;
        }
    }
}
