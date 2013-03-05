using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThinkSprint.Models
{
    public class Question
    {
        public string Text { get; set; }
        public List<string> Options { get; set; }
        public string Answer { get; set; }
    }
}