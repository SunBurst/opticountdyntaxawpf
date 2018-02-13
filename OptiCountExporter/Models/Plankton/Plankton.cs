using ArtDatabanken;
using ArtDatabanken.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OptiCountExporter
{
    public class Plankton
    {
        protected string taxonPhylum;
        protected string taxonClass;
        protected string taxonOrder;
        protected string taxonFamily;
        protected string taxonGenus;
        protected string taxonOrganismGroup;
        protected string taxonSpecies;
        protected int taxonDyntaxaID;
        protected bool taxonDyntaxaIDIsInitialized;
        protected int taxonMinSize;
        protected int taxonMaxSize;
        protected Double taxonConcentration;
        protected Double taxonBiovolume;
        protected Double taxonFreshweight;
        protected List<String> taxonSpeciesFlags;
        protected List<String> taxonSpeciesComments;
        protected List<String> taxonSpeciesIgnores;
        protected List<String> taxonSpeciesMinAndMax;

        public string TaxonPhylum
        {
            get
            {
                return this.taxonPhylum;
            }
            set
            {
                if (this.taxonPhylum != value)
                {
                    this.taxonPhylum = value;
                }
            }
        }

        public string TaxonClass
        {
            get
            {
                return this.taxonClass;
            }
            set
            {
                if (this.taxonClass != value)
                {
                    this.taxonClass = value;
                }
            }
        }

        public string TaxonOrder
        {
            get
            {
                return this.taxonOrder;
            }
            set
            {
                if (this.taxonOrder != value)
                {
                    this.taxonOrder = value;
                }
            }
        }

        public string TaxonFamily
        {
            get
            {
                return this.taxonFamily;
            }
            set
            {
                if (this.taxonFamily != value)
                {
                    this.taxonFamily = value;
                }
            }
        }

        public string TaxonGenus
        {
            get
            {
                return this.taxonGenus;
            }
            set
            {
                if (this.taxonGenus != value)
                {
                    this.taxonGenus = value;
                }
            }
        }

        public string TaxonOrganismGroup
        {
            get
            {
                return this.taxonOrganismGroup;
            }
            set
            {
                if (this.taxonOrganismGroup != value)
                {
                    this.taxonOrganismGroup = value;
                }
            }
        }

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

        public int TaxonDyntaxaID
        {
            get
            {
                return this.taxonDyntaxaID;
            }
            set
            {
                this.taxonDyntaxaID = value;
                this.taxonDyntaxaIDIsInitialized = true;
            }
        }

        public bool TaxonDyntaxaIDIsInitialized
        {
            get
            {
                return this.taxonDyntaxaIDIsInitialized;
            }
        }

        public List<String> TaxonSpeciesFlags
        {
            get
            {
                return this.taxonSpeciesFlags;
            }
            set
            {
                this.taxonSpeciesFlags = value; 
            }
        }

        public List<String> TaxonSpeciesComments
        {
            get
            {
                return this.taxonSpeciesComments;
            }
            set
            {
                this.taxonSpeciesComments = value;
            }
        }

        public List<String> TaxonSpeciesIgnores
        {
            get
            {
                return this.taxonSpeciesIgnores;
            }
            set
            {
                this.taxonSpeciesIgnores = value;
            }
        }

        public List<String> TaxonSpeciesMinAndMax
        {
            get
            {
                return this.taxonSpeciesMinAndMax;
            }
            set
            {
                this.taxonSpeciesMinAndMax = value;
            }
        }

        public int TaxonMinSize
        {
            get
            {
                return this.taxonMinSize;
            }
            set
            {
                if (this.taxonMinSize != value)
                {
                    this.taxonMinSize = value;
                }
            }
        }

        public int TaxonMaxSize
        {
            get
            {
                return this.taxonMaxSize;
            }
            set
            {
                if (this.taxonMaxSize != value)
                {
                    this.taxonMaxSize = value;
                }
            }
        }

        public Double TaxonConcentration
        {
            get
            {
                return this.taxonConcentration;
            }
            set
            {
                if (this.taxonConcentration != value)
                {
                    this.taxonConcentration = value;
                }
            }
        }

        public Double TaxonBiovolume
        {
            get
            {
                return this.taxonBiovolume;
            }
            set
            {
                if (this.taxonBiovolume != value)
                {
                    this.taxonBiovolume = value;
                }
            }
        }

        public Double TaxonFreshweight
        {
            get
            {
                return this.taxonFreshweight;
            }
            set
            {
                if (this.taxonFreshweight != value)
                {
                    this.taxonFreshweight = value;
                }
            }
        }

        public Plankton() {}

        public Plankton(string taxonSpecies) {
            this.taxonSpecies = taxonSpecies;
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
            this.TaxonSpeciesMinAndMax = new List<String>();
        }

        public Plankton(string taxonPhylum, string taxonClass, string taxonOrder, string taxonFamily, 
            string taxonGenus, string taxonOrganismGroup, string taxonSpecies, int taxonMinSize, int taxonMaxSize, 
                int taxonDyntaxaID, bool taxonDyntaxaIDIsInitialized, Double taxonConcentration, Double taxonBiovolume, 
                    Double taxonFreshweight, List<string> taxonSpeciesFlags, List<string> taxonSpeciesComments, List<string> TaxonSpeciesIgnores, List<string> taxonSpeciesMinAndMax)
        {
            TaxonPhylum = taxonPhylum;
            TaxonClass = taxonClass;
            TaxonOrder = taxonOrder;
            TaxonFamily = taxonFamily;
            TaxonGenus = taxonGenus;
            TaxonOrganismGroup = taxonOrganismGroup;
            TaxonConcentration = taxonConcentration;
            TaxonBiovolume = taxonBiovolume;
            TaxonFreshweight = taxonFreshweight;
            TaxonSpeciesFlags = taxonSpeciesFlags;
            TaxonSpeciesComments = taxonSpeciesComments;
            TaxonSpeciesIgnores = taxonSpeciesComments;
            TaxonSpeciesMinAndMax = taxonSpeciesMinAndMax;
        }

        public virtual void CleanTaxonSpecies(List<String> flags, List<String> comments, List<String> ignores)
        {
            this.IdentifyIgnores(ignores);
            this.IdentifySpeciesFlags(flags);
            this.IdentifySpeciesComments(comments);
            this.IdentifySpeciesOperators();
            this.ExtractTaxonSpecies();
        }

        public string GetTaxonSpeciesFlagsAsString()
        {
            return String.Join(" ", this.TaxonSpeciesFlags);
        }

        public string GetTaxonSpeciesCommentsAsString()
        {
            return String.Join(" ", this.TaxonSpeciesComments);
        }

        public string GetTaxonSpeciesIgnoresAsString()
        {
            return String.Join(" ", this.TaxonSpeciesIgnores);
        }

        public void ConvertBiovolume(Double factor)
        {
            this.TaxonBiovolume = this.TaxonBiovolume * factor;
        }

        public void ExtractTaxonSpecies()
        {
            string[] allParts = this.TaxonSpecies.Split();

            int numOfFlagsCommentsAndMinMax = this.TaxonSpeciesFlags.Count + this.TaxonSpeciesComments.Count + this.TaxonSpeciesMinAndMax.Count;
            string[] nameParts = new string[allParts.Length - numOfFlagsCommentsAndMinMax];

            int index = 0;
            foreach (var part in allParts)
            {
                if (!(this.TaxonSpeciesFlags.Contains(part)) & !(this.TaxonSpeciesComments.Contains(part)) & !(this.TaxonSpeciesMinAndMax.Contains(part)))
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

        public void IdentifySpeciesOperators()
        {
            string[] allParts = this.TaxonSpecies.Split();
            foreach (var part in allParts)
            {
                if (part.Contains('<'))
                {
                    if (part.Length == 1)
                    {
                        int pos = Array.IndexOf(allParts, "<");
                        int number;
                        string numberString;
                        bool success;
                        try
                        {
                            numberString = allParts[pos + 1];
                            success = Int32.TryParse(numberString, out number);
                            if (success)
                            {
                                this.taxonMaxSize = number;
                                this.TaxonSpeciesMinAndMax.Add(part);
                                this.TaxonSpeciesMinAndMax.Add(numberString);
                            }
                        }
                        catch (IndexOutOfRangeException)
                        {
                            try
                            {
                                numberString = allParts[pos - 1];
                                success = Int32.TryParse(numberString, out number);
                                if (success)
                                {
                                    this.taxonMinSize = number;
                                    this.TaxonSpeciesMinAndMax.Add(numberString);
                                    this.TaxonSpeciesMinAndMax.Add(part);
                                }
                            }
                            catch (IndexOutOfRangeException)
                            {
                                throw new Exception("Found < but no number!");
                            }
                        }

                    }
                    else
                    {
                        if (part.StartsWith("<"))
                        {
                            string numberString = part.Substring(1, (part.Length - 1));
                            int number;
                            bool success = Int32.TryParse(numberString, out number);
                            if (success)
                            {
                                this.taxonMaxSize = number;
                                this.TaxonSpeciesMinAndMax.Add(part);
                            }

                        }
                        else if (part.EndsWith("<"))
                        {
                            string numberString = part.Substring(0, (part.Length - 2));
                            int number;
                            bool success = Int32.TryParse(numberString, out number);
                            if (success)
                            {
                                this.taxonMinSize = number;
                                this.TaxonSpeciesMinAndMax.Add(part);
                            }
                        }
                        else
                        {
                            throw new Exception("Unsupported operator");
                        }
                    }
                }
                if (part.Contains('>'))
                {
                    if (part.Length == 1)
                    {
                        int pos = Array.IndexOf(allParts, ">");
                        int number;
                        string numberString;
                        bool success;
                        try
                        {
                            numberString = allParts[pos + 1];
                            success = Int32.TryParse(numberString, out number);
                            if (success)
                            {
                                this.taxonMinSize = number;
                                this.TaxonSpeciesMinAndMax.Add(part);
                                this.TaxonSpeciesMinAndMax.Add(numberString);
                            }
                        }
                        catch (IndexOutOfRangeException)
                        {
                            try
                            {
                                numberString = allParts[pos - 1];
                                success = Int32.TryParse(numberString, out number);
                                if (success)
                                {
                                    this.taxonMaxSize = number;
                                    this.TaxonSpeciesMinAndMax.Add(numberString);
                                    this.TaxonSpeciesMinAndMax.Add(part);
                                }
                            }
                            catch (IndexOutOfRangeException)
                            {
                                throw new Exception("Found > but no number!");
                            }
                        }
                    }
                    else
                    {
                        if (part.StartsWith(">"))
                        {
                            string numberString = part.Substring(1, (part.Length - 1));
                            int number;
                            bool success = Int32.TryParse(numberString, out number);
                            if (success)
                            {
                                this.taxonMinSize = number;
                                this.TaxonSpeciesMinAndMax.Add(part);
                            }

                        }
                        else if (part.EndsWith(">"))
                        {
                            string numberString = part.Substring(0, (part.Length - 2));
                            int number;
                            bool success = Int32.TryParse(numberString, out number);
                            if (success)
                            {
                                this.taxonMaxSize = number;
                                this.TaxonSpeciesMinAndMax.Add(part);
                            }
                        }
                        else
                        {
                            throw new Exception("Unsupported operator");
                        }
                    }
                }
                if (part.Contains('-'))
                {
                    if (part.Length == 1)
                    {
                        int pos = Array.IndexOf(allParts, "-");
                        int firstNumber, secondNumber;
                        string firstNumberString, secondNumberString;
                        bool success;
                        try
                        {
                            secondNumberString = allParts[pos + 1];
                            success = Int32.TryParse(secondNumberString, out secondNumber);
                            if (success)
                            {
                                this.taxonMaxSize = secondNumber;
                                this.TaxonSpeciesMinAndMax.Add(part);
                                this.TaxonSpeciesMinAndMax.Add(secondNumberString);
                            }

                            try
                            {
                                firstNumberString = allParts[pos - 1];
                                success = Int32.TryParse(firstNumberString, out firstNumber);
                                if (success)
                                {
                                    this.taxonMinSize = firstNumber;
                                    this.TaxonSpeciesMinAndMax.Add(firstNumberString);
                                    this.TaxonSpeciesMinAndMax.Add(part);
                                }
                            }
                            catch (IndexOutOfRangeException)
                            {
                                throw new Exception("Found - but no lower limit!");
                            }
                        }
                        catch (IndexOutOfRangeException)
                        {
                            throw new Exception("Found - but no upper limit!");
                        }
                    }
                    else
                    {
                        int dashAtPos = part.IndexOf("-");
                        string firstNumberString = part.Substring(0, dashAtPos);
                        string secondNumberString = part.Substring((dashAtPos + 1), ((part.Length - 1) - dashAtPos));
                        int firstNumber, secondNumber;
                        bool success;

                        success = Int32.TryParse(firstNumberString, out firstNumber);
                        if (success)
                        {
                            this.taxonMinSize = firstNumber;
                        }

                        success = Int32.TryParse(secondNumberString, out secondNumber);
                        if (success)
                        {
                            this.taxonMaxSize = secondNumber;
                        }
                        this.TaxonSpeciesMinAndMax.Add(part);
                    }
                }

                int partNum;
                if (Int32.TryParse(part, out partNum) & (!(allParts.Contains("<"))) & (!(allParts.Contains(">"))) & (!(allParts.Contains("-"))))
                {
                    // Number but no operator found in parts
                    this.taxonMinSize = partNum;
                    this.taxonMaxSize = partNum;
                    this.TaxonSpeciesMinAndMax.Add(part);
                }
            }
        }

        public override string ToString()
        {
            return $"Phylum: {this.TaxonPhylum} Class: {this.TaxonClass} Order: {this.TaxonOrder} " +
                $"Family: {this.TaxonFamily} Genus: {this.TaxonGenus} Organism Group: {this.TaxonOrganismGroup} Species: {this.TaxonSpecies} " +
                $"Dyntaxa ID: {this.TaxonDyntaxaID} Concetration: {this.TaxonConcentration} Biovolume: {this.TaxonBiovolume} " +
                $"Freshweight: {this.TaxonFreshweight} Ignores: {String.Join(" ", this.TaxonSpeciesIgnores)} " +
                $"Flags: {String.Join(" ", this.TaxonSpeciesFlags)} Comments: {String.Join(" ", this.TaxonSpeciesComments)}";
        }

    }
}
