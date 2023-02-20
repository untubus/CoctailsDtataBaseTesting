using CoctailsDtataBaseTesting.JsonSchema;

namespace CoctailsDtataBaseTesting
{
    /// <summary>
    /// Assuming that in the future we might want to add another functionality, like search by letter, I'm making the search class
    /// pretty generic with single responsibility. Different options of search can be added throug configuration. Actual search classes are in Search.cs
    /// </summary>
    public class BasicSearch
    {
        public BasicSearch(SearchConfig searchConfig, string searchItem)
        {
            _endpoint = searchConfig.Endpoint;
            _searchSufix = searchConfig.SearchSufix;
            _searchItem = searchItem;
        }

        private readonly string _endpoint;
        private readonly string _searchSufix;
        private readonly string _searchItem;

        public RestResponse GetResponse()
        {
            var searchParameter = $"{_searchSufix}{_searchItem}";

            RestClient client = new (_endpoint);
            RestRequest request = new (searchParameter, Method.Get);
            RestResponse responce = client.Execute(request);
            return responce;
        }
    }
}