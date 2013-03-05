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

        private static Dictionary<string, Game> _startedGames = new Dictionary<string, Game>();
        private static Game _newGame = new Game();
        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(name, message);
        }


        public void SendAnswer(string name, int index, string gameName)
        {
            var game = _startedGames[gameName];
            var answer = game.Provider.CurrentQuestion.Options[index];

            if (!game.Players.Any())
                return;

            if (name == game.Players[game.PlayerIndex].Name)
                return;

            var result = new Result();
            result.Players = game.Players;
            result.Correct = game.Provider.CurrentQuestion.Answer == answer;

            if (result.Correct)
            {
                var player = game.Players.Single(z => z.Name == name);
                player.Score++;
            }

            Clients.Group(gameName).RecieveResult(result);

            SendQuestion(game);
        }




        public void Register(string name)
        {
            _newGame.Players.Add(new Player { Name = name });
            Clients.Caller.OnRegister(_newGame.Name);
            Groups.Add(Context.ConnectionId, _newGame.Name);
            if (_newGame.Players.Count == 2)
            {
                StartGame();
            }
        }

        public void ResetGame(string gameName)
        {
            var game = _startedGames[gameName];
            foreach (var player in game.Players)
            {
                player.Score = 0;
            }

            Clients.Group(gameName).OnGameReset();
        }

        public void StartGame()
        {
            _startedGames[_newGame.Name] = _newGame;
            SendQuestion(_newGame); 
            _newGame = new Game();
        }

        public override System.Threading.Tasks.Task OnDisconnected()
        {
            return base.OnDisconnected();
        }


        private void SendQuestion(Game game)
        {
            var question = game.Provider.GetNextQuestion();
            Clients.Group(game.Name).RecieveQuestion(game.Players[game.PlayerIndex].Name, question);
            Clients.Caller.RecieveQuestion(game.Players[game.PlayerIndex].Name, question);
            game.PlayerIndex++;
            game.PlayerIndex = game.PlayerIndex % 2;
        }


    }
}