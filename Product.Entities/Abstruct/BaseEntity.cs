using System.ComponentModel.DataAnnotations;

namespace Product.Entities.Abstruct
{
    public abstract class BaseEntity : IEntity
    {
        [Key]
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool ActiveFlag { get; set; }
    }
}
