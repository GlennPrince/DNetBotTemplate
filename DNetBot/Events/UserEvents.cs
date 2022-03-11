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
            cachedData.StringSet("user:" + newUser.Id.ToString(), serializedUser);
            return SendEvent("user", "UpdatedUser", "DNetBot.User.Updated", "user:" + newUser.Id.ToString());
        }

        // Handles any actions when a members details or presence is updated
        private Task MemberUpdate(SocketUser oldUser, SocketUser newUser)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "User", "Member Information Updated For: " + newUser.Id);
            var serializedUser = new DiscordUser(newUser).ToString();
            cachedData.StringSet("user:" + newUser.Id.ToString(), serializedUser);
            return SendEvent("user", "UpdatedMember", "DNetBot.User.Updated", "user:" + newUser.Id.ToString());
        }

        // Handles any actions when a user joins a server
        private Task UserJoined(SocketGuildUser user)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "User", "User: " + user.Id + " joined Server: " + user.Guild.Id);
            var serializedUser = new DiscordUser(user).ToString();
            cachedData.StringSet("guild_users:" + user.Guild.Id + ":" + user.Id.ToString(), serializedUser);
            return SendEvent("user", "UserJoined", "DNetBot.User.Joined", "guild_users:" + user.Guild.Id + ":" + user.Id.ToString());
        }

        // Handles any actions when a user leaves a server
        private Task UserLeft(SocketUser user, SocketGuild guild)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "User", "User: " + user.Id + " left Server: " + guild.Id);
            var serializedUser = new DiscordUser(user).ToString();
            cachedData.KeyDelete("guild_users:" + guild.Id + ":" + user.Id.ToString());
            return SendEvent("user", "UserLeft", "DNetBot.User.Left", "guild_users:" + guild.Id + ":" + user.Id.ToString());
        }

        private Task PrivateGroupAdd(SocketGroupUser user)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "User", "User: " + user.Id + " left Server: " + user.Channel.Id);
            var serializedUser = new DiscordUser(user).ToString();
            cachedData.StringSet("private_users:" + user.Channel.Id + ":" + user.Id.ToString(), serializedUser);
            return SendEvent("user", "PrivateGroupAdd", "DNetBot.Group.Add", "private_users:" + user.Channel.Id + ":" + user.Id.ToString());
        }

        private Task PrivateGroupRemove(SocketGroupUser user)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "User", "User: " + user.Id + " left private group: " + user.Channel.Id);
            var serializedUser = new DiscordUser(user).ToString();
            cachedData.KeyDelete("private_users:" + user.Channel.Id + ":" + user.Id.ToString());
            return SendEvent("user", "PrivateGroupRemove", "DNetBot.Group.Remove", "private_users:" + user.Channel.Id + ":" + user.Id.ToString());
        }
    }
}
