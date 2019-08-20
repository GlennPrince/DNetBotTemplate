using Discord;
using Discord.WebSocket;
using DNetBot.Helpers;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace DNetBot.Services
{
    public partial class DiscordSocketService : IHostedService
    {
        //
        // Summary:
        //     Fired when a user is updated.
        private Task UserUpdate(SocketUser oldUser, SocketUser newUser)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "User", "User Information Updated For: " + newUser.Id);

            return Task.CompletedTask;
        }

        // Handles any actions when a members details or presence is updated
        private Task MemberUpdate(SocketUser oldUser, SocketUser newUser)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "User", "Member Information Updated For: " + newUser.Id);

            return Task.CompletedTask;
        }

        // Handles any actions when a user joins a server
        private Task UserJoined(SocketGuildUser user)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "User", "User: " + user.Id + " joined Server: " + user.Guild.Id);

            return Task.CompletedTask;
        }

        // Handles any actions when a user leaves a server
        private Task UserLeft(SocketGuildUser user)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "User", "User: " + user.Id + " left Server: " + user.Guild.Id);

            return Task.CompletedTask;
        }
    }
}
