using Application.Books.Commands.CreateBook;
using Application.Books.Commands.UpdateBook;
using Application.Books.Queries.GetBookById;
using Application.Books.Queries.GetFilteredBooks;
using Application.UserManagers.Commands.CreateUser;
using Application.UserManagers.Commands.EditPhoneNumber;
using Application.UserManagers.Commands.EditRegisteredUser;
using Application.UserManagers.Commands.EditUser;
using Application.UserManagers.Commands.VerifyRegisteration;
using AutoMapper;
using Domain.Books;
using Domain.Common.Mappings;
using Domain.MemberProfiles;
using Domain.Users;
using System;
using System.Linq;
using System.Reflection;

namespace Application_Backend.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

            #region --------------  Entities  ---------------------




            CreateMap<CreateBookCommand, Book>();
            CreateMap<UpdateBookCommand, Book>();
            CreateMap<Book, BookDto>();
            CreateMap<Book, FilteredBooksDto>();

       


            #endregion


            #region --------------  files  ---------------------

            //CreateMap<NewsFile, NewsFileDto>();
            //CreateMap<NewsFile, FilteredNewsFileDto>();

            #endregion

            #region --------------  membership  ---------------------

            CreateMap<EditUserCommand, UserInputDto>();
            CreateMap<EditProfileUserCommand, UserInputDto>();
            CreateMap<EditPhoneNumberCommand, UserInputDto>();
            CreateMap<EditProfileUserCommand, MemberProfile>();
            CreateMap<EditPhoneNumberCommand, MemberProfile>();


            #endregion


            #region ---------------  MemberRProfiles  -----------------
            CreateMap<VerifyRegisterationCommand, MemberProfile>()
                .ForMember(a => a.UserName, opt => opt.MapFrom(p => p.PhoneNumber));
            CreateMap<CreateUserCommand, MemberProfile>();
            CreateMap<EditUserCommand, MemberProfile>()
                .ForMember(a => a.Id, opt => opt.Ignore());
            CreateMap<EditProfileUserCommand, MemberProfile>()
               .ForMember(a => a.Id, opt => opt.Ignore());

            #endregion


        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod("Mapping")
                    ?? type.GetInterface("IMapFrom`1").GetMethod("Mapping");

                methodInfo?.Invoke(instance, new object[] { this });

            }

            types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapTo<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod("Mapping")
                    ?? type.GetInterface("IMapTo`1").GetMethod("Mapping");

                methodInfo?.Invoke(instance, new object[] { this });

            }
        }

    }
}