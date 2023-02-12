using CMSApplication.Data.Entity;
using CMSApplication.Enums;
using CMSApplication.Models.DTO;
using CMSApplication.Models;
using CMSApplication.Models.BindingModel;
using CMSApplication.Services.Abstraction;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CMSApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ScoresController : ControllerBase
    {
        private readonly ILogger<ScoresController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly IScoreService _scoreService;

        public ScoresController
            (
                ILogger<ScoresController> logger, 
                UserManager<User> userManager,
                IScoreService scoreService
            )
        {
            _logger = logger;
            _userManager = userManager;
            _scoreService = scoreService;
        }

        private async Task<User> getCurrentUser()
        {
            try
            {
                var email =    HttpContext.User.Claims.Where(x => x.Type == "UserName").FirstOrDefault() 
                            ?? HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault();

                var user = await _userManager.FindByEmailAsync(email.Value);
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        [HttpPost]
        public async Task<object> Post(ScoresBindingModel scoresBinding)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await getCurrentUser();
                    if (user == null)
                        throw new Exception("User not found");

                    var score = new Scores()
                    {
                        Attempts = scoresBinding.Attempts,
                        Corrected = scoresBinding.Corrected,
                        Score = scoresBinding.Score,
                        UserId = user.Id,
                        QuizId = scoresBinding.QuizId
                    };

                    score = await _scoreService.AddScore(score);

                    return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", score));
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


        [HttpGet]
        [AllowAnonymous]
        public async Task<object> GetScores()
        {
            try
            {
                var Scoress = await _scoreService.GetScoress();
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", Scoress));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message, null));
            }

        }

    }
}
