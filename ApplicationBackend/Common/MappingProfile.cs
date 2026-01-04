using Application.AdvanceSearch.Queries.SearchRuleVersion;
using Application.Cartable.Commands.CreateCartable;
using Application.Cartable.Commands.UpdateCartable;
using Application.Cartable.Queries.GetCartableById;
using Application.Cartable.Queries.GetFilteredCartables;
using Application.Cartable.Queries.GetMyCartables;
using Application.Footers.Commands.CreateFooter;
using Application.Footers.Commands.UpdateFooter;
using Application.Footers.Queries.GetFilteredFooters;
using Application.Footers.Queries.GetFooter;
using Application.Headers.Commands.CreateHeader;
using Application.Headers.Commands.UpdateHeader;
using Application.Headers.Queries.GetFilteredHeaders;
using Application.Headers.Queries.GetHeader;
using Application.SeoFiles.Commands.StoreSeoFile;
using Application.SeoFiles.Queries.GetFilteredSeoFiles;
using Application.SeoFiles.Queries.GetSeoFileById;
using Application.Sliders.Commands.CreateSlider;
using Application.Sliders.Commands.UpdateSlider;
using Application.Sliders.Queries.GetFilteredSliders;
using Application.Sliders.Queries.GetSlider;
using Application.UserManagers.Commands.CreateUser;
using Application.UserManagers.Commands.EditPhoneNumber;
using Application.UserManagers.Commands.EditRegisteredUser;
using Application.UserManagers.Commands.EditUser;
using Application.UserManagers.Commands.VerifyOtpLogin;
using Application.UserManagers.Commands.VerifyRegisteration;
using AutoMapper;
using Domain.AdvanceSearchs;
using Domain.Common.Mappings;
using Domain.Externals.CMRServer.Rules;
using Domain.Externals.MavaServer.Rules;
using Domain.Externals.NotifyServer.EmailNotifications;
using Domain.Footers;
using Domain.Headers;
using Domain.MemberProfiles;
using Domain.SeoFiles;
using Domain.SliderFiles;
using Domain.Sliders;
using Domain.Users;
using Extensions;
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

            CreateMap<Domain.Cartables.Cartable, FilteredCartablesDto>()
                 .ForMember(a => a.Users, opt => opt.MapFrom(p => p.Users));
            CreateMap<Domain.Cartables.Cartable, GetMyCartablesDto>();

            CreateMap<Domain.Cartables.Cartable, CartableDto>()
              .ForMember(a => a.Users, opt => opt.MapFrom(p => p.Users));
            CreateMap<CreateCartableCommand, Domain.Cartables.Cartable>();
            CreateMap<UpdateCartableCommand, Domain.Cartables.Cartable>();
            CreateMap<MemberProfile, FiltterdCartableProfileDto>();
            CreateMap<MemberProfile, CartableProfileDto>();




            #endregion


            #region --------------  membership  ---------------------

            CreateMap<EditUserCommand, UserInputDto>();
            CreateMap<EditProfileUserCommand, UserInputDto>();
            CreateMap<EditPhoneNumberCommand, UserInputDto>();
            CreateMap<EditProfileUserCommand, MemberProfile>();
            CreateMap<EditPhoneNumberCommand, MemberProfile>();
            #endregion

            #region ---------------  Profiles  -----------------
            CreateMap<VerifyRegisterationCommand, MemberProfile>()
                .ForMember(a => a.UserName, opt => opt.MapFrom(p => p.PhoneNumber));
            CreateMap<VerifyOtpLoginCommand, MemberProfile>()
                .ForMember(a => a.UserName, opt => opt.MapFrom(p => p.PhoneNumber));
            CreateMap<CreateUserCommand, MemberProfile>();
            CreateMap<EditUserCommand, MemberProfile>()
                .ForMember(a => a.Id, opt => opt.Ignore());
            CreateMap<EditProfileUserCommand, MemberProfile>()
               .ForMember(a => a.Id, opt => opt.Ignore());

            #endregion

            CreateMap<CreateFooterLinkCommand, FooterLink>();
            CreateMap<UpdateFooterLinkCommand, FooterLink>();

            CreateMap<CreateFooterCommand, Footer>();
            CreateMap<UpdateFooterCommand, Footer>();
            CreateMap<Footer, FilteredFootersDto>();
            CreateMap<FooterLink, FilteredFootersLinkDto>();
            CreateMap<Footer, FooterDto>();
            CreateMap<FooterLink, FootersLinkDto>();

            CreateMap<CreateHeaderCommand, Header>();
            CreateMap<UpdateHeaderCommand, Header>();
            CreateMap<Header, FilteredHeadersDto>();
            CreateMap<Header, HeaderDto>();

           

            #region ---------------  AdvanceSearch  -----------------


            //     CreateMap<FullTextResultDto, SearchRuleVersionDto>();
            CreateMap<FullTextResultDto, SearchRuleVersionDto>();



            CreateMap<SearchRuleVersionQuery, AdvanceSearchInputDto>();
            CreateMap<ColumnSortQuery, ColumnSort>();
            CreateMap<FullTextSearchRuleVersionQuery, AdvanceSearchInputDto>();

            #endregion

            #region Seo file
            CreateMap<SeoFile, SeoFileDto>();
            CreateMap<SeoFile, FilteredSeoFileDto>();
            CreateMap<StoreSeoFileCommand, SeoFile>();
            #endregion


            #region -------------------  Slider  ---------------------
            CreateMap<CreateSliderCommand, Slider>();
            CreateMap<CreateSliderFileCommand, SliderFile>();
            CreateMap<UpdateSliderCommand, Slider>();
            CreateMap<UpdateSliderFileCommand, SliderFile>();
            CreateMap<Slider, SliderDto>();
            CreateMap<SliderFile, GetSliderFileDto>();
            CreateMap<Slider, FilteredSlidersDto>();
            CreateMap<SliderFile, FilteredSliderFileDto>();
            CreateMap<CreateSliderRequest, CreateSliderCommand>()
               .ForMember(a => a.Image, opt => opt.Ignore());
            CreateMap<UpdateSliderRequest, UpdateSliderCommand>()
             .ForMember(a => a.Image, opt => opt.Ignore());
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