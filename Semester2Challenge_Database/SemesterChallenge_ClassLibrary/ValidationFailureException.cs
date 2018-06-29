using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemesterChallenge_ClassLibrary
{
    public class ValidationFailureException: Exception
    {
        public ValidationFailureException(string message) : base("ValidationFailureException: " + message)
        {

        }
    }
}
