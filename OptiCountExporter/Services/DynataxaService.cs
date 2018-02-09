using ArtDatabanken;
using ArtDatabanken.Data;
using ArtDatabanken.WebService.Client.AnalysisService;
using ArtDatabanken.WebService.Client.ReferenceService;
using ArtDatabanken.WebService.Client.TaxonAttributeService;
using ArtDatabanken.WebService.Client.TaxonService;
using ArtDatabanken.WebService.Client.UserService;
using System;
using System.Collections.Generic;

namespace OptiCountExporter
{
    public class DyntaxaService
    {
        private IUserContext userContext;

        public DyntaxaService(string userName, string password, string appId)
        {
            // Configure data sources in the Onion architecture.
            // User service must be first because the other services depends on UserService.
            UserDataSource.SetDataSource();
            TaxonDataSource.SetDataSource();
            TaxonAttributeDataSource.SetDataSource();
            ReferenceDataSource.SetDataSource();
            AnalysisDataSource.SetDataSource();

            setUserContext(userName, password, appId);
        }

        public void setUserContext(string userName, string password, string appId)
        {
            userContext = CoreData.UserManager.Login(userName, password, appId);
        }

        public IUserContext getUserContext()
        {
            return userContext;
        }

        public TaxonNameList searchTaxa(string searchString)
        {
            ITaxonNameSearchCriteria searchCriteria;
            TaxonNameList taxonNames;

            searchCriteria = new TaxonNameSearchCriteria();

            // Begränsa sökningen till vetenskapliga namn.
            searchCriteria.Category = CoreData.TaxonManager.GetTaxonNameCategory(getUserContext(), (Int32)(TaxonNameCategoryId.ScientificName));

            // Begränsa sökningen till giltiga namn och taxa.
            searchCriteria.IsValidTaxon = true;
            searchCriteria.IsValidTaxonName = true;

            // Ange söksträng.
            searchCriteria.NameSearchString = new StringSearchCriteria();
            searchCriteria.NameSearchString.SearchString = searchString;

            // Tala om hur söksträngen ska matchas mot de vetenskapliga namnen.
            // Om mer än en operator anges så provas de en i taget tills minst en träff fås.
            searchCriteria.NameSearchString.CompareOperators = new List<StringCompareOperator>();
            searchCriteria.NameSearchString.CompareOperators.Add(StringCompareOperator.Equal);
            taxonNames = CoreData.TaxonManager.GetTaxonNames(getUserContext(), searchCriteria);
            //if (taxonNames.IsNotEmpty() && taxonNames.Count == 1)
            //{
                // Hämta föräldraträdet.
                // TaxonTreeNode pekar på taxonNames[0].Taxon så det gäller
                // att gå upp i trädet via taxonTreeNode.Parents för att få
                // information om föräldrarna.
                //taxonTreeNode = taxonNames[0].Taxon.GetParentTaxonTree(getUserContext(), true);
            //}
            // else. Hantera problemet om sökningen gav noll eller flera träffar.

            return taxonNames;
        }

    }
}