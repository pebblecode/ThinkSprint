using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThinkSprint.Models
{
    public  class QuestionProvider
    {
        private List<Question> _questions = new List<Question>()
            {
                new Question {Answer = 3, Options = new List<string>() {"1", "50", "2", "3"}, Text = "2 + 1"},
                new Question {Answer = 1, Options = new List<string>() {"1", "8", "20", "3"}, Text = "2 + 6"},
                new Question {Answer = 0, Options = new List<string>() {"4", "50", "2", "3"}, Text = "2 x 2"},
                new Question {Answer = 2, Options = new List<string>() {"1", "50", "2", "3"}, Text = "6 / 3"},
            };

        private int i = 0;
        public Question GetNextQuestion()
        {
            var question = _questions[i % _questions.Count];
            i++;
            return question;
        }
    }
}