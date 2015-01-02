using System.Collections.Generic;

namespace LiveSpanish.WindowsPhone.DataAccess.Entities
{
    public class WordsContainer
    {
       public List<ExpressionEntity> LongWords { get; set; }
       public List<ExpressionEntity> ShortWords { get; set; } 
    }
}
