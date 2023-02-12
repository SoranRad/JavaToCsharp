using CMSApplication.Data;
using CMSApplication.Data.Entity;
using CMSApplication.Services.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace CMSApplication.Services.Implementation
{
    public class QuestionService : IQuestionService
    {
        private readonly DBContext _context;

        public QuestionService(DBContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Question> addQuestion(Question question)
        {
            await _context.Questions.AddAsync(question);
            await _context.SaveChangesAsync();

            return question;
        }

        public async Task<Question> updateQuestion(Question question)
        {
            _context.Attach(question);
            _context.Entry(question).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return question;
        }

        public async Task<List<Question>> getQuestions()
        {
            return await _context.Questions.ToListAsync();
        }

        public async Task<Question> getQuestion(long questionId)
        {
            var obj = await _context.Questions.Where(x => x.Id == questionId).FirstOrDefaultAsync();
            if (obj == null) throw new Exception("Quiz does not exist");
            return obj;
        }

        public async Task<List<Question>> getQuestionsOfQuiz(Quiz quiz)
        {
            return await _context.Questions.Where(x=>x.QuizID== quiz.Id).ToListAsync();
        }

        public async Task deleteQuestion(long quesId)
        {
            var question = new Question() { Id = quesId };
            _context.Entry(question).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
 
    }
}
