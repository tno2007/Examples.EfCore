namespace Examples.EfCore.Entities.Form
{
    using Examples.EfCore.Entities.Base;

    public class Questionnaire : BaseEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public virtual ICollection<Question>? Questions { get; set; }

    }
}
