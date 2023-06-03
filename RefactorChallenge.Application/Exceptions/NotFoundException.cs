using System;
using System.Collections.Generic;
using System.Text;

namespace RefactorChallenge.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            : base($"{name} ({key}) is not found")
        {
        }
    }
}
