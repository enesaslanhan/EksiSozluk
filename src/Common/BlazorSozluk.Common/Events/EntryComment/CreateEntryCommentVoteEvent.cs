﻿using BlazorSozluk.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Common.Events.EntryComment
{
    public class CreateEntryCommentVoteEvent
    {
        public Guid EntryCommentId { get; set; }
        public Guid CreateBy { get; set; }
        public VoteType VoteType { get; set; }
    }
}
