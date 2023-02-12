using CMSApplication.Data.Entity; 

namespace CMSApplication.Services.Abstraction
{
    public interface IQuizService
    {
        Task<Quiz> addQuiz(Quiz quiz);

        Task<Quiz> updateQuiz(Quiz quiz);

        Task<List<Quiz>> getQuizzes();

        Task<Quiz> getQuiz(long quizId);

        Task deleteQuiz(long quizId);

        Task<List<Quiz>> getQuizzesOfCategory(Category category);

        Task<List<Quiz>> getActiveQuizzes();

        Task<List<Quiz>> getActiveQuizzesOfCategory(Category c);
    }
}
