using Api = CoctailsDtataBaseTesting.ApiHelpers.CoctailApi;

namespace CoctailsDtataBaseTesting
{
    [TestClass]
    public class CoctailsTests
    {
        private readonly string validCoctailName = "Margarita";

        [TestMethod, Description("The system shall include a method to search by cocktail name.")]
        public void TestValidCoctailSearchReturnsData()
        {
            //Act
            var coctailsNames = Api.GetDrinksByName(validCoctailName).Select(drink => drink.strDrink.ToString()).ToList();

            //Assert
            coctailsNames.ForEach(name => StringAssert.Contains(name, validCoctailName, "Incorrect or no coctail name is found"));
        }

        [TestMethod, Description("If the cocktail does not exist in the cocktail DB, the API shall return drinks as null.")]
        public void TestInvalidCoctailSearchReturnsNull()
        {
            //Arrange
            var inValidCoctailName = "ThereIsNoMargarita";

            //Act
            var drinks = Api.GetDrinksByName(inValidCoctailName);

            //Assert
            Assert.IsNull(drinks, "Null was expected as search result, but no success");
        }

        /// <summary>
        /// Checking different options for the upper an lower case. As an expected values I'm using response for basic "Margarita" string
        /// I'ts a basic kind of data driven test
        /// </summary>
        /// <param name="searchParameter">Lower and upper case values to test</param>

        [DataTestMethod, Description("Searching for a cocktail by name is case-insensitive")]
        [DataRow("MaRgArItA")]
        [DataRow("margarita")]
        [DataRow("MARGARITA")]
        public void TestsSearchCoctailsIsCaseInsensetive(string searchParameter)
        {
            //Act
            var baseDrinks = Api.GetDrinksByName(validCoctailName).Select(drink => drink.strDrink.ToString()).ToList();
            var TestDrinks = Api.GetDrinksByName(searchParameter).Select(drink => drink.strDrink.ToString()).ToList();

            //Assert
            CollectionAssert.AreEquivalent(baseDrinks, TestDrinks, "Search is case sensetive or returned incorrect values");
        }

        /// <summary>
        /// Pretty ugly test, becasue we have predefined object for Json deserealization with
        /// all types and names, but here I'm showing that I know about dictionaries :D
        /// </summary>
        [TestMethod, Description("API response must contain the following Schema properties")]
        public void TestResponceShoudContainSchemaProperties()            
        {
            //Arrange
            var expectedPropertiesNames = new List<string>
            {
                "strDrink",
                "strDrinkAlternate",  // In the task there was misspelling "strDrinkAlternative" instead of actual "strDrinkAlternate" If it was real world I'd double check the requirements
                "strTags",
                "strVideo",
                "strCategory",
                "strIBA",
                "strAlcoholic",
                "strGlass",
                "strInstructions",
                "strDrinkThumb",
                "strIngredient1",
                "strMeasure1",
                "strImageSource",
                "strImageAttribution",
                "strCreativeCommonsConfirmed",
                "dateModified"
            };

            //Act
            var response = Api.Search.Coctails(validCoctailName);
            var deserializedDrinks = Api.Deserialize.CoctailsData(response);
            var drinks = deserializedDrinks.drinks;

            var coctail = deserializedDrinks.drinks.First();          
            var coctailProperties = coctail.GetType().GetProperties().ToDictionary(prop => prop.Name, prop => prop.GetValue(coctail));            
            var actualPropertiesNames = coctailProperties.Keys.ToList();
            
            // according to the requiements we don't care if property is string or null, so I'm not checking each one, just all should be null or string
            var nullValues = coctailProperties.Values.ToList().Where(val => val is null).ToList();
            var nonNullValues = coctailProperties.Values.ToList().Where(val => val is not null).ToList();

            //Assert            
            Assert.IsTrue(drinks is Coctail[], "Drinks is expected to be and array");
            CollectionAssert.AreEquivalent(expectedPropertiesNames, actualPropertiesNames, "Unexpected drink's property name");
            nonNullValues.ForEach(value => Assert.IsTrue(value is string, "Drink's properties should be a string or Null type"));
            nullValues.ForEach(value => Assert.IsNull(value, "Drink's properties should be a string or Null type"));
        }

        [TestMethod, Description("Additional test for checking the response code")]
        public void TestSuccessResponseStatusCode()
        {
            //Arrange
            var successCode = System.Net.HttpStatusCode.OK;

            //Act
            var actualCode = Api.Search.Coctails(validCoctailName).StatusCode;
            
            //Assert
            Assert.AreEqual(successCode, actualCode, "Unexpected status code");
        }
    }
}
