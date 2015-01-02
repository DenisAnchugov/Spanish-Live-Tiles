using SQLite;

namespace LiveSpanish.WindowsPhone.DataAccess.Entities
{
    public sealed class ExpressionEntity
    {
        [PrimaryKey]
        public string Expression {get; set; }
        public string Translation { get; set; }
        [Ignore]
        public int ExpressionLength
        {
            get { return Expression.Length; }
        }
        
        public override string ToString()
        {
            return Expression + " " + Translation;
        }
    }
}
