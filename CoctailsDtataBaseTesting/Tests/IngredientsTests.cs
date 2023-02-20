using Api = CoctailsDtataBaseTesting.ApiHelpers.CoctailApi;

namespace CoctailsDtataBaseTesting
{
    [TestClass]
    public class IngredientsTests
    {
        private const string validAlcoholicIngredientName = "Vodka";
        private const string validNonAlcoholicIngredientName = "Water";

        /// <summary>
        /// Alcoholic and non alcoholic options have the same set of properties, so checking both in data driven way
        /// </summary>
        /// <param name="searchItem">ingredient search string</param>

        [DataTestMethod, Description("The system shall include a method to search by ingredient name and return the following")]
        [DataRow(validNonAlcoholicIngredientName)]
        [DataRow(validAlcoholicIngredientName)]
        public void TestNonAlcoholIngredientSchema(string searchItem)
        {
            //Arrange
            var expectedFieldsNames = new List<string>
            {
                "idIngredient",
                "strIngredient",
                "strDescription",
                "strType",
                "strAlcohol",
                "strABV"
            };

            //Act
            var ingredient = Api.GetIngredientsByName(searchItem).First();
            var actualFieldsNames = ingredient.GetType().GetProperties().Select(prop => prop.Name).ToList();

            //Assert
            CollectionAssert.AreEquivalent(expectedFieldsNames, actualFieldsNames, "Unexpected Ingredient field name");
        }

        [DataTestMethod, Description("The system shall include a method to search by ingredient name and return the following (Alcoholic ingredient)")]              
        public void TestAlcoholIngredientFieldsTypes()
        {
            //Act
            var ingredient = Api.GetIngredientsByName(validAlcoholicIngredientName).First();      
            var ingredientDict = ingredient.GetType().GetProperties().ToDictionary(prop => prop.Name, prop => prop.GetValue(ingredient));

            var actualValues = ingredientDict.Values.ToList();

            //Assert
            //Assuming all fields in Alcoholic ingredient are populated and are strings
            actualValues.ForEach(val => Assert.IsTrue(val is string, "All ingredient's properties should be a string type"));           
        }

        [DataTestMethod, Description("The system shall include a method to search by ingredient name and return the following")]
        public void TestNonAlcoholIngredientFieldsTypes()
        {
            //Arrange
            //A bit of question, in the task "If an ingredient is non-alcoholic, Alcohol is null and ABV is null" but if ingredient is non alcoholic, value is "No" (at least for the water)
            var expectedStrAlcoholValue = "No";
            var expectedStringValuesQuantity = 5;

            //Act
            var ingredient = Api.GetIngredientsByName(validNonAlcoholicIngredientName).First();
            var ingredientDict = ingredient.GetType().GetProperties().ToDictionary(prop => prop.Name, prop => prop.GetValue(ingredient));
                     
            var ingredientValuesStrings = ingredientDict.Values.ToList().Where(val => val is not null).ToList();

            // We assume that only strABV is null, so will check the lengh of string values list           
            var abvNullValue = ingredient.strABV;

            //Assert            
            Assert.AreEqual(expectedStrAlcoholValue, ingredient.strAlcohol, "strAlcohol value is not correct");
            Assert.AreEqual(expectedStringValuesQuantity, ingredientValuesStrings.Count, "Only strAlcohol was expected to be Null");
            Assert.IsNull(abvNullValue, "strABV should be Null for the non-alcoholic ingredient");
            ingredientValuesStrings.ForEach(val => Assert.IsTrue(val is string, "Ingredient's properties should be a string type"));
        }

        [DataTestMethod, Description("Searching for an ingredient by name is case-insensitive (additional test)")]
        [DataRow("VoDkA")]
        [DataRow("vodka")]
        [DataRow("VODKA")]
        public void TestsSearchIngredientIsCaseInsensetive(string searchParameter)
        {
            //Act
            var baseIngredient = Api.GetIngredientsByName(validAlcoholicIngredientName).Select(drink => drink.strIngredient.ToString()).ToList();
            var testIngredient = Api.GetIngredientsByName(searchParameter).Select(ing => ing.strIngredient.ToString()).ToList();

            //Assert
            CollectionAssert.AreEquivalent(baseIngredient, testIngredient, "Search is case sensetive or returned incorrect values");
        }
    }
}
