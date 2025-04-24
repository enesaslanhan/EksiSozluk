using BlazorSozluk.Api.Domain.Models;
using BlazorSozluk.Common.Infrastructure;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Infrastructure.Persistence.Context
{
    internal class SeedData
    {
        private static List<User> GetUsers()
        {
            var result = new Faker<User>("tr")
                .RuleFor(i => i.Id, i => Guid.NewGuid())
                .RuleFor(i => i.CreatedDate, i => i.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(i => i.FirstName, i => i.Person.FirstName)
                .RuleFor(i => i.LastName, i => i.Person.LastName)
                .RuleFor(i => i.Email, i => i.Internet.Email())
                .RuleFor(i => i.UserName, i => i.Internet.UserName())
                .RuleFor(i => i.Password, i =>PasswordEncryptor.Encrpt(i.Internet.Password()))
                .RuleFor(i => i.EmailConfirmed, i => i.PickRandom(true, false))
                .Generate(500);
            return result;
        }
        public async Task SeedAsync(IConfiguration configuration)
        {
            var dbContextBuilder = new DbContextOptionsBuilder();
            dbContextBuilder.UseSqlServer(configuration["BlazorSozlukDbConnectionString"]);

            var context=new BlazorSozlukContext(dbContextBuilder.Options);

            var users = GetUsers();
            var userIds=users.Select(i=>i.Id).ToList();

            await context.Users.AddRangeAsync(users);

            var guids = Enumerable.Range(0, 150).Select(i => Guid.NewGuid()).ToList();
            int counter = 0;

            var entries = new Faker<Entry>("tr")
                .RuleFor(i => i.Id,i=> guids[counter++])
                .RuleFor(i => i.CreatedDate, i => i.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(i => i.Subject, i => i.Lorem.Sentence(5, 15))
                .RuleFor(i => i.Content, i => i.Lorem.Paragraph(2))
                .RuleFor(i => i.CreatedById, i => i.PickRandom(userIds))
                .Generate(150);

            await context.Entries.AddRangeAsync(entries);

            var coments = new Faker<EntryComment>("tr")
                .RuleFor(i => i.Id, i=>Guid.NewGuid())
                .RuleFor(i => i.CreatedDate, i => i.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(i => i.Content, i => i.Lorem.Paragraph(2))
                .RuleFor(i => i.CreatedById, i => i.PickRandom(userIds))
                .RuleFor(i => i.EnrtyId, i => i.PickRandom(guids))
                .Generate(1000);
            await context.EnrtyComments.AddRangeAsync(coments);

            await context.SaveChangesAsync();
        }
    }
}
