using Discord;
using Discord.WebSocket;
using DNetBot.Helpers;
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

            return Task.CompletedTask;
        }

        // Handles the bot being initally removed from a guild / server
        private Task GuildLeave(SocketGuild guild)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Guild", "Left Guild: " + guild.Id);

            return Task.CompletedTask;
        }
        #endregion

        #region Server Update Events
        // Handles any actions required when a guild is ready to be interacted (After a reset or disconnect)
        private Task GuildAvailable(SocketGuild guild)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Guild", "Guild Became Available: " + guild.Id);

            return Task.CompletedTask;
        }

        // Handles any actions required if the guild updates it's details
        private Task GuildUpdate(SocketGuild oldGuild, SocketGuild newGuild)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Guild", "Guild Updated: " + newGuild.Id);

            return Task.CompletedTask;
        }

        // Handles processes for when a guild becomes unavailable
        private Task GuildUnAvailable(SocketGuild guild)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Guild", "Guild Became Unavailable: " + guild.Id);

            return Task.CompletedTask;
        }
        #endregion

        #region Server Channel Events
        // Handles processes for when a guild adds a role
        private Task ChannelCreated(SocketChannel channel)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Channel", "Channel Created: " + channel.Id);

            return Task.CompletedTask;
        }

        // Handles actions for when a guild updates a role
        private Task ChannelUpdated(SocketChannel oldChannel, SocketChannel newChannel)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Channel", "Channel Updated: " + newChannel.Id);

            return Task.CompletedTask;
        }

        // Handles actions for when a guild deletes a role
        private Task ChannelDeleted(SocketChannel channel)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Channel", "Channel Deleted: " + channel.Id);

            return Task.CompletedTask;
        }

        // Handles actions for when a guild deletes a role
        private Task ChannelJoined(SocketGroupUser user)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Channel", "User: " + user.Id + " Joined Channel: " + user.Channel.Id);

            return Task.CompletedTask;
        }

        // Handles actions for when a guild deletes a role
        private Task ChannelLeft(SocketGroupUser user)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Channel", "User: " + user.Id + " Left Channel: " + user.Channel.Id);

            return Task.CompletedTask;
        }
        #endregion

        #region Server Role Events
        // Handles processes for when a guild adds a role
        private Task RoleCreated(SocketRole role)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Role", "Role Created: " + role.Id);

            return Task.CompletedTask;
        }

        // Handles actions for when a guild updates a role
        private Task RoleUpdated(SocketRole oldRole, SocketRole newRole)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Role", "Role Updated: " + newRole.Id);

            return Task.CompletedTask;
        }

        // Handles actions for when a guild deletes a role
        private Task RoleDeleted(SocketRole role)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Role", "Role Deleted: " + role.Id);

            return Task.CompletedTask;
        }
        #endregion

        #region User Information Update Events
        private Task UserBan(SocketUser user, SocketGuild guild)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Guild", "User: " + user.Id + " Banned From: " + guild.Id);


            return Task.CompletedTask;
        }


        private Task UserUnban(SocketUser user, SocketGuild guild)
        {
            Formatter.GenerateLog(_logger, LogSeverity.Info, "Guild", "User: " + user.Id + " Unbanned From: " + guild.Id);

            return Task.CompletedTask;
        }
        #endregion
    }
}
