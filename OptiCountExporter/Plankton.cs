﻿using ArtDatabanken.Data;
using System;
using System.Collections.Generic;
using System.Linq;

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
        protected List<String> SpeciesIgnores { get; set; }

        public Plankton(string optiCountTaxonomy, string optiCountSpecies, Double concetration, Double biovolume, Double freshweight)
        {
            this.OptiCountTaxonomy = optiCountTaxonomy;
            this.OptiCountSpecies = optiCountSpecies;
            this.Concentration = concetration;
            this.Biovolume = biovolume;
            this.Freshweight = freshweight;
            this.SpeciesFlags = new List<String>();
            this.SpeciesComments = new List<String>();
            this.SpeciesIgnores = new List<String>();
        }

        public virtual void IdentifySpecies(List<String> flags, List<String> comments, List<String> ignores)
        {
            this.IdentifyIgnores(ignores);
            this.IdentifySpeciesFlags(flags);
            this.IdentifySpeciesComments(comments);
            this.IdentifySpeciesName();
        }

        public virtual void IdentifySpeciesName()
        {
            string[] allParts = this.OptiCountSpecies.Split();

            int numOfFlagsAndComments = this.SpeciesFlags.Count + this.SpeciesComments.Count;
            string[] nameParts = new string[allParts.Length - numOfFlagsAndComments];

            int index = 0;
            foreach (var part in allParts)
            {
                if (!(this.SpeciesFlags.Contains(part)) & !(this.SpeciesComments.Contains(part)))
                {
                    nameParts[index] = part;
                    index++;
                }
            }

            this.OptiCountSpecies = String.Join(" ", nameParts);
        }

        public void IdentifySpeciesFlags(List<String> flags)
        {
            string[] allParts = this.OptiCountSpecies.Split();
            foreach (var flag in flags)
            {
                if (allParts.Contains(flag))
                {
                    this.SpeciesFlags.Add(flag);
                }
            }
        }

        public void IdentifySpeciesComments(List<String> comments)
        {
            string[] allParts = this.OptiCountSpecies.Split();
            foreach (var comment in comments)
            {
                if (allParts.Contains(comment))
                {
                    this.SpeciesComments.Add(comment);
                }
            }
        }

        public void IdentifyIgnores(List<String> ignores)
        {
            string[] allParts = this.OptiCountSpecies.Split();
            foreach (var ignore in ignores)
            {
                if (allParts.Contains(ignore))
                {
                    this.SpeciesIgnores.Add(ignore);
                }
            }

            string[] newAllParts = new string[allParts.Length - this.SpeciesIgnores.Count];
            for (int i = 0, j = 0; i < allParts.Length; i++)
            {
                if (!(this.SpeciesIgnores.Contains(allParts[i])))
                {
                    newAllParts[j] = allParts[i];
                    j++;
                }
            }

            this.OptiCountSpecies = string.Join(" ", newAllParts);
        }

        public void DyntaxaMatch(DyntaxaService session)
        {
            ITaxonTreeNode result = session.searchTaxa(this.OptiCountSpecies);
            Console.WriteLine("Trying to match: " + this.OptiCountSpecies);
            if (result != null)
            {
                Console.WriteLine("Taxon: " + result.Taxon.ScientificName);
            }
            else
            {
                Console.WriteLine("No match! :(");
            }
        }

        public override string ToString()
        {
            return "Taxonomy: " + this.OptiCountTaxonomy + " Species: " + this.OptiCountSpecies + " Ignores: " + String.Join(" ", this.SpeciesIgnores) +  " Flags: " + String.Join(" ", this.SpeciesFlags) + " Comments: " + String.Join(" ", this.SpeciesComments);
        }

    }
}
