using Examples.EfCore.Entities.Base;

namespace Examples.EfCore.Entities.Library
{
    public class Publisher : BaseEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public virtual ICollection<Book>? Books { get; set; }
    }
}

