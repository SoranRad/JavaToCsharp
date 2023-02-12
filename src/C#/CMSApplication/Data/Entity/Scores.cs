namespace CMSApplication.Data.Entity
{
    public class Scores : BaseEntity<long>
    {
        public int      UserId      { get; set; }
        public long     QuizId      { get; set; }
        public decimal  Score       { get; set; }
        public int      Attempts    { get; set; }
        public int      Corrected   { get; set; }

        public User     User        { get; set; }
        public Quiz     Quiz        { get; set; }
    }
}
