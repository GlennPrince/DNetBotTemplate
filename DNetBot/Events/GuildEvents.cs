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

            foreach(var channel in guild.Channels)
            {
                var fullChannel = discordClient.GetChannel(channel.Id);
                var serializedChannel = new DiscordChannel(fullChannel).ToString();
                cachedData.StringSet("channel:" + channel.Id.ToString(), serializedChannel);
            }

            string features = "";
            foreach (var feature in guild.Features)
                features += feature + ";";
            cachedData.StringSet("feature:" + guild.Id.ToString(), features);

            foreach (var user in guild.Users)
            {
                var serializedUser = new DiscordUser(user).ToString();
                cachedData.StringSet("user:" + guild.Id.ToString() + ":" + user.Id.ToString(), serializedUser);
            }

            foreach (var role in guild.Roles)
            {
                var serializedRole = new DiscordRole(role).ToString();
                cachedData.StringSet("role:" + guild.Id.ToString() + ":" + role.Id.ToString(), serializedRole);
            }

            foreach (var emote in guild.Emotes)
            {
                var serializedEmote = new DiscordEmote(emote).ToString();
                cachedData.StringSet("emote:" + guild.Id.ToString() + ":" + emote.Id.ToString(), serializedEmote);
            }

            return SendEvent("guild", "JoinedGuild", "DNetBot.Guild.Joined", "guild:" + guild.Id.ToString());
        }

        // Handles the bot being initially removed from a guild / server
        private Task GuildLeave(SocketGuild guild)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Guild", "Left Guild: " + guild.Id);
            cachedData.KeyDelete("guild:" + guild.Id.ToString());

            foreach (var channel in guild.Channels)
                cachedData.KeyDelete("channel:" + channel.Id.ToString());

            foreach (var user in guild.Users)
                cachedData.KeyDelete("user:" + guild.Id.ToString() + ":" + user.Id.ToString());

            foreach (var role in guild.Roles)
                cachedData.KeyDelete("role:" + guild.Id.ToString() + ":" + role.Id.ToString());

            cachedData.KeyDelete("feature:" + guild.Id.ToString());

            foreach (var role in guild.Roles)
                cachedData.KeyDelete("role:" + guild.Id.ToString() + ":" + role.Id.ToString());

            foreach (var emote in guild.Emotes)
                cachedData.KeyDelete("emote:" + guild.Id.ToString() + ":" + emote.Id.ToString());

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

            foreach (var channel in guild.Channels)
            {
                var fullChannel = discordClient.GetChannel(channel.Id);
                if (fullChannel.GetType() == typeof(SocketCategoryChannel))
                    cachedData.StringSet("channel:" + channel.Id.ToString(), new DiscordChannel((SocketCategoryChannel)fullChannel).ToString());
                else if (fullChannel.GetType() == typeof(SocketDMChannel))
                    cachedData.StringSet("channel:" + channel.Id.ToString(), new DiscordChannel((SocketDMChannel)fullChannel).ToString());
                else if (fullChannel.GetType() == typeof(SocketGroupChannel))
                    cachedData.StringSet("channel:" + channel.Id.ToString(), new DiscordChannel((SocketGroupChannel)fullChannel).ToString());
                else if (fullChannel.GetType() == typeof(SocketGuildChannel))
                    cachedData.StringSet("channel:" + channel.Id.ToString(), new DiscordChannel((SocketGuildChannel)fullChannel).ToString());
                else if (fullChannel.GetType() == typeof(SocketNewsChannel))
                    cachedData.StringSet("channel:" + channel.Id.ToString(), new DiscordChannel((SocketNewsChannel)fullChannel).ToString());
                else if (fullChannel.GetType() == typeof(SocketTextChannel))
                    cachedData.StringSet("channel:" + channel.Id.ToString(), new DiscordChannel((SocketTextChannel)fullChannel).ToString());
                else if (fullChannel.GetType() == typeof(SocketVoiceChannel))
                    cachedData.StringSet("channel:" + channel.Id.ToString(), new DiscordChannel((SocketVoiceChannel)fullChannel).ToString());
                else
                    cachedData.StringSet("channel:" + channel.Id.ToString(), new DiscordChannel(fullChannel).ToString());
            }

            foreach (var user in guild.Users)
            {
                var serializedUser = new DiscordUser(user).ToString();
                cachedData.StringSet("user:" + guild.Id.ToString() + ":" + user.Id.ToString(), serializedUser);
            }

            string features = "";
            foreach (var feature in guild.Features)
                features += feature + ";";
            cachedData.StringSet("feature:" + guild.Id.ToString(), features);

            foreach (var role in guild.Roles)
            {
                var serializedRole = new DiscordRole(role).ToString();
                cachedData.StringSet("role:" + guild.Id.ToString() + ":" + role.Id.ToString(), serializedRole);
            }

            foreach (var emote in guild.Emotes)
            {
                var serializedEmote = new DiscordEmote(emote).ToString();
                cachedData.StringSet("emote:" + guild.Id.ToString() + ":" + emote.Id.ToString(), serializedEmote);
            }

            return SendEvent("guild", "GuildAvailable", "DNetBot.Guild.Available", "guild:" + guild.Id.ToString());
        }

        // Handles any actions required if the guild updates it's details
        private Task GuildUpdate(SocketGuild oldGuild, SocketGuild newGuild)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Guild", "Guild Updated: " + newGuild.Id);
            var serializedGuild = new DiscordGuild(newGuild).ToString();
            cachedData.StringSet("guild:" + newGuild.Id.ToString(), serializedGuild);

            foreach (var channel in newGuild.Channels)
            {
                var fullChannel = discordClient.GetChannel(channel.Id);
                if (fullChannel.GetType() == typeof(SocketCategoryChannel))
                    cachedData.StringSet("channel:" + channel.Id.ToString(), new DiscordChannel((SocketCategoryChannel)fullChannel).ToString());
                else if (fullChannel.GetType() == typeof(SocketDMChannel))
                    cachedData.StringSet("channel:" + channel.Id.ToString(), new DiscordChannel((SocketDMChannel)fullChannel).ToString());
                else if (fullChannel.GetType() == typeof(SocketGroupChannel))
                    cachedData.StringSet("channel:" + channel.Id.ToString(), new DiscordChannel((SocketGroupChannel)fullChannel).ToString());
                else if (fullChannel.GetType() == typeof(SocketGuildChannel))
                    cachedData.StringSet("channel:" + channel.Id.ToString(), new DiscordChannel((SocketGuildChannel)fullChannel).ToString());
                else if (fullChannel.GetType() == typeof(SocketNewsChannel))
                    cachedData.StringSet("channel:" + channel.Id.ToString(), new DiscordChannel((SocketNewsChannel)fullChannel).ToString());
                else if (fullChannel.GetType() == typeof(SocketTextChannel))
                    cachedData.StringSet("channel:" + channel.Id.ToString(), new DiscordChannel((SocketTextChannel)fullChannel).ToString());
                else if (fullChannel.GetType() == typeof(SocketVoiceChannel))
                    cachedData.StringSet("channel:" + channel.Id.ToString(), new DiscordChannel((SocketVoiceChannel)fullChannel).ToString());
                else
                    cachedData.StringSet("channel:" + channel.Id.ToString(), new DiscordChannel(fullChannel).ToString());
            }

            foreach (var user in newGuild.Users)
            {
                var serializedUser = new DiscordUser(user).ToString();
                cachedData.StringSet("user:" + newGuild.Id.ToString() + ":" + user.Id.ToString(), serializedUser);
            }

            string features = "";
            foreach (var feature in newGuild.Features)
                features += feature + ";";
            cachedData.StringSet("feature:" + newGuild.Id.ToString(), features);

            foreach (var role in newGuild.Roles)
            {
                var serializedRole = new DiscordRole(role).ToString();
                cachedData.StringSet("role:" + newGuild.Id.ToString() + ":" + role.Id.ToString(), serializedRole);
            }

            foreach (var emote in newGuild.Emotes)
            {
                var serializedEmote = new DiscordEmote(emote).ToString();
                cachedData.StringSet("emote:" + newGuild.Id.ToString() + ":" + emote.Id.ToString(), serializedEmote);
            }

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
            var fullChannel = discordClient.GetChannel(channel.Id);
            if (fullChannel.GetType() == typeof(SocketCategoryChannel))
                cachedData.StringSet("channel:" + channel.Id.ToString(), new DiscordChannel((SocketCategoryChannel)fullChannel).ToString());
            else if (fullChannel.GetType() == typeof(SocketDMChannel))
                cachedData.StringSet("channel:" + channel.Id.ToString(), new DiscordChannel((SocketDMChannel)fullChannel).ToString());
            else if (fullChannel.GetType() == typeof(SocketGroupChannel))
                cachedData.StringSet("channel:" + channel.Id.ToString(), new DiscordChannel((SocketGroupChannel)fullChannel).ToString());
            else if (fullChannel.GetType() == typeof(SocketGuildChannel))
                cachedData.StringSet("channel:" + channel.Id.ToString(), new DiscordChannel((SocketGuildChannel)fullChannel).ToString());
            else if (fullChannel.GetType() == typeof(SocketNewsChannel))
                cachedData.StringSet("channel:" + channel.Id.ToString(), new DiscordChannel((SocketNewsChannel)fullChannel).ToString());
            else if (fullChannel.GetType() == typeof(SocketTextChannel))
                cachedData.StringSet("channel:" + channel.Id.ToString(), new DiscordChannel((SocketTextChannel)fullChannel).ToString());
            else if (fullChannel.GetType() == typeof(SocketVoiceChannel))
                cachedData.StringSet("channel:" + channel.Id.ToString(), new DiscordChannel((SocketVoiceChannel)fullChannel).ToString());
            else
                cachedData.StringSet("channel:" + channel.Id.ToString(), new DiscordChannel(fullChannel).ToString());

            foreach(var user in channel.Users)
                cachedData.StringSet("channel_users:" + channel.Id.ToString() + ":" + user.Id.ToString(), new DiscordChannel(fullChannel).ToString());

            return SendEvent("channel", "ChannelCreated", "DNetBot.Channel.Created", "channel:" + channel.Id.ToString());
        }

        // Handles actions for when a guild updates a channel
        private Task ChannelUpdated(SocketChannel oldChannel, SocketChannel newChannel)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Channel", "Channel Updated: " + newChannel.Id);
            var fullChannel = discordClient.GetChannel(newChannel.Id);
            if (fullChannel.GetType() == typeof(SocketCategoryChannel))
                cachedData.StringSet("channel:" + newChannel.Id.ToString(), new DiscordChannel((SocketCategoryChannel)fullChannel).ToString());
            else if (fullChannel.GetType() == typeof(SocketDMChannel))
                cachedData.StringSet("channel:" + newChannel.Id.ToString(), new DiscordChannel((SocketDMChannel)fullChannel).ToString());
            else if (fullChannel.GetType() == typeof(SocketGroupChannel))
                cachedData.StringSet("channel:" + newChannel.Id.ToString(), new DiscordChannel((SocketGroupChannel)fullChannel).ToString());
            else if (fullChannel.GetType() == typeof(SocketGuildChannel))
                cachedData.StringSet("channel:" + newChannel.Id.ToString(), new DiscordChannel((SocketGuildChannel)fullChannel).ToString());
            else if (fullChannel.GetType() == typeof(SocketNewsChannel))
                cachedData.StringSet("channel:" + newChannel.Id.ToString(), new DiscordChannel((SocketNewsChannel)fullChannel).ToString());
            else if (fullChannel.GetType() == typeof(SocketTextChannel))
                cachedData.StringSet("channel:" + newChannel.Id.ToString(), new DiscordChannel((SocketTextChannel)fullChannel).ToString());
            else if (fullChannel.GetType() == typeof(SocketVoiceChannel))
                cachedData.StringSet("channel:" + newChannel.Id.ToString(), new DiscordChannel((SocketVoiceChannel)fullChannel).ToString());
            else
                cachedData.StringSet("channel:" + newChannel.Id.ToString(), new DiscordChannel(fullChannel).ToString());

            foreach (var user in newChannel.Users)
                cachedData.StringSet("channel_users:" + newChannel.Id.ToString() + ":" + user.Id.ToString(), new DiscordChannel(fullChannel).ToString());

            return SendEvent("channel", "ChannelUpdated", "DNetBot.Channel.Updated", "channel:" + newChannel.Id.ToString());
        }

        // Handles actions for when a guild deletes a channel
        private Task ChannelDeleted(SocketChannel channel)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Channel", "Channel Deleted: " + channel.Id);
            cachedData.KeyDelete("channel:" + channel.Id.ToString());

            foreach (var user in channel.Users)
                cachedData.KeyDelete("channel_users:" + channel.Id.ToString() + ":" + user.Id.ToString());

            return SendEvent("channel", "ChannelDeleted", "DNetBot.Channel.Deleted", "channel:" + channel.Id.ToString());
        }

        // Handles actions for when a user joins a channel
        private Task ChannelJoined(SocketGroupUser user)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Channel", "User: " + user.Id + " Joined Channel: " + user.Channel.Id);
            var fullChannel = discordClient.GetChannel(user.Channel.Id);
            if (fullChannel.GetType() == typeof(SocketCategoryChannel))
                cachedData.StringSet("channel_users:" + fullChannel.Id.ToString() + ":" + user.Id.ToString(), new DiscordChannel((SocketCategoryChannel)fullChannel).ToString());
            else if (fullChannel.GetType() == typeof(SocketDMChannel))
                cachedData.StringSet("channel_users:" + fullChannel.Id.ToString() + ":" + user.Id.ToString(), new DiscordChannel((SocketDMChannel)fullChannel).ToString());
            else if (fullChannel.GetType() == typeof(SocketGroupChannel))
                cachedData.StringSet("channel_users:" + fullChannel.Id.ToString() + ":" + user.Id.ToString(), new DiscordChannel((SocketGroupChannel)fullChannel).ToString());
            else if (fullChannel.GetType() == typeof(SocketGuildChannel))
                cachedData.StringSet("channel_users:" + fullChannel.Id.ToString() + ":" + user.Id.ToString(), new DiscordChannel((SocketGuildChannel)fullChannel).ToString());
            else if (fullChannel.GetType() == typeof(SocketNewsChannel))
                cachedData.StringSet("channel_users:" + fullChannel.Id.ToString() + ":" + user.Id.ToString(), new DiscordChannel((SocketNewsChannel)fullChannel).ToString());
            else if (fullChannel.GetType() == typeof(SocketTextChannel))
                cachedData.StringSet("channel_users:" + fullChannel.Id.ToString() + ":" + user.Id.ToString(), new DiscordChannel((SocketTextChannel)fullChannel).ToString());
            else if (fullChannel.GetType() == typeof(SocketVoiceChannel))
                cachedData.StringSet("channel_users:" + fullChannel.Id.ToString() + ":" + user.Id.ToString(), new DiscordChannel((SocketVoiceChannel)fullChannel).ToString());
            else
                cachedData.StringSet("channel_users:" + fullChannel.Id.ToString() + ":" + user.Id.ToString(), new DiscordChannel(fullChannel).ToString());

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

            foreach (var member in role.Members)
            {
                var serializedMember = new DiscordUser(member).ToString();
                cachedData.StringSet("role_members:" + role.Id.ToString() + ":" + member.Id.ToString(), serializedMember);
            }

            return SendEvent("role", "RoleCreated", "DNetBot.Role.Created", "role:" + role.Id.ToString());
        }

        // Handles actions for when a guild updates a role
        private Task RoleUpdated(SocketRole oldRole, SocketRole newRole)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Role", "Role Updated: " + newRole.Id);
            var serializedRole = new DiscordRole(newRole).ToString();
            cachedData.StringSet("role:" + newRole.Id.ToString(), serializedRole);

            foreach (var member in newRole.Members)
            {
                var serializedMember = new DiscordUser(member).ToString();
                cachedData.StringSet("role_members:" + newRole.Id.ToString() + ":" + member.Id.ToString(), serializedMember);
            }

            return SendEvent("role", "RoleUpdated", "DNetBot.Role.Updated", "role:" + newRole.Id.ToString());
        }

        // Handles actions for when a guild deletes a role
        private Task RoleDeleted(SocketRole role)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Role", "Role Deleted: " + role.Id);
            cachedData.KeyDelete("role:" + role.Id.ToString());

            foreach (var member in role.Members)
            {
                cachedData.KeyDelete("role_members:" + role.Id.ToString() + ":" + member.Id.ToString());
            }

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
