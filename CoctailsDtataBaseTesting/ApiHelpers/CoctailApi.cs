namespace CoctailsDtataBaseTesting.ApiHelpers
{
    /// <summary>
    /// I gathered different functionality together in one place to make one entry point for the actions needed in tests
    /// </summary>
    public static class CoctailApi
    {
        public static Search Search => new Search();
        public static Deserialize Deserialize => new Deserialize();

        public static Coctail[] GetDrinksByName(string searchItem)
        {
            var response = Search.Coctails(searchItem);
            return Deserialize.CoctailsData(response).drinks;
        }

        public static Ingredient[] GetIngredientsByName(string searchItem)
        {
            var response = Search.Ingredients(searchItem);
            return Deserialize.IngredientsData(response).ingredients;
        }
    }
}
