namespace MoexXL.MoexApi
{
    public class Attribute
    {
        public readonly string  name;
        public readonly object value;

        public Attribute(string name, object value)
        {
            this.name = name;
            this.value = value;
        }
    }

}