using AutoMapper;
using BlazorSozluk.Api.Application.Features.Queries.GetEntries;
using BlazorSozluk.Api.Domain.Models;
using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.Common.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            #region UserMapping
            CreateMap<User, LoginUserViewModel>().ReverseMap();
            CreateMap<User, CreateUserCommand>().ReverseMap();
            CreateMap<User, UpdateUserCommand>().ReverseMap();
            #endregion
            #region EntryMapping
            CreateMap<CreateEntryCommand,Entry>().ReverseMap();
            #endregion
            #region EntryComment
            CreateMap<CreateEntryCommentCommand, EntryComment>().ReverseMap();
            CreateMap<Entry,GetEntiresViewModel>().ForMember(x=>x.CommentCount,y=>y.MapFrom(z=>z.EntryComments.Count));
            #endregion

        }
    }
}
