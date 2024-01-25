using AcccuLynx.Models;
using AcccuLynx.StackOverflow;
using Microsoft.AspNetCore.Components;

namespace AccuLynx.Components.Pages
{
    public partial class QuestionPage
    {
        [Inject]
        public IHttpClientStackOverflow stackOverflow { get; set; }

        public List<Question> questions { get; set; } = new List<Question>();

        protected  async override Task OnInitializedAsync()
        {
            questions = await stackOverflow.GetQuestions();
        }
    }
}
