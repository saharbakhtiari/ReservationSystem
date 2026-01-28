using Application.AdvanceSearch.Queries.SearchRuleVersion;
using Application.Amenities.Queries.GetFilteredAmenities;
using Application.Amenitys.Commands.CreateAmenity;
using Application.Amenitys.Commands.UpdateAmenity;
using Application.Amenitys.Queries.GetAmenity;
using Application.BookingHolds.Commands.CreateBookingHold;
using Application.BookingHolds.Commands.UpdateBookingHold;
using Application.BookingHolds.Queries.GetBookingHold;
using Application.BookingHolds.Queries.GetFilteredBookingHolds;
using Application.Bookings.Commands.CreateBooking;
using Application.Bookings.Commands.UpdateBooking;
using Application.Bookings.Queries.GetBooking;
using Application.Bookings.Queries.GetFilteredBookings;
using Application.CancellationPolicys.Commands.CreateCancellationPolicy;
using Application.CancellationPolicys.Commands.UpdateCancellationPolicy;
using Application.CancellationPolicys.Queries.GetCancellationPolicy;
using Application.CancellationPolicys.Queries.GetFilteredCancellationPolicys;
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
using Application.Spaces.Commands.CreateSpace;
using Application.Spaces.Commands.UpdateSpace;
using Application.Spaces.Queries.GetFilteredSpaces;
using Application.Spaces.Queries.GetSpace;
using Application.Tariffs.Commands.CreateTariff;
using Application.Tariffs.Commands.UpdateTariff;
using Application.Tariffs.Queries.GetFilteredTariffs;
using Application.Tariffs.Queries.GetTariff;
using Application.UserManagers.Commands.CreateUser;
using Application.UserManagers.Commands.EditPhoneNumber;
using Application.UserManagers.Commands.EditRegisteredUser;
using Application.UserManagers.Commands.EditUser;
using Application.UserManagers.Commands.VerifyOtpLogin;
using Application.UserManagers.Commands.VerifyRegisteration;
using AutoMapper;
using Domain.AdvanceSearchs;
using Domain.Amenitys;
using Domain.BookingHolds;
using Domain.Bookings;
using Domain.CancellationPolicys;
using Domain.Common.Mappings;
using Domain.Footers;
using Domain.Headers;
using Domain.MemberProfiles;
using Domain.SeoFiles;
using Domain.SliderFiles;
using Domain.Sliders;
using Domain.SpaceFiles;
using Domain.Spaces;
using Domain.Tariffs;
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

            #region Space
            CreateMap<CreateSpaceCommand, Space>();
            CreateMap<CreateSpaceFileCommand, SpaceFile>();
            CreateMap<UpdateSpaceFileCommand, SpaceFile>();
            CreateMap<UpdateSpaceCommand, Space>();
            CreateMap<CreateSpaceRequest, CreateSpaceCommand>()
                .ForMember(a => a.Gallery, opt => opt.Ignore())
                .ForMember(a => a.MainImage, opt => opt.Ignore());

            CreateMap<UpdateSpaceRequest, UpdateSpaceCommand>()
                .ForMember(a => a.Gallery, opt => opt.Ignore())
                .ForMember(a => a.MainImage, opt => opt.Ignore());
            CreateMap<Space, GetSpaceByIdDto>();
            CreateMap<SpaceFile, GetSpaceByIdFileDto>();
            CreateMap<Space, FilteredSpacesDto>();
            CreateMap<SpaceFile, FilteredSpacesFileDto>();
            CreateMap<Amenity, GetSpaceByIdAmenity>();
            #endregion



            #region Amenity
            CreateMap<CreateAmenityCommand, Amenity>();
            CreateMap<CreateAmenityFileCommand, SpaceFile>();
            CreateMap<UpdateAmenityFileCommand, SpaceFile>();
            CreateMap<UpdateAmenityCommand, Amenity>();
            CreateMap<CreateAmenityRequest, CreateAmenityCommand>()
                .ForMember(a => a.Icon, opt => opt.Ignore());
            CreateMap<UpdateAmenityRequest, UpdateAmenityCommand>()
                .ForMember(a => a.Icon, opt => opt.Ignore());
            CreateMap<Amenity, GetAmenityByIdDto>();
            CreateMap<SpaceFile, GetAmenityByIdFileDto>();
            CreateMap<Amenity, FilteredAmenitiesDto>();
            CreateMap<SpaceFile, FilteredAmenitiesFileDto>();
            #endregion

            #region Tariff
            CreateMap<CreateTariffCommand, Tariff>();
            CreateMap<UpdateTariffCommand, Tariff>();
            CreateMap<Tariff, GetTariffByIdDto>();
            CreateMap<Space, GetTariffByIdSpaceDto>();
            CreateMap<Tariff, FilteredTariffsDto>();
            #endregion


            #region CancellationPolicy
            CreateMap<CreateCancellationPolicyCommand, CancellationPolicy>();
            CreateMap<UpdateCancellationPolicyCommand, CancellationPolicy>();
            CreateMap<CancellationPolicy, GetCancellationPolicyByIdDto>();
            CreateMap<Tariff, GetCancellationPolicyByIdTariffDto>();
            CreateMap<CancellationPolicy, FilteredCancellationPolicysDto>();

            #endregion


            #region Booking
            CreateMap<CreateBookingCommand, Booking>();
            CreateMap<UpdateBookingCommand, Booking>();
            CreateMap<Booking, GetBookingByIdDto>();
            CreateMap<Space, GetBookingByIdSpaceDto>();
            CreateMap<MemberProfile, GetBookingByIdProfileDto>();
            CreateMap<Booking, FilteredBookingsDto>()
                .ForMember(a => a.ProfileUserName, opt => opt.MapFrom(p => p.Profile.UserName))
                .ForMember(a => a.SpaceTitle, opt => opt.MapFrom(p => p.Space.Title));

            #endregion

            #region Booking
            CreateMap<CreateBookingHoldCommand, BookingHold>();
            CreateMap<UpdateBookingHoldCommand, BookingHold>();
            CreateMap<BookingHold, GetBookingHoldByIdDto>();
            CreateMap<Space, GetBookingHoldByIdSpaceDto>();
            CreateMap<MemberProfile, GetBookingHoldByIdProfileDto>();
            CreateMap<BookingHold, FilteredBookingHoldsDto>()
                .ForMember(a => a.ProfileUserName, opt => opt.MapFrom(p => p.Profile.UserName))
                .ForMember(a => a.SpaceTitle, opt => opt.MapFrom(p => p.Space.Title));

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