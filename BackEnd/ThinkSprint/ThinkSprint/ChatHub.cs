using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using ThinkSprint.Models;

namespace ThinkSprint
{
    public class ChatHub : Hub
    {
        private static QuestionProvider _provider = new QuestionProvider();


        public void Send(string name, string message)
        {



            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(name, message);
        }


        public void SendAnswer(string name, int answer)
        {
            var result = new Result();
            result.Players = _players;
            result.Correct = _provider.CurrentQuestion.Answer == answer;

            if (result.Correct)
            {
                var player = _players.Single(z => z.Name == name);
                player.Score++;
            }

            Clients.All.RecieveResult(result);

            SendQuestion(name);
        }



        private static List<Player> _players = new List<Player>();
        public void Register(string name)
        {
            _players.Add(new Player { Name = name });

            // if (_players.Count == 2)
            StartGame();
        }

        public void StartGame()
        {
            SendQuestion(_players[0].Name); ;
        }

        public override System.Threading.Tasks.Task OnDisconnected()
        {
            _players.Clear();
            return base.OnDisconnected();
        }


        private void SendQuestion(string playerName)
        {
            var question = _provider.GetNextQuestion();
            Clients.All.RecieveQuestion(playerName, question);
        }


    }
}