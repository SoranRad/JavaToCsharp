namespace CMSApplication.Models.DTO
{
    public class QuestionDTO
    {
        public long Id { get; set; }
        public string content { get; set; }

        public string? image { get; set; }

        public string option1 { get; set; }
        public string option2 { get; set; }
        public string option3 { get; set; }
        public string option4 { get; set; }


        public string answer { get; set; }

        public string? givenAnswer { get; set; }

        public long QuizID { get; set; }
    }
}
