namespace CMSApplication.Models.DTO
{
    public class QuizDTO
    {
        public string title                 { get; set; }
        public string description           { get; set; }
        public string maxMarks              { get; set; }
        public string numberOfQuestions { get; set; }
        public bool   active                { get; set; }

        public long CategorId { get; set; }

    }
}
