namespace CMSApplication.Models.BindingModel
{
    public class ScoresBindingModel
    {
        public decimal  Score       { get; set; }
        public int      Attempts    { get; set; }
        public int      Corrected   { get; set; }
        public long     QuizId      { get; set; }
    }
}
