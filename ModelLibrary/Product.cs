namespace ModelLibrary
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public override string ToString()
        {
            return String.Format("{0} - {1} category id = {2}, {3}", Id, Name, CategoryId, Description);
        }
    }
}
