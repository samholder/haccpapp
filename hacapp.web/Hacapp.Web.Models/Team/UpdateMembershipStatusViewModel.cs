namespace Hacapp.Web.Models.Team
{
    public class UpdateMembershipStatusViewModel
    {
        public string IdOfUserToModify { get; set; }
        public int TeamId { get; set; }
        public UserMembershipStatus NewStatus { get; set; }
    }
}