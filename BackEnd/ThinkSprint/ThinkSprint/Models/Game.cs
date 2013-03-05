using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThinkSprint.Models
{
    public class Game
    {
        public int PlayerIndex = 0;
        public string Name = Guid.NewGuid().ToString();
        public QuestionProvider Provider = new QuestionProvider();
        public List<Player> Players = new List<Player>();
    }
}