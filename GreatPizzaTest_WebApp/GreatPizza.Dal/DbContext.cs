using System;
using System.Collections.Generic;
using System.Linq;

namespace GreatPizza.Dal
{
    public class DbContext : IDisposable
    {
        List<Pizza> _pizzas;
        List<Topping> _toppings;

        public List<Pizza> Pizzas
        {
            get
            {
                return _pizzas;
            }
            set
            {
                _pizzas = value;
            }
        }

        public List<Topping> Toppings
        {
            get
            {
                return _toppings;
            }
            set
            {
                _toppings = value;
            }
        }

        public DbContext()
        {
            Helper.InitDbFiles();

            _pizzas = Helper.Deserialize<DbPizza>(Constants.DB_PIZZA_PATH).Pizzas;
            _toppings = Helper.Deserialize<DbTopping>(Constants.DB_TOPPING_PATH).Toppings;
        }

        public bool Save()
        {
            try
            {
                int pizzaMaxId = Pizzas.Max(u => u.Id);
                int toppingMaxId = Toppings.Max(u => u.Id);
                int idIncrementer = 1;

                foreach (var np in Pizzas.Where(u => u.Id == 0))
                {
                    np.Id = pizzaMaxId + idIncrementer;
                    idIncrementer++;
                }

                idIncrementer = 0;

                foreach (var nt in Toppings.Where(u => u.Id == 0))
                {
                    nt.Id = toppingMaxId + idIncrementer;
                    idIncrementer++;
                }

                Helper.Serialize(Constants.DB_PIZZA_PATH, new DbPizza() { Pizzas = Pizzas });
                Helper.Serialize(Constants.DB_TOPPING_PATH, new DbTopping() { Toppings = Toppings });

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}
