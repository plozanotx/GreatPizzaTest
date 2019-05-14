using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace GreatPizza.Dal
{
    internal static class Helper
    {
        internal static void Serialize<T>(string filePath, T data)
        {
            using (TextWriter tw = new StreamWriter(filePath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(tw, data);
            }
        }

        internal static T Deserialize<T>(string filePath)
        {
            T result;

            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                result = (T)serializer.Deserialize(fs);
            }

            return result;
        }

        internal static void InitDbFiles()
        {
            ValidateDbFolder();

            if (!File.Exists(Constants.DB_PIZZA_PATH))
            {
                List<Pizza> defaultPizzas = new List<Pizza>();
                defaultPizzas.Add(new Pizza() { Id = 1, Name = "Hawaiian" });
                defaultPizzas.Add(new Pizza() { Id = 2, Name = "Peperoni" });
                defaultPizzas.Add(new Pizza() { Id = 3, Name = "Irish" });

                Serialize(Constants.DB_PIZZA_PATH, new DbPizza() { Pizzas = defaultPizzas });
            }

            if (!File.Exists(Constants.DB_TOPPING_PATH))
            {
                List<Topping> defaultToppings = new List<Topping>();
                defaultToppings.Add(new Topping() { Id = 1, Name = "Ham", PizzaId = 1 });
                defaultToppings.Add(new Topping() { Id = 2, Name = "Pineapple", PizzaId = 1 });

                defaultToppings.Add(new Topping() { Id = 3, Name = "Peperoni", PizzaId = 2 });

                defaultToppings.Add(new Topping() { Id = 4, Name = "Potatoes", PizzaId = 3 });
                defaultToppings.Add(new Topping() { Id = 5, Name = "Cabbage", PizzaId = 3 });



                Serialize(Constants.DB_TOPPING_PATH, new DbTopping() { Toppings = defaultToppings });
            }
        }

        internal static void ValidateDbFolder()
        {
            if (Directory.Exists(Constants.DB_FOLDER_PATH))
                return; 

            Directory.CreateDirectory(Constants.DB_FOLDER_PATH);
        }

    }
}
