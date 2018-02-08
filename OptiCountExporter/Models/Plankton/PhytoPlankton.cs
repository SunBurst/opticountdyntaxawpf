using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiCountExporter
{
    public class PhytoPlankton : Plankton
    {
        protected int TaxonMinSize { get; set; }
        protected int TaxonMaxSize { get; set; }
        protected List<String> SpeciesMinAndMax;

        public PhytoPlankton(string taxonSpecies, Double concetration, Double biovolume, Double freshweight)
            : base(taxonSpecies, concetration, biovolume, freshweight)
        {
            this.SpeciesMinAndMax = new List<String>();
        }

        public override void CleanTaxonSpecies(List<String> flags, List<String> comments, List<String> ignores)
        {
            this.IdentifyIgnores(ignores);
            this.IdentifySpeciesFlags(flags);
            this.IdentifySpeciesComments(comments);
            this.IdentifySpeciesOperators();
            this.ExtractTaxonSpecies();
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
                                this.TaxonMaxSize = number;
                                this.SpeciesMinAndMax.Add(part);
                                this.SpeciesMinAndMax.Add(numberString);
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
                                    this.TaxonMinSize = number;
                                    this.SpeciesMinAndMax.Add(numberString);
                                    this.SpeciesMinAndMax.Add(part);
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
                                this.TaxonMaxSize = number;
                                this.SpeciesMinAndMax.Add(part);
                            }
                            
                        }
                        else if (part.EndsWith("<"))
                        {
                            string numberString = part.Substring(0, (part.Length - 2));
                            int number;
                            bool success = Int32.TryParse(numberString, out number);
                            if (success)
                            {
                                this.TaxonMinSize = number;
                                this.SpeciesMinAndMax.Add(part);
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
                                this.TaxonMinSize = number;
                                this.SpeciesMinAndMax.Add(part);
                                this.SpeciesMinAndMax.Add(numberString);
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
                                    this.TaxonMaxSize = number;
                                    this.SpeciesMinAndMax.Add(numberString);
                                    this.SpeciesMinAndMax.Add(part);
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
                                this.TaxonMinSize = number;
                                this.SpeciesMinAndMax.Add(part);
                            }

                        }
                        else if (part.EndsWith(">"))
                        {
                            string numberString = part.Substring(0, (part.Length - 2));
                            int number;
                            bool success = Int32.TryParse(numberString, out number);
                            if (success)
                            {
                                this.TaxonMaxSize = number;
                                this.SpeciesMinAndMax.Add(part);
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
                                this.TaxonMaxSize = secondNumber;
                                this.SpeciesMinAndMax.Add(part);
                                this.SpeciesMinAndMax.Add(secondNumberString);
                            }

                            try
                            {
                                firstNumberString = allParts[pos - 1];
                                success = Int32.TryParse(firstNumberString, out firstNumber);
                                if (success)
                                {
                                    this.TaxonMinSize = firstNumber;
                                    this.SpeciesMinAndMax.Add(firstNumberString);
                                    this.SpeciesMinAndMax.Add(part);
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
                            this.TaxonMinSize = firstNumber;
                        }

                        success = Int32.TryParse(secondNumberString, out secondNumber);
                        if (success)
                        {
                            this.TaxonMaxSize = secondNumber;
                        }
                        this.SpeciesMinAndMax.Add(part);
                    }
                }

                int partNum;
                if (Int32.TryParse(part, out partNum) & (!(allParts.Contains("<"))) & (!(allParts.Contains(">"))) & (!(allParts.Contains("-"))))
                {
                    // Number but no operator found in parts
                    this.TaxonMinSize = partNum;
                    this.TaxonMaxSize = partNum;
                    this.SpeciesMinAndMax.Add(part);
                }
            }
        }

        public override void ExtractTaxonSpecies()
        {
            string[] allParts = this.TaxonSpecies.Split();

            int numOfFlagsCommentsAndMinMax = this.TaxonSpeciesFlags.Count + this.TaxonSpeciesComments.Count + this.SpeciesMinAndMax.Count;
            string[] nameParts = new string[allParts.Length - numOfFlagsCommentsAndMinMax];

            int index = 0;
            foreach (var part in allParts)
            {
                if (!(this.TaxonSpeciesFlags.Contains(part)) & !(this.TaxonSpeciesComments.Contains(part)) &!(this.SpeciesMinAndMax.Contains(part)))
                {
                    nameParts[index] = part;
                    index++;
                }
            }

            this.TaxonSpecies = String.Join(" ", nameParts);
        }

        public override string ToString()
        {
            return $"{ base.ToString()} Min: {this.TaxonMinSize} Max: {this.TaxonMaxSize}";
        }

    }
}
