using Microsoft.EntityFrameworkCore;

namespace CMSApplication.Data.Entity
{
    public class Category :  BaseEntity<long>
    {
        public Category()
        {
            Quizzes = new List<Quiz>();
        }
        public string Title { get; set; }

        public string description { get; set; }

        public virtual ICollection<Quiz> Quizzes { get; set; }
    }
}
