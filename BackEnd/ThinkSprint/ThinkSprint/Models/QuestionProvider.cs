using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThinkSprint.Models
{
    public  class QuestionProvider
    {
        public Question CurrentQuestion { get; private set; }
        private List<Question> _questions = new List<Question>()
            {
                new Question {Answer = 3, Options = new List<string>() {"1", "10", "2", "3"}, Text = "2 + 1"},
                new Question {Answer = 4, Options = new List<string>() {"1", "8", "4", "3"}, Text = "6 - 2"},
                new Question {Answer = 4, Options = new List<string>() {"4", "7", "2", "3"}, Text = "2 × 2"},
                new Question {Answer = 2, Options = new List<string>() {"1", "6", "2", "3"}, Text = "6 ÷ 3"},
            };

        private int i = 0;
        public Question GetNextQuestion()
        {
            var question = _questions[i % _questions.Count];
            CurrentQuestion = question;
            i++;
            return question;
        }
    }
}