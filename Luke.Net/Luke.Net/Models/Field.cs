namespace Luke.Net.Models
{
    public class Field
    {
        public Field(string fieldName)
        {
            FieldName = fieldName;
        }

        public string FieldName { get; private set; }
    }
}
