using System;
using System.Linq;
using AutoMapper;
using BLL.Models;
using BLL.Models.AuthorizationViewModel;
using DAL.Models;
using DAL.Models.AuthorizationModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

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

            CreateMap<EditNewsPaperViewModel, NewsPaper>();
            CreateMap<NewsPaperViewModel, NewsPaper>();
            CreateMap<CreateNewsPaperViewModel, NewsPaper>()
                .ForMember(q => q.DateInsert, x => x.ResolveUsing(c => DateTime.Now))
                .ForMember(item => item.Id, opt => opt.Ignore());

            CreateMap<NewsPaper, NewsPaperViewModel>();
            CreateMap<NewsPaper, EditNewsPaperViewModel>()
              .ForMember(item => item.ListPublichHouse, opt => opt.Ignore());
            CreateMap<NewsPaper, CreateNewsPaperViewModel>()
                .ForMember(item => item.ListPublichHouse, opt => opt.Ignore());


            CreateMap<PaperPublishHouseViewModel, PaperPublishHouses>();
            CreateMap<CreatePaperPublishHousViewModel, PaperPublishHouses>()
                .ForMember(item => item.Id, opt => opt.Ignore());
            CreateMap<EditPaperPublishHouseViewModel, PaperPublishHouses>()
               .ForMember(item => item.NewsPapers, opt => opt.Ignore());


            CreateMap<PaperPublishHouses, PaperPublishHouseViewModel>();
            CreateMap<PaperPublishHouses, EditPaperPublishHouseViewModel>();
            CreateMap<PaperPublishHouses, CreatePaperPublishHousViewModel>();

            CreateMap<CreateBookViewModel, Book>()
                .ForMember(item => item.Id, opt => opt.Ignore());
            CreateMap<EditBookViewModel, Book>();
            CreateMap<BookViewModel, Book>();

            CreateMap<Book, CreateBookViewModel>()
                .ForMember(item => item.ListGeners, opt => opt.Ignore());
            CreateMap<Book, EditBookViewModel>()
                .ForMember(item => item.ListGeners, opt=>opt.Ignore());
            CreateMap<Book, BookViewModel>();

            CreateMap<Gener, GenerViewModel>();
            CreateMap<Gener, CreateGenerViewModel>();
            CreateMap<Gener, EditGenerViewModel>();

            CreateMap<GenerViewModel, Gener>();
            CreateMap<CreateGenerViewModel, Gener>()
                 .ForMember(item => item.Id, opt => opt.Ignore())
                 .ForMember(item => item.Books, opt => opt.Ignore());
            CreateMap<EditGenerViewModel, Gener>()
                .ForMember(item => item.Books, opt => opt.Ignore());

            CreateMap<RegisterViewModel, ApplicationUser>()
                .ForMember(item=>item.Email, opt=>opt.MapFrom(src=>src.Email))
                .ForMember(item => item.UserName, opt => opt.MapFrom(src => src.Email))
                .ForAllOtherMembers(opt=>opt.Ignore());

            CreateMap<BookViewModel, BookAndPaperViewModel>()
                .ForMember(item => item.Type, opt => opt.UseValue("Book"))
                .ForMember(item => item.Check, opt => opt.UseValue(false));

            CreateMap<NewsPaperViewModel, BookAndPaperViewModel>()
                .ForMember(item => item.Type, opt => opt.UseValue("News Paper"))
                .ForMember(item => item.Author, opt => opt.MapFrom(src => src.PublishHouse))
                .ForMember(item => item.YearOfPublish, opt => opt.MapFrom(src => src.PrindDate))
                .ForMember(item => item.Check, opt => opt.UseValue(false));
        }
    }
}