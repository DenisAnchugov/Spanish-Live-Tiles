namespace LiveSpanish.WindowsPhone.DataAccess.Entities
{
    public static class TileDependancyProvider
    {
        const string SecondaryTileId = "liveSpanishSecondary";

        public static string GetSecondaryTileId { get { return SecondaryTileId; } }

        public static ExpressionProvider GetExpressionProvider()
        {
            return new ExpressionProvider();
        }
    }
}
