using CMSApplication.Data.Entity;
using CMSApplication.Models.BindingModel;
using CMSApplication.Models.DTO;

namespace CMSApplication.Services.Abstraction
{
    public interface IScoreService
    {
        Task<Scores> AddScore(Scores Score);

        Task<List<ScoresDTO>> GetScoress();
    }
}
