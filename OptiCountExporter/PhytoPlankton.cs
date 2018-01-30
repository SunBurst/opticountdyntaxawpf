using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiCountExporter
{
    public class PhytoPlankton : Plankton
    {
        protected int MinSize { get; set; }
        protected int MaxSize { get; set; }

        public PhytoPlankton(string optiCountTaxonomy, string optiCountSpecies, Double concetration, Double biovolume, Double freshweight)
            : base(optiCountTaxonomy, optiCountSpecies, concetration, biovolume, freshweight)
        {

        }

    }
}
