using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseManager.AutoMapper
{
    using BLL.Models;

    using global::AutoMapper;

    using HouseManager.ViewModels;

    using Persistence.Models;
    using Persistence.Models.Identity;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Property, PropertyViewModel>().
                ForMember(x => x.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(x => x.AppUser, opts => opts.MapFrom(src => src.AppUser))
                .ForMember(x => x.AppUserId, opts => opts.MapFrom(src => src.AppUserId))
                .ForMember(x => x.Area, opts => opts.MapFrom(src => src.Area))
                .ForMember(x => x.Comments, opts => opts.MapFrom(src => src.Comments))
                .ForMember(x => x.PropertyType, opts => opts.MapFrom(src => src.PropertyType))
                .ForMember(x => x.PropertyTypeId, opts => opts.MapFrom(src => src.PropertyTypeId)).ForMember(
                    x => x.BuildingName,
                    opts => opts.MapFrom(src => CreateBuildingName(src)));

            CreateMap<Questionnaire, QuestionnaireViewModel>()
                .ForMember(x => x.Id, opts => opts.MapFrom(src => src.Id)).
                ForMember(x => x.Question, opts => opts.MapFrom(src => src.Question));

            CreateMap<MeetingViewModel, Meeting>();
            CreateMap<Meeting, MeetingViewModel>();
            CreateMap<PropertyViewModel, Property>();
            CreateMap<PropertyType, PropertyTypeViewModel>();
            CreateMap<AppUser, AppUserViewModel>();
            CreateMap<AppUserViewModel, AppUser>();
            CreateMap<AppRole, AppRoleViewModel>();
            CreateMap<AppRoleViewModel, AppRole>();
        }

        private string CreateBuildingName(Property property)
        {
            var bld = property.BuildingProperties.SingleOrDefault();

            return bld != null
                       ? $"{bld.Building.Id} {bld.Building.City} {bld.Building.Street} {bld.Building.Number}"
                       : string.Empty;
        }
    }
}