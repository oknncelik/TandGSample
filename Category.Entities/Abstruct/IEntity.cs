namespace Category.Entities.Abstruct
{
    public interface IEntity
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool ActiveFlag { get; set; }
    }
}
