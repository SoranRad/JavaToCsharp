using CMSApplication.Data;
using CMSApplication.Data.Entity;
using CMSApplication.Services.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace CMSApplication.Services.Implementation
{
    public class FeedbackService : IFeedbackService
    {
        private readonly DBContext _context;

        public FeedbackService(DBContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Feedback> AddFeedback(Feedback Feedback)
        {
            await _context.Feedbacks.AddAsync(Feedback);
            await _context.SaveChangesAsync();

            return Feedback;
        }

        public async Task<List<Feedback>> GetFeedbacks()
        {
            return await _context.Feedbacks.ToListAsync();
        }

        public async Task<bool> IsContactNumberUnique(string contactNumber)
        {
            var result = await 
                _context
                .Feedbacks
                .AnyAsync(x => x.ContactNumber.Trim() == contactNumber.Trim());

            return !result;
        }
    }
}
