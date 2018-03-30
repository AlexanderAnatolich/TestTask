using System;
using System.Linq;
using AutoMapper;
using Model.Models;
using Model.Models.AuthorizationViewModel;
using DAL.Models;
using DAL.Models.AuthorizationModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using DAL.DTO;
using System.Collections.Generic;

namespace BLL.AutoMapperProfile
{
    public static class MapperProfile
    {
        public static void InitAutoMapper()
        {
            Mapper.Initialize(item =>
            {
                item.AddProfile<BaseModelProfile>();
            });
            Mapper.AssertConfigurationIsValid();
        }
    }
    
    public class BaseModelProfile : Profile
    {
        
        public BaseModelProfile()
        {

            CreateMap<NewsPaperViewModel, NewsPaper>();
            CreateMap<NewsPaper, NewsPaperViewModel>();
           
            CreateMap<CreatePublishHousViewModel, PublishHouse>()
                .ForMember(item => item.Id, opt => opt.Ignore()); 
            CreateMap<PublishHouse, PublishHouseViewModel>();
            CreateMap<PublishHouse, CreatePublishHousViewModel>();
            CreateMap<PublishHouseViewModel, PublishHouse>();

            CreateMap<BookDTO, BookViewModel>()
                .ForMember(item => item.Genre, opt => opt.MapFrom(gh=>gh.Genre))
                .ForMember(item => item.PublishHouse, opt => opt.MapFrom(gh => gh.PublishHouse));           
            CreateMap<CreateBookViewModel, BookDTO>()
                .ForMember(item => item.Genre, opt => opt.MapFrom(gh => gh.Genre))
                .ForMember(item => item.PublishHouse, opt => opt.MapFrom(gh => gh.PublishHouse))
                .ForMember(item=>item.Id,opt=>opt.Ignore());

            CreateMap<NewsPaperDTO, NewsPaperViewModel>()
                .ForMember(item => item.PublishHouse_Id, opt => opt.MapFrom(gh => gh.PublishHouse.Id))
                .ForMember(item => item.PublishHouse, opt => opt.MapFrom(gh => gh.PublishHouse));

            CreateMap<BookViewModel, Book>()
                .ForMember(item => item.PublishHouseId, opt => opt.MapFrom(src => src.PublishHouse.Id));
            CreateMap<Book, BookViewModel>()
                .ForMember(item => item.Genre, opt => opt.Ignore());
            CreateMap<CreateBookViewModel, Book>()
                .ForMember(item => item.Id, opt => opt.Ignore())
                .ForMember(item => item.PublishHouseId, opt => opt.MapFrom(src => src.PublishHouse.Id))
                .ForMember(item => item.PublishHouse, opt => opt.Ignore());

            CreateMap<Gener, GenerViewModel>()
                .ForMember(item => item.Genre, opt => opt.MapFrom(src => src.Genre));
            CreateMap<Gener, CreateGenerViewModel>();
            CreateMap<GenerViewModel, Gener>()
                .ForMember(item => item.Genre, opt => opt.MapFrom(src => src.Genre));
            CreateMap<CreateGenerViewModel, Gener>()
                 .ForMember(item => item.Id, opt => opt.Ignore());

            CreateMap<RegisterViewModel, ApplicationUser>()
                .ForMember(item=>item.Email, opt=>opt.MapFrom(src=>src.Email))
                .ForMember(item => item.UserName, opt => opt.MapFrom(src => src.Email))
                .ForAllOtherMembers(opt=>opt.Ignore());

            CreateMap<CreateNewsPaperViewModel, NewsPaper>()
                 .ForMember(item => item.Id, opt => opt.Ignore())
                 .ForMember(item => item.PublishHouseId, opt => opt.MapFrom(src => src.PublishHouse.Id));

            CreateMap<BookViewModel, SummaryViewModel>()
                .ForMember(item => item.Type, opt => opt.UseValue("Book"))
                .ForMember(item => item.Check, opt => opt.UseValue(false));
            CreateMap<NewsPaperViewModel, SummaryViewModel>()
                .ForMember(item => item.Type, opt => opt.UseValue("News Paper"))
                .ForMember(item => item.Author, opt => opt.UseValue("-"))
                .ForMember(item => item.YearOfPublish, opt => opt.MapFrom(src => src.PrintDate))
                .ForMember(item => item.Check, opt => opt.UseValue(false));

            CreateMap<CreateJournalViewModel, Journal>()
                .ForMember(item => item.Id, opt => opt.Ignore())
                .ForMember(item => item.PublishHouseId, opt => opt.MapFrom(src => src.PublishHouse.Id))
                .ForMember(item => item.PublishHouse, opt => opt.Ignore());

            CreateMap<JournalViewModel, Journal>();
            CreateMap<Journal, JournalViewModel>();

            CreateMap<JournalDTO, JournalViewModel>();

        }
    }
}