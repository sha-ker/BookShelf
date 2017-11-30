using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buecherregal
{
    public class cBuch
    {
        int höhe;
        int zone;

        public cBuch(int _höhe)
        {
            höhe = _höhe;
            zone = -1;
        }

        public int Höhe
        {
            get
            {
                return höhe;
            }

            set
            {
                höhe = value;
            }
        }

        public int Zone
        {
            get
            {
                return zone;
            }

            set
            {
                zone = value;
            }
        }
    }
}
