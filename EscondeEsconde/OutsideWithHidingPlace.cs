using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscondeEsconde
{
    class OutsideWithHidingPlace : Outside, IHidingPlace
    {
        public OutsideWithHidingPlace(string name, bool hot) : base(name, hot)
        {
        }

        public string LocalDescription => throw new NotImplementedException();
    }
}
