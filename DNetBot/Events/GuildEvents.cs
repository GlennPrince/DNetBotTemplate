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
        #region Server Join / Leave Events
        // Handles the bot being initially added to a guild / server
        private Task GuildJoin(SocketGuild guild)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Guild", "Joined Guild: " + guild.Id);
            var serializedGuild = new DiscordGuild(guild).ToString();
            return SendEvent("guild", "JoinedGuild", "DNetBot.Guild.Joined", serializedGuild);
        }

        // Handles the bot being initially removed from a guild / server
        private Task GuildLeave(SocketGuild guild)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Guild", "Left Guild: " + guild.Id);
            var serializedGuild = new DiscordGuild(guild).ToString();
            return SendEvent("guild", "LeftGuild", "DNetBot.Guild.Left", serializedGuild);
        }
        #endregion

        #region Server Update Events
        // Handles any actions required when a guild is ready to be interacted (After a reset or disconnect)
        private Task GuildAvailable(SocketGuild guild)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Guild", "Guild Became Available: " + guild.Id);
            var serializedGuild = new DiscordGuild(guild).ToString();

            cachedData.StringSet("guild:" + guild.Id.ToString(), serializedGuild);
            return SendEvent("guild", "GuildAvailable", "DNetBot.Guild.Available", guild.Id.ToString());
        }

        // Handles any actions required if the guild updates it's details
        private Task GuildUpdate(SocketGuild oldGuild, SocketGuild newGuild)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Guild", "Guild Updated: " + newGuild.Id);
            var serializedGuild = new DiscordGuild(newGuild).ToString();
            return SendEvent("guild", "UpdatedGuild", "DNetBot.Guild.Updated", newGuild.Id.ToString());
        }

        // Handles processes for when a guild becomes unavailable
        private Task GuildUnAvailable(SocketGuild guild)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Guild", "Guild Became Unavailable: " + guild.Id);
            var serializedGuild = new DiscordGuild(guild).ToString();
            return SendEvent("guild", "GuildUnavailable", "DNetBot.Guild.Unavailable", serializedGuild);
        }
        #endregion

        #region Server Channel Events
        // Handles processes for when a guild adds a role
        private Task ChannelCreated(SocketChannel channel)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Channel", "Channel Created: " + channel.Id);
            var serializedChannel = new DiscordChannel(channel).ToString();
            return SendEvent("channel", "ChannelCreated", "DNetBot.Channel.Created", serializedChannel);
        }

        // Handles actions for when a guild updates a role
        private Task ChannelUpdated(SocketChannel oldChannel, SocketChannel newChannel)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Channel", "Channel Updated: " + newChannel.Id);
            var serializedChannel = new DiscordChannel(newChannel).ToString();
            return SendEvent("channel", "ChannelUpdated", "DNetBot.Channel.Updated", serializedChannel);
        }

        // Handles actions for when a guild deletes a role
        private Task ChannelDeleted(SocketChannel channel)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Channel", "Channel Deleted: " + channel.Id);
            var serializedChannel = new DiscordChannel(channel).ToString();
            return SendEvent("channel", "ChannelDeleted", "DNetBot.Channel.Deleted", serializedChannel);
        }

        // Handles actions for when a guild deletes a role
        private Task ChannelJoined(SocketGroupUser user)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Channel", "User: " + user.Id + " Joined Channel: " + user.Channel.Id);
            var serializedUser = new DiscordUser(user).ToString();
            return SendEvent("channel", "ChannelJoined", "DNetBot.Channel.Joined", serializedUser);
        }

        // Handles actions for when a guild deletes a role
        private Task ChannelLeft(SocketGroupUser user)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Channel", "User: " + user.Id + " Left Channel: " + user.Channel.Id);
            var serializedUser = new DiscordUser(user).ToString();
            return SendEvent("channel", "ChannelLeft", "DNetBot.Channel.Left", serializedUser);
        }
        #endregion

        #region Server Role Events
        // Handles processes for when a guild adds a role
        private Task RoleCreated(SocketRole role)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Role", "Role Created: " + role.Id);
            var serializedRole = new DiscordRole(role).ToString();
            return SendEvent("role", "RoleCreated", "DNetBot.Role.Created", serializedRole);
        }

        // Handles actions for when a guild updates a role
        private Task RoleUpdated(SocketRole oldRole, SocketRole newRole)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Role", "Role Updated: " + newRole.Id);
            var serializedRole = new DiscordRole(newRole).ToString();
            return SendEvent("role", "RoleUpdated", "DNetBot.Role.Updated", serializedRole);
        }

        // Handles actions for when a guild deletes a role
        private Task RoleDeleted(SocketRole role)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Role", "Role Deleted: " + role.Id);
            var serializedRole = new DiscordRole(role).ToString();
            return SendEvent("role", "RoleDeleted", "DNetBot.Role.Deleted", serializedRole);
        }
        #endregion

        #region User Information Update Events
        private Task UserBan(SocketUser user, SocketGuild guild)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Guild", "User: " + user.Id + " Banned From: " + guild.Id);
            var serializedUser = new DiscordUser(user).ToString();
            return SendEvent("user", "UserBanned", "DNetBot.User.Banned", serializedUser);
        }


        private Task UserUnban(SocketUser user, SocketGuild guild)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Guild", "User: " + user.Id + " Unbanned From: " + guild.Id);
            var serializedUser = new DiscordUser(user).ToString();
            return SendEvent("user", "UserUnbanned", "DNetBot.User.Unbanned", serializedUser);
        }
        #endregion
    }
}
