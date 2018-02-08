using ArtDatabanken;
using ArtDatabanken.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OptiCountExporter
{
    public class Plankton
    {
        protected string TaxonPhylum { get; set; }
        protected string TaxonClass { get; set; }
        protected string TaxonOrder { get; set; }
        protected string TaxonFamily { get; set; }
        protected string TaxonGenus { get; set; }
        protected string taxonSpecies;
        protected int TaxonDyntaxaID { get; set; }
        protected Double TaxonConcentration { get; set; }
        protected Double TaxonBiovolume { get; set; }
        protected Double TaxonFreshweight { get; set; }
        protected List<String> TaxonSpeciesFlags { get; set; }
        protected List<String> TaxonSpeciesComments { get; set; }
        protected List<String> TaxonSpeciesIgnores { get; set; }

        public string TaxonSpecies
        {
            get
            {
                return this.taxonSpecies;
            }
            set
            {
                if (this.taxonSpecies != value)
                {
                    this.taxonSpecies = value;
                }
            }
        }

        public Plankton(string taxonSpecies, Double concetration, Double biovolume, Double freshweight)
        {
            this.taxonSpecies = taxonSpecies;
            this.TaxonConcentration = concetration;
            this.TaxonBiovolume = biovolume;
            this.TaxonFreshweight = freshweight;
            this.TaxonSpeciesFlags = new List<String>();
            this.TaxonSpeciesComments = new List<String>();
            this.TaxonSpeciesIgnores = new List<String>();
        }

        public virtual void CleanTaxonSpecies(List<String> flags, List<String> comments, List<String> ignores)
        {
            this.IdentifyIgnores(ignores);
            this.IdentifySpeciesFlags(flags);
            this.IdentifySpeciesComments(comments);
            this.ExtractTaxonSpecies();
        }

        public virtual void ExtractTaxonSpecies()
        {
            string[] allParts = this.TaxonSpecies.Split();

            int numOfFlagsAndComments = this.TaxonSpeciesFlags.Count + this.TaxonSpeciesComments.Count;
            string[] nameParts = new string[allParts.Length - numOfFlagsAndComments];

            int index = 0;
            foreach (var part in allParts)
            {
                if (!(this.TaxonSpeciesFlags.Contains(part)) & !(this.TaxonSpeciesComments.Contains(part)))
                {
                    nameParts[index] = part;
                    index++;
                }
            }

            this.TaxonSpecies = String.Join(" ", nameParts);
        }

        public void IdentifySpeciesFlags(List<String> flags)
        {
            string[] allParts = this.TaxonSpecies.Split();
            foreach (var flag in flags)
            {
                if (allParts.Contains(flag))
                {
                    this.TaxonSpeciesFlags.Add(flag);
                }
            }
        }

        public void IdentifySpeciesComments(List<String> comments)
        {
            string[] allParts = this.TaxonSpecies.Split();
            foreach (var comment in comments)
            {
                if (allParts.Contains(comment))
                {
                    this.TaxonSpeciesComments.Add(comment);
                }
            }
        }

        public void IdentifyIgnores(List<String> ignores)
        {
            string[] allParts = this.TaxonSpecies.Split();
            foreach (var ignore in ignores)
            {
                if (allParts.Contains(ignore))
                {
                    this.TaxonSpeciesIgnores.Add(ignore);
                }
            }

            string[] newAllParts = new string[allParts.Length - this.TaxonSpeciesIgnores.Count];
            for (int i = 0, j = 0; i < allParts.Length; i++)
            {
                if (!(this.TaxonSpeciesIgnores.Contains(allParts[i])))
                {
                    newAllParts[j] = allParts[i];
                    j++;
                }
            }

            this.TaxonSpecies = string.Join(" ", newAllParts);
        }

        public override string ToString()
        {
            return $"Phylum: {this.TaxonPhylum} Class: {this.TaxonClass} Order: {this.TaxonOrder} " +
                $"Family: {this.TaxonFamily} Genus: {this.TaxonGenus} Species: {this.TaxonSpecies} " +
                $"Dyntaxa ID: {this.TaxonDyntaxaID} Concetration: {this.TaxonConcentration} Biovolume: {this.TaxonBiovolume} " +
                $"Freshweight: {this.TaxonFreshweight} Ignores: {String.Join(" ", this.TaxonSpeciesIgnores)} " +
                $"Flags: {String.Join(" ", this.TaxonSpeciesFlags)} Comments: {String.Join(" ", this.TaxonSpeciesComments)}";
        }

    }
}
