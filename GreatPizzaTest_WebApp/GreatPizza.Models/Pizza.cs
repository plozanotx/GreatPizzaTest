using System.Collections.Generic;

namespace GreatPizza.Models
{
    public class Pizza
    {
        public string Name { get; set; }
        public IEnumerable<Topping> Toppings { get; set; }
    }
}
