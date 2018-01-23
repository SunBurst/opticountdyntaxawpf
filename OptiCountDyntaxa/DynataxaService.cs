using ArtDatabanken.Data;
using ArtDatabanken.WebService.Client.AnalysisService;
using ArtDatabanken.WebService.Client.ReferenceService;
using ArtDatabanken.WebService.Client.TaxonAttributeService;
using ArtDatabanken.WebService.Client.TaxonService;
using ArtDatabanken.WebService.Client.UserService;


namespace OptiCountDyntaxa
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

        public TaxonList searchTaxa(string searchString)
        {
            ITaxonSearchCriteria taxonSearchCriteria = new TaxonSearchCriteria();
            taxonSearchCriteria.IsValidTaxon = true;
            taxonSearchCriteria.TaxonNameSearchString = searchString;
            TaxonList taxa = CoreData.TaxonManager.GetTaxa(userContext, taxonSearchCriteria);

            return taxa;
        }

    }
}