using System;
using System.Collections.Generic;

namespace OptiCountExporter
{
    public class Plankton
    {
        protected string OptiCountTaxonomy { get; set; }
        protected string OptiCountSpecies { get; set; }
        protected string Phylum { get; set; }
        protected string Class { get; set; }
        protected string Order { get; set; }
        protected string Family { get; set; }
        protected string Genus { get; set; }
        protected string Species { get; set; }
        protected int DyntaxaID { get; set; }
        protected Double Concentration { get; set; }
        protected Double Biovolume { get; set; }
        protected Double Freshweight { get; set; }
        protected List<String> SpeciesFlags { get; set; }
        protected List<String> SpeciesComments { get; set; }

        public Plankton(string optiCountTaxonomy, string optiCountSpecies, Double concetration, Double biovolume, Double freshweight)
        {
            this.OptiCountTaxonomy = optiCountTaxonomy;
            this.OptiCountSpecies = optiCountSpecies;
            this.Concentration = concetration;
            this.Biovolume = biovolume;
            this.Freshweight = freshweight;
        }


        public void IdentifySpecies(List<String> flags, List<String> comments)
        {

        }

        public void IdentifySpeciesFlags(List<String> flags)
        {

        }

        public void IdentifyCommentsFlags(List<String> comments)
        {

        }

    }
}
