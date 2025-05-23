﻿using BlazorSozluk.Api.Domain.Models;
using BlazorSozluk.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Infrastructure.Persistence.EntityConfigurations
{
    public class EmailConfirmationEntityConfiguration:BaseEntityConfiguration<Api.Domain.Models.EmailComfirmation>
    {
        public override void Configure(EntityTypeBuilder<EmailComfirmation> builder)
        {
            base.Configure(builder);
            builder.ToTable("emailConfirmation", BlazorSozlukContext.DEFAULT_SCHEMA);
        }
    }
}
