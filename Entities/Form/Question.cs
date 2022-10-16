namespace Examples.EfCore.Entities.Form
{
    using Examples.EfCore.Entities.Base;

    public class Question : BaseEntity
    {
        public int Id { get; set; }
        public string? Label { get; set; }
        public string? Type { get; set; }
        public string? HelpText { get; set; }
        public bool Required { get; set; }
        public virtual Questionnaire? Questionnaire { get; set; }
    }
}

