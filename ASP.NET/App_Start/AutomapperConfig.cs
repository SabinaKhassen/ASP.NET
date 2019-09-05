using ASP.NET.Models;
using AutoMapper;
using BussinessLayer.BussinessObjects;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;

namespace ASP.NET.App_Start
{
    public static class AutomapperConfig
    {
        public static void RegisterWithUnity(IUnityContainer container)
        {
            IMapper mapper = CreateMapperConfig().CreateMapper();

            container.RegisterInstance<IMapper>(mapper);
        }

        public static MapperConfiguration CreateMapperConfig()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Authors, AuthorBO>().
                ConstructUsing(item => DependencyResolver.Current.GetService<AuthorBO>());

                cfg.CreateMap<AuthorBO, Authors>().
                ConstructUsing(item => DependencyResolver.Current.GetService<Authors>());

                cfg.CreateMap<AuthorViewModel, AuthorBO>().
                ConstructUsing(item => DependencyResolver.Current.GetService<AuthorBO>());

                cfg.CreateMap<AuthorBO, AuthorViewModel>().
                ConstructUsing(item => DependencyResolver.Current.GetService<AuthorViewModel>());


                cfg.CreateMap<Books, BookBO>().
                ConstructUsing(item => DependencyResolver.Current.GetService<BookBO>());

                cfg.CreateMap<BookBO, Books>().
                ConstructUsing(item => DependencyResolver.Current.GetService<Books>());

                cfg.CreateMap<BookViewModel, BookBO>().
                ConstructUsing(item => DependencyResolver.Current.GetService<BookBO>());

                cfg.CreateMap<BookBO, BookViewModel>().
                ConstructUsing(item => DependencyResolver.Current.GetService<BookViewModel>());


                cfg.CreateMap<Users, UserBO>().
                ConstructUsing(item => DependencyResolver.Current.GetService<UserBO>());

                cfg.CreateMap<UserBO, Users>().
                ConstructUsing(item => DependencyResolver.Current.GetService<Users>());

                cfg.CreateMap<UserViewModel, UserBO>().
                ConstructUsing(item => DependencyResolver.Current.GetService<UserBO>());

                cfg.CreateMap<UserBO, UserViewModel>().
                ConstructUsing(item => DependencyResolver.Current.GetService<UserViewModel>());


                cfg.CreateMap<UserBookLinks, UserBookLinkBO>().
                ConstructUsing(item => DependencyResolver.Current.GetService<UserBookLinkBO>());

                cfg.CreateMap<UserBookLinkBO, UserBookLinks>().
                ConstructUsing(item => DependencyResolver.Current.GetService<UserBookLinks>());

                cfg.CreateMap<UserBookLinkViewModel, UserBookLinkBO>().
                ConstructUsing(item => DependencyResolver.Current.GetService<UserBookLinkBO>());

                cfg.CreateMap<UserBookLinkBO, UserBookLinkViewModel>().
                ConstructUsing(item => DependencyResolver.Current.GetService<UserBookLinkViewModel>());
            });
        }
    }
}