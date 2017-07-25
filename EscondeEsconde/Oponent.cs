using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscondeEsconde
{
    class Oponent
    {
        private Location myLocation;
        private Random randon;

        public Oponent(Location startingLocation)
        {
            this.myLocation = startingLocation;
            randon = new Random();
            //for (int i = 0; i < 10; i++)
            //{

            //}
        }

        public void Move()
        {
            if (myLocation is IHasExteriorDoor)
            {
                IHasExteriorDoor LocationWithDoor = myLocation as IHasExteriorDoor;
                if (randon.Next(2) == 1)
                    myLocation = LocationWithDoor.DoorLocation;
            }

            bool hidden = false;
            while (hidden)
            {
                int rand = randon.Next(0, myLocation.Exits.Length);
                myLocation = myLocation.Exits[rand];
                if (myLocation is IHidingPlace)
                    hidden = true;
            }
        }

        public bool Check(Location locationToCheck)
        {
            if (locationToCheck != myLocation)
                return false;
            else
                return true;
        }
    }
}
