using SQLite;

namespace LiveSpanish.DataAccess.Entities
{
    public sealed class ExpressionEntity
    {
        [PrimaryKey]
        public string Expression {get; set; }
        public string Translation { get; set; }

        public override string ToString()
        {
            return Expression + " " + Translation;
        }
    }
}
