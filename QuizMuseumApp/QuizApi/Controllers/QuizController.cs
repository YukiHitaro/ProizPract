using Microsoft.AspNetCore.Mvc;
using QuizApi.Models;
using QuizApi.Services;

namespace QuizApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuizController : ControllerBase
{
    private readonly IWebHostEnvironment _env;

    public QuizController(IWebHostEnvironment env)
    {
        _env = env;
    }

    [HttpGet]
    public ActionResult<List<QuizQuestion>> GetQuestions()
    {
        var path = Path.Combine(_env.ContentRootPath, "Data", "quiz_data.json");
        var questions = QuizRepository.LoadFromJson(path);
        return Ok(questions);
    }
}