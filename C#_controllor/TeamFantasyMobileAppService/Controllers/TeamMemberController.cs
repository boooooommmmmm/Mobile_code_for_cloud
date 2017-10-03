using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;

namespace TeamFantasyMobileAppService.Controllers
{
    [MobileAppController]

    public class TeamMemberController : ApiController
    {
        // GET api/TeamMember
        public getTeamMember Get(int? bl_request)
        {
            if (bl_request == null)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest);
            }
            if (bl_request == 0)
            {
                getTeamMember members = new getTeamMember
                {
                    team_member_one = "",
                    team_member_two = "",
                    team_member_three = ""
                };
                return members;

            }
            if (bl_request == 1)
            {
                getTeamMember members = new getTeamMember
                {
                    team_member_one = "gyy",
                    team_member_two = "cx",
                    team_member_three = "hsy"
                };
                return members;
            }

            throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest);

        }
    }
}


public class getTeamMember
{
    //public int Bl_request { get; set; }
    public string team_member_one { get; set; }
    public string team_member_two { get; set; }
    public string team_member_three { get; set; }
}

public class getTeamMemberEmpty
{
    //public int Bl_request { get; set; }
    public string team_member_empty { get; set; }
}


