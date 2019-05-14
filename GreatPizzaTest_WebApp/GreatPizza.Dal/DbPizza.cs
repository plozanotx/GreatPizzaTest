using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GreatPizza.Dal
{
    [Serializable]
    public class DbPizza
    {
        [XmlArray]
        public List<Pizza> Pizzas { get; set; }
    }
}
