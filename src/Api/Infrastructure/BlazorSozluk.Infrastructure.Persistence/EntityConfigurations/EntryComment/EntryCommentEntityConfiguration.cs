using BlazorSozluk.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Infrastructure.Persistence.EntityConfigurations.EntryComment
{
    public class EntryCommentEntityConfiguration:BaseEntityConfiguration<Api.Domain.Models.EntryComment>
    {
        public override void Configure(EntityTypeBuilder<Api.Domain.Models.EntryComment> builder)
        {
            base.Configure(builder);
            builder.ToTable("entryComment", BlazorSozlukContext.DEFAULT_SCHEMA);
            builder.HasOne(e => e.CreatedBy)
                    .WithMany(e => e.EntryComments)
                    .HasForeignKey(e => e.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Entry)
                    .WithMany(e => e.EntryComments)
                    .HasForeignKey(e => e.EnrtyId);
        }
    }
}
