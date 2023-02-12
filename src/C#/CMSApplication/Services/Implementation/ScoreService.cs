using CMSApplication.Data;
using CMSApplication.Data.Entity;
using CMSApplication.Models.BindingModel;
using CMSApplication.Models.DTO;
using CMSApplication.Services.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace CMSApplication.Services.Implementation
{
    public class ScoreService : IScoreService
    {
        private readonly DBContext _dbContext;

        public ScoreService(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Scores> AddScore(Scores Score)
        {
            
            await _dbContext.Scores.AddAsync(Score);
            await _dbContext.SaveChangesAsync();

            return Score;
        }

        public async Task<List<ScoresDTO>> GetScoress()
        {
            return await _dbContext
                .Scores
                .Include(x => x.User)
                .Include(x => x.Quiz)
                .Select(x => new ScoresDTO
                {
                    Attempts    = x.Attempts,
                    Corrected   = x.Corrected,
                    Email       = x.User.Email??string.Empty,
                    FullName    = x.User.FullName,
                    Score       = x.Score,
                    UserId      = x.UserId,
                    QuizId      = x.QuizId,
                    QuizTitle   = x.Quiz.title
                }).ToListAsync();
        }

         
    }
}
