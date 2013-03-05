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


        public void SendAnswer(string name, int index)
        {
            var answer = _provider.CurrentQuestion.Options[index];

            if(!_players.Any())
                return;

            if(name == _players[playerIndex].Name)
                return;
            
            var result = new Result();
            result.Players = _players;
            result.Correct = _provider.CurrentQuestion.Answer == answer;

            if (result.Correct)
            {
                var player = _players.Single(z => z.Name == name);
                player.Score++;
            }

            Clients.All.RecieveResult(result);

            SendQuestion();
        }



        private static List<Player> _players = new List<Player>();
        public void Register(string name)
        {
            _players.Add(new Player { Name = name });

            if (_players.Count == 2)
                StartGame();
        }

        public void ResetGame()
        {
            foreach (var player in _players)
            {
                player.Score = 0;
            }

            Clients.All.OnGameReset();
        }

        public void StartGame()
        {
            SendQuestion(); ;
        }

        public override System.Threading.Tasks.Task OnDisconnected()
        {
            _players.Clear();
            return base.OnDisconnected();
        }

        private static int playerIndex = 0;
        private void SendQuestion()
        {
            var question = _provider.GetNextQuestion();
            Clients.All.RecieveQuestion(_players[playerIndex].Name, question);
            playerIndex++;
            playerIndex = playerIndex%2;
        }


    }
}