using System.Text.Json;

namespace CoctailsDtataBaseTesting.ApiHelpers
{
    public class Deserialize
    {
        public Coctails CoctailsData(RestResponse response) => JsonSerializer.Deserialize<Coctails>(response.Content);
        public Ingredients IngredientsData(RestResponse response) => JsonSerializer.Deserialize<Ingredients>(response.Content);
    }
}
