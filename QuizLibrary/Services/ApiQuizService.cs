using QuizLibrary.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace QuizLibrary.Services
{
    public class ApiQuizService
    {
        private const string ApiUrl = "https://localhost:7268/api/quiz";

        public async Task<List<QuizQuestion>> LoadQuestionsAsync()
        {
            try
            {
                using var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using var client = new HttpClient(handler);

                var result = await client.GetFromJsonAsync<List<QuizQuestion>>(ApiUrl);

                if (result == null || result.Count == 0)
                    throw new Exception("Сервер вернул пустой список вопросов.");

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при загрузке данных с API: " + ex.Message, ex);
            }
        }
    }
}
