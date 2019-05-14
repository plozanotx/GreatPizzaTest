using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GreatPizza.Dal
{
    [Serializable]
    public class DbTopping
    {
        [XmlArray]
        public List<Topping> Toppings { get; set; }
    }
}
