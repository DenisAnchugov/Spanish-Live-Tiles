namespace LiveSpanish.WindowsPhone.DataAccess.Entities
{
    public class SelectionEntity
    {
        public bool IsSelected { get; set; }
        public VocabularySetEnum SetEnum { get; set; }

        public override string ToString()
        {
            return SetEnum.ToString();
        }
    }
}
