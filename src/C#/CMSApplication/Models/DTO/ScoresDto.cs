namespace CMSApplication.Models.DTO
{
    public class ScoresDTO
    {
        public int      UserId      { get; set; }
        public long     QuizId      { get; set; }
        public decimal  Score       { get; set; }
        public int      Attempts    { get; set; }
        public int      Corrected   { get; set; }

        public string   FullName    { get; set; }
        public string   Email       { get; set; }
        public string   QuizTitle   { get; set; }
    }
}
