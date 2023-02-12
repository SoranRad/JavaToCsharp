using CMSApplication.Data.Entity;
using CMSApplication.Enums;
using CMSApplication.Models;
using CMSApplication.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace CMSApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<QuizController> _logger;

        public CategoryController
            (
                ICategoryService categoryService,
                ILogger<QuizController> logger
            )
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<object> Post(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    category = await _categoryService.addCategory(category);

                    return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", category));
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
        public async Task<object> getCategories()
        {
            try
            {

                var list = await _categoryService.getCategories();

                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", list));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message, null));
            }

        }

        [HttpGet("{Id}")]
        public async Task<object> getCategory(long Id)
        {
            try
            {

                var item = await _categoryService.getCategory(Id);

                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", item));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message, null));
            }

        }

        [HttpPut]
        public async Task<object> put(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    category = await _categoryService.updateCategory(category);

                    return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", category));
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
                 
                    await _categoryService.deleteCategory(Id);

                    return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", null));
                 
                 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message, null));
            }

        }



    }
}
