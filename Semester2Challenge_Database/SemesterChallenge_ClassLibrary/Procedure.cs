using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemesterChallenge_ClassLibrary
{
    public class Procedure
    {
        private int procedureID;
        private string description;
        private double price;

        public int ProcedureID
        {
            get { return procedureID; }
            set { procedureID = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public double Price
        {
            get { return price; }
            set { price = value; }
        }
    }
}
