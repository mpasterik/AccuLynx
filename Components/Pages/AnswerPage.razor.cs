using AcccuLynx.Models;
using AcccuLynx.StackOverflow;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using System.Reflection;

namespace AccuLynx.Components.Pages
{
    public partial class AnswerPage
    {
        [Inject]
        public IHttpClientStackOverflow stackOverflow { get; set; }
        

        public List<Answer> answers { get; set; } = new List<Answer>();

        [Parameter]
        public int QuestionId { get; set; }
                
        protected async override Task OnInitializedAsync()
        {
            answers = await stackOverflow.GetAnswers(QuestionId);
        }

        protected async Task OnSelectedValueChanged(object value) 
        {
            bool correct = await IsThisTheAcceptedAnswer((int)value);           
            if(correct)
               Snackbar.Add(@"You have identified the Accepted Answer", Severity.Success);            
            else
               Snackbar.Add(@"That is not the Accepted Answer", Severity.Error);            
        }

        protected async Task<bool> IsThisTheAcceptedAnswer(int answer_id)
        {
            var theone = answers.Where(q => q.answer_id == answer_id).First();
            return theone.is_accepted;
        }
    }
}
