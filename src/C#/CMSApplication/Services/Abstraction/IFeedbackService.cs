using CMSApplication.Data.Entity;

namespace CMSApplication.Services.Abstraction
{
    public interface IFeedbackService
    {
        Task<Feedback>          AddFeedback             (Feedback Feedback);

        Task<List<Feedback>>    GetFeedbacks            ();

        Task<bool>              IsContactNumberUnique   (string contactNumber);
    }
}
