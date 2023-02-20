namespace CoctailsDtataBaseTesting.JsonSchema
{
    public class SearchConfig
    {
        public string Endpoint
        {
            get
            {
                return "https://www.thecocktaildb.com/api/json/v1/1/search.php";
            }
            set { }
        }
        public string SearchSufix { get; set; }
    }
}
