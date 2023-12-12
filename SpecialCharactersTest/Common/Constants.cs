namespace SpecialCharactersTest.Common
{
    public static class Constants
    {
        public const string RegularExpression 
            = @"^[a-zA-Z0-9 `~!@#$^&*()=+{}|;:',.<>?/-]+$";
        public const string RegularExpressionWithPercentAndUnderscore 
            = @"^[a-zA-Z0-9 `~!@#$^&*()=+{}|;:',.<>?/%_-]+$";

        public const string ReplacementStringForPercent = "{84b94bd5-7784-4e14-a35f-ddc313a906a7}";
        public const string ReplacementStringForUnderscore = "{4f76c2df-7355-482e-8b8a-c7ec750b055c}";
    }
}
