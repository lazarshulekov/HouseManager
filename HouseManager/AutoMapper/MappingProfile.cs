using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseManager.AutoMapper
{
    using BLL.Models;

    using DAL.Models;
    using DAL.Models.Identity;

    using global::AutoMapper;

    using HouseManager.ViewModels;

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
            CreateMap<Questionnaire, MeetingQuestionnaireViewModel>()
                .ForMember(x => x.Id, opts => opts.MapFrom(src => src.Id)).
                ForMember(x => x.Question, opts => opts.MapFrom(src => src.Question));


            CreateMap<BuildingViewModel, Building>()
                .ForMember(x => x.Id, opts => opts.MapFrom(src => src.Id)).
                ForMember(x => x.City, opts => opts.MapFrom(src => src.City)).
                ForMember(x => x.Number, opts => opts.MapFrom(src => src.Number)).
                ForMember(x => x.Street, opts => opts.MapFrom(src => src.Street)).
                ForMember(x => x.BuildingHouseManagers, opts => opts.MapFrom(src => CreateBuildingHouseManagers(src.SelectedManagers, src.Id)));

            CreateMap<MeetingViewModel, Meeting>().ForMember(x => x.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(x => x.Comments, opts => opts.MapFrom(src => src.Comments))
                .ForMember(x => x.DateTime, opts => opts.MapFrom(src => src.DateTime))
                .ForMember(x => x.Location, opts => opts.MapFrom(src => src.Location))
                .ForMember(x => x.MeetingsQuestionnaires, opts => opts.MapFrom(src => CreateMeetingIssues(src.SelectedIssues, src.Id)));

            CreateMap<Meeting, MeetingViewModel>();
            CreateMap<PropertyViewModel, Property>();
            CreateMap<PropertyType, PropertyTypeViewModel>();
            CreateMap<AppUser, AppUserViewModel>();
            CreateMap<AppUserViewModel, AppUser>();
            CreateMap<AppRole, AppRoleViewModel>();
            CreateMap<AppRoleViewModel, AppRole>();
        }

        private List<BuildingHousemanagers> CreateBuildingHouseManagers(IEnumerable<int> selectedManagers, int buildingId)
        {
            return selectedManagers.Select(x => new BuildingHousemanagers { HouseManagerId = x, BuildingId = buildingId }).ToList();
        }

        private string CreateBuildingName(Property property)
        {
            var bld = property.BuildingProperties.SingleOrDefault();

            return bld != null
                       ? $"{bld.Building.Id} {bld.Building.City} {bld.Building.Street} {bld.Building.Number}"
                       : string.Empty;
        }

        private List<MeetingsQuestionnaires> CreateMeetingIssues(IEnumerable<int> selectedIssues, int meetingId)
        {
            return selectedIssues.Select(x => new MeetingsQuestionnaires { QuestionnaireId = x, MeetingId = meetingId }).ToList();
        }
    }
}