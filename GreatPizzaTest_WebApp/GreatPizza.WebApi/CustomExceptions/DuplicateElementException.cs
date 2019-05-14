using System;

namespace GreatPizza.WebApi.CustomExceptions
{
    public class DuplicateElementException : Exception
    {
        public DuplicateElementException()
        { }

        public DuplicateElementException(string name)
        : base(string.Format("Element already exists: {0}", name))
        { }
    }
}