using AcccuLynx.Models;
using System.Text.Json;
using static System.Net.WebRequestMethods;


namespace AcccuLynx.StackOverflow
{
    public interface IHttpClientStackOverflow
    {
        Task<List<Question>> GetQuestions();
        Task<List<Answer>> GetAnswers(int questionId);
    }

    public class HttpClientStackOverflow : IHttpClientStackOverflow
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;
        private readonly IConfiguration _config;
        
        public HttpClientStackOverflow(HttpClient client, IConfiguration config)
        {
            _config = config;
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };            
        }

        public async Task<List<Question>> GetQuestions()
        {
            List<Question>? filtered = new List<Question>();

            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);            
            int today = (int)t.TotalSeconds;            
           
            DateTime start = DateTime.UtcNow.AddMonths(-3);
            t = start - new DateTime(1970, 1, 1);
            int searchstartdate = (int)t.TotalSeconds;

            bool hasMoreResults = false;
            int pagenumber = 1;
            do
            {
                var response = await _client.GetAsync($"questions?page_size=100&page={pagenumber}&fromdate={searchstartdate}&todate={today}&site=stackoverflow");

                response.EnsureSuccessStatusCode();

                try
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    var questions = await JsonSerializer.DeserializeAsync<QuestionResults>(stream, _options);

                    hasMoreResults = questions.has_more;

                    if (questions != null)
                    {
                        filtered.AddRange(questions.items.Where(items => items.accepted_answer_id != 0 && items.answer_count > 1).ToList());
                    }
                    if (filtered?.Count >= _config.GetValue<int>("QuestionsToDisplay"))
                        break;
                }
                catch (Exception ex)
                {
                    return null;
                }
                pagenumber++;
            } while (hasMoreResults);        
            
            return filtered;           
        }

        public async Task<List<Answer>> GetAnswers(int questionId)
        {        
            // Parameters for the request, including the filter to get the body
            string parameters = $"?site=stackoverflow&order=desc&sort=activity&filter=withbody";
            
            var response = await _client.GetAsync($"questions/{questionId}/answers{parameters}");
            
            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync();

            var results = await JsonSerializer.DeserializeAsync<AnswerResults>(stream, _options);

            return results.items;
            
        }
    }
}
