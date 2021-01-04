using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace DNetBotFunctions.Clients.TableEntities
{
    public class ChannelEntity : TableEntity
    {
        public ChannelEntity(ulong GuildID, ulong channel)
        {
            PartitionKey = GuildID.ToString();
            RowKey = "Channel." + channel.ToString();
        }
    }
}
