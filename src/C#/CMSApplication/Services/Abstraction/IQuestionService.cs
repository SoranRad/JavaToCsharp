using CMSApplication.Data.Entity;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CMSApplication.Services.Abstraction
{
    public interface IQuestionService
    {
         Task<Question> addQuestion(Question question);

         Task<Question> updateQuestion(Question question);

         Task<List<Question>> getQuestions();

         Task<Question> getQuestion(long questionId);

         Task<List<Question>> getQuestionsOfQuiz(Quiz quiz);

         Task deleteQuestion(long quesId);
         
    }
}
