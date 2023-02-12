namespace CMSApplication.Data.Entity
{
    public class Quiz : BaseEntity<long>
    {
        public string title             { get; set; }
        public string description       { get; set; }
        public string maxMarks          { get; set; }
        public string numberOfQuestions { get; set; }
        public bool   active            { get; set; }
        public long   CategorId         { get; set; }
        public Category? Category       { get; set; }

        public virtual ICollection<Question> Questions  { get; set; }
        public virtual ICollection<Scores>   Scores     { get; set; }
    }
}
