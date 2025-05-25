using BlazorSozluk.Common.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Common.Models.RequestModels
{
    public class CreateEntryCommentVoteCommand:IRequest<bool>
    {
        public CreateEntryCommentVoteCommand(Guid entryCommentId, Guid createBy, VoteType voteType)
        {
            EntryCommentId = entryCommentId;
            CreateBy = createBy;
            VoteType = voteType;
        }
        public CreateEntryCommentVoteCommand()
        {
            
        }

        public Guid EntryCommentId { get; set; }
        public Guid CreateBy { get; set; }
        public VoteType VoteType { get; set; }
    }
}
