using Examples.EfCore.Entities.Base;

namespace Examples.EfCore.Entities.Library
{
    public class Book : BaseEntity
    {
        public int Id { get; set; }
        public string? Isbn { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Language { get; set; }
        public int Pages { get; set; }
        public virtual Publisher? Publisher { get; set; }
    }
}

