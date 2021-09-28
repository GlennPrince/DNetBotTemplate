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
            cachedData.StringSet("guild:" + guild.Id.ToString(), serializedGuild);
            return SendEvent("guild", "JoinedGuild", "DNetBot.Guild.Joined", "guild:" + guild.Id.ToString());
        }

        // Handles the bot being initially removed from a guild / server
        private Task GuildLeave(SocketGuild guild)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Guild", "Left Guild: " + guild.Id);
            cachedData.KeyDelete("guild:" + guild.Id.ToString());
            return SendEvent("guild", "LeftGuild", "DNetBot.Guild.Left", "guild:" + guild.Id.ToString());
        }
        #endregion

        #region Server Update Events
        // Handles any actions required when a guild is ready to be interacted (After a reset or disconnect)
        private Task GuildAvailable(SocketGuild guild)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Guild", "Guild Became Available: " + guild.Id);
            var serializedGuild = new DiscordGuild(guild).ToString();
            cachedData.StringSet("guild:" + guild.Id.ToString(), serializedGuild);
            return SendEvent("guild", "GuildAvailable", "DNetBot.Guild.Available", "guild:" + guild.Id.ToString());
        }

        // Handles any actions required if the guild updates it's details
        private Task GuildUpdate(SocketGuild oldGuild, SocketGuild newGuild)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Guild", "Guild Updated: " + newGuild.Id);
            var serializedGuild = new DiscordGuild(newGuild).ToString();
            cachedData.StringSet("guild:" + newGuild.Id.ToString(), serializedGuild);
            return SendEvent("guild", "UpdatedGuild", "DNetBot.Guild.Updated", "guild:" + newGuild.Id.ToString());
        }

        // Handles processes for when a guild becomes unavailable
        private Task GuildUnAvailable(SocketGuild guild)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Guild", "Guild Became Unavailable: " + guild.Id);
            cachedData.KeyDelete("guild:" + guild.Id.ToString());
            return SendEvent("guild", "GuildUnavailable", "DNetBot.Guild.Unavailable", "guild:" + guild.Id.ToString());
        }
        #endregion

        #region Server Channel Events
        // Handles processes for when a guild creates a channel
        private Task ChannelCreated(SocketChannel channel)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Channel", "Channel Created: " + channel.Id);
            var serializedChannel = new DiscordChannel(channel).ToString();
            cachedData.StringSet("channel:" + channel.Id.ToString(), serializedChannel);
            return SendEvent("channel", "ChannelCreated", "DNetBot.Channel.Created", "channel:" + channel.Id.ToString());
        }

        // Handles actions for when a guild updates a channel
        private Task ChannelUpdated(SocketChannel oldChannel, SocketChannel newChannel)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Channel", "Channel Updated: " + newChannel.Id);
            var serializedChannel = new DiscordChannel(newChannel).ToString();
            cachedData.StringSet("channel:" + newChannel.Id.ToString(), serializedChannel);
            return SendEvent("channel", "ChannelUpdated", "DNetBot.Channel.Updated", "channel:" + newChannel.Id.ToString());
        }

        // Handles actions for when a guild deletes a channel
        private Task ChannelDeleted(SocketChannel channel)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Channel", "Channel Deleted: " + channel.Id);
            cachedData.KeyDelete("channel:" + channel.Id.ToString());
            return SendEvent("channel", "ChannelDeleted", "DNetBot.Channel.Deleted", "channel:" + channel.Id.ToString());
        }

        // Handles actions for when a user joins a channel
        private Task ChannelJoined(SocketGroupUser user)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Channel", "User: " + user.Id + " Joined Channel: " + user.Channel.Id);
            var serializedUser = new DiscordUser(user).ToString();
            cachedData.StringSet("channel_users:" + user.Channel.Id.ToString() + ":" + user.Id.ToString(), serializedUser);
            return SendEvent("channel", "ChannelJoined", "DNetBot.Channel.Joined", "channel_users:" + user.Id.ToString() + ":" + user.Channel.Id.ToString());
        }

        // Handles actions for when a user leaves a channel
        private Task ChannelLeft(SocketGroupUser user)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Channel", "User: " + user.Id + " Left Channel: " + user.Channel.Id);
            cachedData.KeyDelete("channel_users:" + user.Channel.Id.ToString() + ":" + user.Id.ToString());
            return SendEvent("channel", "ChannelLeft", "DNetBot.Channel.Left", "channel_users:" + user.Id.ToString() + ":" + user.Channel.Id.ToString());
        }
        #endregion

        #region Server Role Events
        // Handles processes for when a guild adds a role
        private Task RoleCreated(SocketRole role)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Role", "Role Created: " + role.Id);
            var serializedRole = new DiscordRole(role).ToString();
            cachedData.StringSet("role:" + role.Id.ToString(), serializedRole);
            return SendEvent("role", "RoleCreated", "DNetBot.Role.Created", "role:" + role.Id.ToString());
        }

        // Handles actions for when a guild updates a role
        private Task RoleUpdated(SocketRole oldRole, SocketRole newRole)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Role", "Role Updated: " + newRole.Id);
            var serializedRole = new DiscordRole(newRole).ToString();
            cachedData.StringSet("role:" + newRole.Id.ToString(), serializedRole);
            return SendEvent("role", "RoleUpdated", "DNetBot.Role.Updated", "role:" + newRole.Id.ToString());
        }

        // Handles actions for when a guild deletes a role
        private Task RoleDeleted(SocketRole role)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Role", "Role Deleted: " + role.Id);
            cachedData.KeyDelete("role:" + role.Id.ToString());
            return SendEvent("role", "RoleDeleted", "DNetBot.Role.Deleted", "role:" + role.Id.ToString());
        }
        #endregion

        #region User Information Update Events
        private Task UserBan(SocketUser user, SocketGuild guild)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Guild", "User: " + user.Id + " Banned From: " + guild.Id);
            var serializedUser = new DiscordUser(user).ToString();
            cachedData.StringSet("banned_users:" + guild.Id.ToString() + ":" + user.Id.ToString(), serializedUser);
            return SendEvent("user", "UserBanned", "DNetBot.User.Banned", "banned_users:" + user.Id.ToString() + ":" + guild.Id.ToString());
        }


        private Task UserUnban(SocketUser user, SocketGuild guild)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Guild", "User: " + user.Id + " Unbanned From: " + guild.Id);
            cachedData.KeyDelete("banned_users:" + guild.Id.ToString() + ":" + user.Id.ToString());
            return SendEvent("user", "UserUnbanned", "DNetBot.User.Unbanned", "banned_users:" + user.Id.ToString() + ":" + guild.Id.ToString());
        }
        #endregion
    }
}
