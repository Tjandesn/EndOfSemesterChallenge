using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemesterChallenge_ClassLibrary
{
    public class Treatment
    {
        private string petName;
        private int ownerID;
        private int procedureID;
        private DateTime date;
        private string notes;
        private double price;

        public string PetName
        {
            get { return petName; }
            set { petName = value; }
        }

        public int OwnerID
        {
            get { return ownerID; }
            set { ownerID = value; }
        }

        public int ProcedureID
        {
            get { return procedureID; }
            set { procedureID = value; }
        }

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }

        public double Price
        {
            get { return price; }
            set { price = value; }
        }
    }
}
