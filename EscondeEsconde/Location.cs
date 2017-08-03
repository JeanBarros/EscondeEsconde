using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscondeEsconde
{
    public abstract class Location
    {
        public Location(string name)
        {
            this.name = name;
        }

        public Location[] Exits;
        private string name;
        public string Name { get { return name; } }
        public virtual string Description
        {
            get
            {
                string description = "";

                if(name.EndsWith("a") || name.EndsWith("r"))
                    description = "Você está na " + name + ". Você pode ir para os seguintes lugares: ";
                else
                    description = "Você está no " + name + ". Você pode ir para os seguintes lugares: ";

                for (int i = 0; i < Exits.Length; i++)
                {
                    description += "" + Exits[i].Name;
                    if (i != Exits.Length - 1)
                        description += ", ";
                }
                description += ".";

                return description;
            }
        }
    }
}
