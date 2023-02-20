using CoctailsDtataBaseTesting.JsonSchema;

namespace CoctailsDtataBaseTesting.ApiHelpers
{
    /// <summary>
    /// Here we are making actual settings for the different types of search
    /// I named actual searches to create clear pattern on which searc are we uing. Usage will be like Search.Coctails
    /// </summary>
    public class Search
    {
        private readonly SearchConfig CoctailSearchConfig = new SearchConfig { SearchSufix = "?s=" };
        private readonly SearchConfig IngredientSearchConfig = new SearchConfig { SearchSufix = "?i=" };

        public RestResponse Coctails(string searchItem) => new BasicSearch(CoctailSearchConfig, searchItem).GetResponse();
        public RestResponse Ingredients(string searchItem) => new BasicSearch(IngredientSearchConfig, searchItem).GetResponse();
    }
}
