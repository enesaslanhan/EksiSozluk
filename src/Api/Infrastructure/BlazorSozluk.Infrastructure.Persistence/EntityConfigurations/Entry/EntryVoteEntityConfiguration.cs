﻿using BlazorSozluk.Api.Domain.Models;
using BlazorSozluk.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Infrastructure.Persistence.EntityConfigurations.Entry
{
    public class EntryVoteEntityConfiguration:BaseEntityConfiguration<Api.Domain.Models.EntryVote>
    {
        public override void Configure(EntityTypeBuilder<EntryVote> builder)
        {
            base.Configure(builder);
            builder.ToTable("entryVote", BlazorSozlukContext.DEFAULT_SCHEMA);
            builder.HasOne(e => e.Entry)
                    .WithMany(e => e.EntryVotes)
                    .HasForeignKey(e => e.EntryId);
        }
    }
}
