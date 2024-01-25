namespace AcccuLynx.Models
{
    public class QuestionResults
    {
        public List<Question> items { get; set; }
        public bool has_more { get; set; }
        public int quota_max { get; set; }
        public int quota_remaining { get; set; }
    }
    
    public class Question
    {
        public List<string> tags { get; set; }        
        public QuestionOwner owner { get; set; }
        public bool is_answered { get; set; }
        public int view_count { get; set; }
        public int accepted_answer_id { get; set; }
        public int answer_count { get; set; }
        public int score { get; set; }
        public int last_activity_date { get; set; }
        public int creation_date { get; set; }
        public int last_edit_date { get; set; }
        public int question_id { get; set; }
        public string content_license { get; set; }
        public string link { get; set; }
        public string title { get; set; }
        public int? closed_date { get; set; }
        public string closed_reason { get; set; }
    }

    public class QuestionOwner
    {
        public int account_id { get; set; }
        public int reputation { get; set; }
        public int user_id { get; set; }
        public string user_type { get; set; }
        public int accept_rate { get; set; }
        public string profile_image { get; set; }
        public string display_name { get; set; }
        public string link { get; set; }
    }

   

    public class AnswerResults
    {
        public List<Answer> items { get; set; }
        public bool has_more { get; set; }
        public int quota_max { get; set; }
        public int quota_remaining { get; set; }
    }
    public class Answer
    {
        public AnswerOwner owner { get; set; }
        public bool is_accepted { get; set; }
        public int score { get; set; }
        public int last_activity_date { get; set; }
        public int last_edit_date { get; set; }
        public int creation_date { get; set; }
        public int answer_id { get; set; }
        public int question_id { get; set; }
        public string content_license { get; set; }
        public string body { get; set; }
    }

    public class AnswerOwner
    {
        public int account_id { get; set; }
        public int reputation { get; set; }
        public int user_id { get; set; }
        public string user_type { get; set; }
        public string profile_image { get; set; }
        public string display_name { get; set; }
        public string link { get; set; }
        public int? accept_rate { get; set; }
    }

   


}
