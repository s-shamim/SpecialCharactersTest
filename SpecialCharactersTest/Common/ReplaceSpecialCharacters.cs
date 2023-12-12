namespace SpecialCharactersTest.Common
{
    public static class ReplaceSpecialCharacters
    {
        public static string ReplacePercentAndUnderscoreCharacters(string input)
        {
            string output = input
                .Replace("%", Constants.ReplacementStringForPercent)
                .Replace("_", Constants.ReplacementStringForUnderscore);

            return output;
        }

        public static string ReplacePercentAndUnderscoreReplacementBackToCharacters(string input)
        {
            string output = input
                .Replace(Constants.ReplacementStringForPercent, "%")
                .Replace(Constants.ReplacementStringForUnderscore, "_");

            return output;
        }
    }

}
