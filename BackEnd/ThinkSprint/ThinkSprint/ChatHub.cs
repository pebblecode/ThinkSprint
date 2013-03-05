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

        private static List<string> _players = new List<string>();
        public void Register(string name)
        {
            _players.Add(name);

           // if (_players.Count == 2)
                StartGame();
        }

        public void StartGame()
        {
            SendQuestion(_players[0]);;
        }

        private void SendQuestion(string playerName)
        {
            var question = _provider.GetNextQuestion();
            Clients.All.RecieveQuestion(playerName, question);
        }


    }
}