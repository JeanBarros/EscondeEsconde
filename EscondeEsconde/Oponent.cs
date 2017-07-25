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
        public int MyLocation { get; set; }
        private Random randon;
        public Random Randon { get; set; }

        public Oponent(Location initialLocation)
        {
            this.myLocation = initialLocation;

            randon = new Random();

            for (int i = 0; i < 10; i++)
            {

            }
        }
    }
}
