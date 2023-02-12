using CMSApplication.Data.Entity;
using CMSApplication.Enums;
using CMSApplication.Models;
using CMSApplication.Models.DTO;
using CMSApplication.Services.Abstraction;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Category = CMSApplication.Data.Entity.Category;

namespace CMSApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;
        private readonly ILogger<QuizController> _logger;

        public QuizController
        (
            IQuizService quizService,
            ILogger<QuizController> logger
        )
        {
            _quizService = quizService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<object> Post(QuizDTO quizDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var quiz = new Quiz()
                    {
                        title = quizDto.title,
                        description = quizDto.description,
                        maxMarks = quizDto.maxMarks,
                        numberOfQuestions = quizDto.numberOfQuestions,
                        active = quizDto.active,
                        CategorId = quizDto.CategorId
                    };
                    quiz = await _quizService.addQuiz(quiz);
                         
                    return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", quiz));
                }

                return await Task.FromResult
                (
                    new ResponseModel
                    (
                        ResponseCode.Error,
                        null,
                        ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)
                    )
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message, null));
            }

        }

        [HttpPut]
        public async Task<object> put(QuizDTO quizDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var quiz = new Quiz()
                    {
                        title = quizDto.title,
                        description = quizDto.description,
                        maxMarks = quizDto.maxMarks,
                        numberOfQuestions = quizDto.numberOfQuestions,
                        active = quizDto.active,
                    };
                    quiz = await _quizService.updateQuiz(quiz);

                    return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", quiz));
                }

                return await Task.FromResult(new ResponseModel(ResponseCode.Error, null, null));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message, null));
            }

        }

        [HttpGet]
        public async Task<object> Get( )
        {
            try
            {
                
                    var list = await _quizService.getQuizzes();

                    return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", list));
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message, null));
            }

        }

        [HttpGet("{Id}")]
        public async Task<object> GetQuiz(long Id)
        {
            try
            {
                 
                    var item = await _quizService.getQuiz(Id);

                    return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", item));
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message, null));
            }

        }

        [HttpDelete("{Id}")]
        public async Task<object> Delete(long Id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                     await _quizService.deleteQuiz(Id);

                    return await Task.FromResult(new ResponseModel(ResponseCode.OK, "",null));
                }

                return await Task.FromResult(new ResponseModel(ResponseCode.Error, null, null));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message, null));
            }

        }


        [HttpGet("/api/quiz/category/{id}")]
        public async Task<object> getQuizzesOfCategory(long Id)
        {
            try
            {
                var list = await _quizService.getQuizzesOfCategory(new Category(){Id = Id});
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", list));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message, null));
            }

        }

        [HttpGet("/api/quiz/active")]
        public async Task<object> getActiveQuizzes()
        {
            try
            {
                var list = await _quizService.getActiveQuizzes();
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", list));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message, null));
            }

        }


        [HttpGet("/api/quiz/category/active/{Id}")]
        public async Task<object> getActiveQuizzes(long Id)
        {
            try
            {
                var list = await _quizService.getActiveQuizzesOfCategory(new Category(){Id=Id});
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", list));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message, null));
            }

        }


    }
}
