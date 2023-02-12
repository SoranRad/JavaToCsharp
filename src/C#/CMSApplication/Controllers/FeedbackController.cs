using CMSApplication.Data.Entity;
using CMSApplication.Enums;
using CMSApplication.Models;
using CMSApplication.Models.BindingModel;
using CMSApplication.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace CMSApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly ILogger<QuestionController>    _logger;
        private readonly IFeedbackService               _feedbackService;

        public FeedbackController
            (
                ILogger<QuestionController>     logger,
                IFeedbackService                feedbackService
            )
        {
            _logger             = logger;
            _feedbackService    = feedbackService;
        }


        [HttpPost]
        public async Task<object> Post(FeedbackBindingModel feedbackModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var feedback = new Feedback()
                    {
                        ContactNumber = feedbackModel.ContactNumber,
                        Commnets = feedbackModel.Commnets,
                        Email = feedbackModel.Email,
                        FullName = feedbackModel.FullName,
                        Rate = feedbackModel.Rate
                    };
                    feedback = await _feedbackService.AddFeedback(feedback);
                    return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", feedback));
                }

                return await Task.FromResult(new 
                    ResponseModel
                    (
                        ResponseCode.Error, 
                        null,
                        ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                    );

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message, null));
            }

        }

        [HttpGet]
        public async Task<object> GetFeedBacks()
        {
            try
            {
                var feedbacks = await _feedbackService.GetFeedbacks();
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", feedbacks));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message, null));
            }

        }

        [HttpGet("/api/Feedback/IsContactNumberUnique/{contactNumber}")]
        public async Task<object> IsContactNumberUnique(string contactNumber)
        {
            try
            {
                var result = await _feedbackService.IsContactNumberUnique(contactNumber);
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message, null));
            }

        }

    }
}
