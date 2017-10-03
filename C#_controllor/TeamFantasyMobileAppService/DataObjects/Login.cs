using Microsoft.Azure.Mobile.Server;

namespace TeamFantasyMobileAppService.DataObjects
{
    public class Login : EntityData
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}