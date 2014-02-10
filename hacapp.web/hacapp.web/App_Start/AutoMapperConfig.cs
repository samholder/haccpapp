using System.Linq;
using AutoMapper;
using Hacapp.Core.Queries.Result;
using Hacapp.Web.Models;
using Hacapp.Web.Models.Identity;
using Hacapp.Web.Models.Team;
using Haccapp.Model.Domain;
using Haccapp.Model.Identity;

namespace hacapp.web
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<ApplicationUser, UserViewModel>();
            Mapper.CreateMap<Team, TeamDetailsViewModel>()
                .ForMember(x => x.ConfirmedMembers,
                    t =>
                        t.MapFrom(s => s.Members.Where(m => m.Status == MembershipStatus.Confimed).Select(me => me.User)))
                .ForMember(x => x.PendingMembers,
                    t =>
                        t.MapFrom(s => s.Members.Where(m => m.Status == MembershipStatus.Pending).Select(me => me.User)))
                .ForMember(x => x.SuspendedMembers,
                    t =>
                        t.MapFrom(
                            s => s.Members.Where(m => m.Status == MembershipStatus.Suspended).Select(me => me.User)));
            Mapper.CreateMap<PendingMembershipResult, PendingMembershipModel>();
        }
    }
}