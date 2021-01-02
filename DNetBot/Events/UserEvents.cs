using Discord;
using Discord.WebSocket;
using DNetBot.Helpers;
using DNetUtils.Entities;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace DNetBot.Services
{
    public partial class DiscordSocketService : IHostedService
    {
        // Handles any actions when a user is updated.
        private Task UserUpdate(SocketUser oldUser, SocketUser newUser)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "User", "User Information Updated For: " + newUser.Id);
            var serializedUser = new DiscordUser(newUser).ToString();
            return SendEvent("user", "UpdatedUser", "DNetBot.User.Updated", serializedUser);
        }

        // Handles any actions when a members details or presence is updated
        private Task MemberUpdate(SocketUser oldUser, SocketUser newUser)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "User", "Member Information Updated For: " + newUser.Id);
            var serializedUser = new DiscordUser(newUser).ToString();
            return SendEvent("user", "UpdatedMember", "DNetBot.User.Updated", serializedUser);
        }

        // Handles any actions when a user joins a server
        private Task UserJoined(SocketGuildUser user)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "User", "User: " + user.Id + " joined Server: " + user.Guild.Id);
            var serializedUser = new DiscordUser(user).ToString();
            return SendEvent("user", "UserJoined", "DNetBot.User.Joined", serializedUser);
        }

        // Handles any actions when a user leaves a server
        private Task UserLeft(SocketGuildUser user)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "User", "User: " + user.Id + " left Server: " + user.Guild.Id);
            var serializedUser = new DiscordUser(user).ToString();
            return SendEvent("user", "UserLeft", "DNetBot.User.Left", serializedUser);
        }

        private Task PrivateGroupAdd(SocketGroupUser user)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "User", "User: " + user.Id + " left Server: " + user.Channel.Id);
            var serializedUser = new DiscordUser(user).ToString();
            return SendEvent("user", "PrivateGroupAdd", "DNetBot.Group.Add", serializedUser);
        }

        private Task PrivateGroupRemove(SocketGroupUser user)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "User", "User: " + user.Id + " left private group: " + user.Channel.Id);
            var serializedUser = new DiscordUser(user).ToString();
            return SendEvent("user", "PrivateGroupRemove", "DNetBot.Group.Remove", serializedUser);
        }
    }
}
