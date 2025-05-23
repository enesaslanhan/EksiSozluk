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
    public class EntryFavoriteEntityConfiguration:BaseEntityConfiguration<Api.Domain.Models.EntryFavorite>
    {
        public override void Configure(EntityTypeBuilder<EntryFavorite> builder)
        {
            base.Configure(builder);
            builder.ToTable("entryFavorite", BlazorSozlukContext.DEFAULT_SCHEMA);
            builder.HasOne(e => e.Entry)
                    .WithMany(e => e.EntryFavorites)
                    .HasForeignKey(e => e.EntryId);

            builder.HasOne(e => e.CreatedUser)
                .WithMany(e => e.EntryFavorites)
                .HasForeignKey(e => e.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
