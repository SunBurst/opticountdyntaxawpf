using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiCountExporter
{
    public class ZooPlankton : Plankton
    {
        public ZooPlankton(string taxonSpecies, Double concetration, Double biovolume, Double freshweight)
            : base(taxonSpecies, concetration, biovolume, freshweight)
        {

        }
    }
}
