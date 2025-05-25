using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Common
{
    public class SozlukContants
    {
        public const string RabbitMQHost = "localhost";
        public const string DefaultExchangeType = "direct";


        public const string UserExchangedName = "UserExchanged";
        public const string UserEmailExchangedQueueName = "UserEmailExchangedQueue";

        public const string FavExchangedName = "FavExchanged";
        public const string CreateEntryFavQueueName = "CreateEntryFavQueue";
        public const string CreateEntryCommentFavQueueName = "CreateEntryCommentFavQueue";

        public const string VoteExchangedName = "VoteExchanged";
        public const string CreateEntryVoteQueueName = "CreateEntryVoteQueue";
        public const string DeleteEntryFavQueueName = "DeleteEntryFavQueue";
        public const string DeleteEntryVoteQueueName = "DeleteEntryVoteQueue";

        public const string CreateEntryCommentVoteQueueName = "CreateEntryCommentVoteQueue";
        public const string DeleteEntryCommentFavQueueName = "DeleteEntryCommentFavQueue";
        public const string DeleteEntryCommentVoteQueueName = "DeleteEntryCommentVoteQueue";
    }
}
