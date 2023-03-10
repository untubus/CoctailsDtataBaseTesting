namespace CoctailsDtataBaseTesting
{
    public class Coctail
    {
        public string? strDrink { get; set; }
        public string? strDrinkAlternate { get; set; }
        public string? strTags { get; set; }
        public string? strVideo { get; set; }
        public string? strCategory { get; set; }
        public string? strIBA { get; set; }
        public string? strAlcoholic { get; set; }
        public string? strGlass { get; set; }
        public string? strInstructions { get; set; }
        public string? strDrinkThumb { get; set; }
        public string? strIngredient1 { get; set; }
        public string? strMeasure1 { get; set; }
        public string? strImageSource { get; set; }
        public string? strImageAttribution { get; set; }
        public string? strCreativeCommonsConfirmed { get; set; }
        public string? dateModified { get; set; }
    }

    public class Coctails
    {
        public Coctail[] drinks { get; set; }
    }
}
