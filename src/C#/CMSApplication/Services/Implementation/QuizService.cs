using CMSApplication.Data;
using CMSApplication.Data.Entity;
using CMSApplication.Services.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace CMSApplication.Services.Implementation
{
    public class QuizService : IQuizService
    {
        private readonly DBContext _context;

        public QuizService(DBContext context)
        {
            _context = context;
        }

        public async Task<Quiz> addQuiz(Quiz quiz)
        {
            try
            {
                await _context.Quizzes.AddAsync(quiz);
                await _context.SaveChangesAsync();

                return quiz;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Quiz> updateQuiz(Quiz quiz)
        {
            try
            {
                _context.Attach(quiz);
                _context.Quizzes.Entry(quiz).State =EntityState.Modified ;
                await _context.SaveChangesAsync();

                return quiz;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Quiz>> getQuizzes()
        {
            try
            {
                return await _context.Quizzes.Include(x=>x.Category).ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Quiz> getQuiz(long quizId)
        {
            try
            {
                var obj = await _context.Quizzes.Where(x=>x.Id== quizId).FirstOrDefaultAsync();
                if (obj == null) throw new Exception("Quiz does not exist");
                return obj;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task  deleteQuiz(long quizId)
        {
            try
            {
                 var quiz= new Quiz(){Id = quizId};
                 _context.Entry(quiz).State = EntityState.Deleted;
                 await _context.SaveChangesAsync();
            }
            catch (Exception e)
            { 
                throw;
            }
        }

        public async Task<List<Quiz>> getQuizzesOfCategory(Category category)
        {
            try
            {
                return await _context.Quizzes.Where(x=>x.CategorId==category.Id).ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Quiz>> getActiveQuizzes()
        {
            try
            {
                return await _context.Quizzes.Where(x => x.active).Include(x=>x.Category).ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Quiz>> getActiveQuizzesOfCategory(Category c)
        {
            try
            {
                return await _context.Quizzes.Where(x => x.active  && x.CategorId == c.Id).ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
