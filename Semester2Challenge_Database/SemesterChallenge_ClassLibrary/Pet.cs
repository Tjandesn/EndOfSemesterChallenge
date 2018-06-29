using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemesterChallenge_ClassLibrary
{
    public class Pet
    {
        private string petName;
        private string type;
        private string ownerID;

        public string PetName
        {
            get { return petName; }
            set { petName = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public string OwnerID
        {
            get { return ownerID; }
            set { ownerID = value; }
        }
    }
}
