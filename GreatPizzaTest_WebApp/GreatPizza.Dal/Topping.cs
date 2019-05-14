namespace GreatPizza.Dal
{
    public class Topping
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? PizzaId { get; set; }
    }
}
