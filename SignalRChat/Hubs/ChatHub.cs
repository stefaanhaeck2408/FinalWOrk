using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    public class ChatHub: Hub
    {
        public async Task SendMessage(int quizId, string message) {
            await Clients.All.SendAsync("ReceiveMessage", quizId, message);
        }

        public async Task SendQuestion(Object question, object quizId)
        {
            await Clients.All.SendAsync("ReceiveQuestion", question, quizId);
        }
        public async Task SendAnswer(string team,string answer, int id)
        {
            await Clients.All.SendAsync("ReceiveAnswer", team, answer, id);
        }

        public async Task SendScoreboard(object teams, object quizId)
        {
            await Clients.All.SendAsync("ReceiveScoreboard", teams, quizId);
        }

        public async Task GiveQuizOk(int quizId) {
            var ok = true;
            await Clients.All.SendAsync("RoundGetsTheOk", ok);
        }

        public async Task UserLeftGame(string teamName, int teamId) {
            await Clients.All.SendAsync("ReceiveUserLeftGame", teamName, teamId);
        }
    }
}
