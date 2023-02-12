using CMSApplication.Data.Entity;
using CMSApplication.Enums;
using CMSApplication.Models;
using CMSApplication.Models.DTO;
using CMSApplication.Services.Abstraction;
using CMSApplication.Services.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMSApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        private readonly IQuizService _quizService;
        private readonly ILogger<QuestionController> _logger;

        public QuestionController
        (
            IQuestionService questionService,
            IQuizService quizService,
            ILogger<QuestionController> logger
        )
        {
            _questionService = questionService;
            _quizService = quizService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<object> Post(QuestionDTO questionDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var question = new Question()
                    {
                        QuizID = questionDto.QuizID,
                        answer = questionDto.answer,
                        content = questionDto.content,
                        image   = questionDto.image,
                        option1 = questionDto.option1,
                        option2 = questionDto.option2,
                        option3 = questionDto.option3,
                        option4 = questionDto.option4,
                        givenAnswer = questionDto.givenAnswer,
                    };
                    question = await _questionService.addQuestion(question);

                    return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", question));
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
        public async Task<object> put(QuestionDTO questionDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var question = new Question()
                    {
                        Id = questionDto.Id,
                        QuizID = questionDto.QuizID,
                        answer = questionDto.answer,
                        content = questionDto.content,
                        image = questionDto.image,
                        option1 = questionDto.option1,
                        option2 = questionDto.option2,
                        option3 = questionDto.option3,
                        option4 = questionDto.option4,
                        givenAnswer = questionDto.givenAnswer,
                    };
                    question = await _questionService.updateQuestion(question);

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

                return await Task.FromResult(new ResponseModel(ResponseCode.Error, null, null));
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
                    await _questionService.deleteQuestion(Id);

                    return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", null));
                }

                return await Task.FromResult(new ResponseModel(ResponseCode.Error, null, null));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message, null));
            }

        }

        [HttpGet("/api/Question/{qid}")]
        public async Task<object> getQuestionsOfQuiz(long qid)
        {
            try
            {
                var item = await _questionService.getQuestion(qid);

                //var item = await _questionService.getQuestionsOfQuiz(new Quiz(){Id = Id});

                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", item));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message, null));
            }

        }

        [HttpGet("/api/Question/quiz/{qid}")]
        public async Task<object> getQuestionsOfQuizAdmin(long qid)
        {
            try
            {

                var item = await _questionService.getQuestionsOfQuiz(new Quiz() { Id = qid });

                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", item));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message, null));
            }

        }

        [HttpPost("/api/Question/eval-quiz")]
        public async Task<object> evalQuiz(List<QuestionDTO> questions)
        {
            try
            {
                double marksGot = 0;
                int correctAnswers = 0;
                int attempted = 0;

                foreach (var item in questions)
                {
                    var question = await _questionService.getQuestion(item.Id);
                    var quiz = await _quizService.getQuiz(question.QuizID);

                    if (question.answer.Equals(item.givenAnswer))
                    {
                        correctAnswers++;

                        double marksSingle = double.Parse(quiz.maxMarks) / questions.Count;
                        marksGot += marksSingle;

                    }

                    if (item.givenAnswer != null)
                    {
                        attempted++;
                    }
                }

                Dictionary<string, object> map = new Dictionary<string, object>()
                {
                    { "marksGot", marksGot },
                    { "correctAnswers", correctAnswers },
                    { "attempted", attempted },
                };

                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", map));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message, null));
            }

        }

    }
}
