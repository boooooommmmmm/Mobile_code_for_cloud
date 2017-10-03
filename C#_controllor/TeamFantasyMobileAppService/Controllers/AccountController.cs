using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
//using System.Web.UI.WebControls.WebParts;

namespace TeamFantasyMobileAppService.Controllers
{
    [MobileAppController]
    public class AccountController : ApiController
    {
        // GET api/Test
        public List<Account> accountTable = new List<Account>();

        public string Get(string email, string password)
        {
            init();

            Account result = accountTable.Find(x => x.Email == email);          

            if (result==null) {
                return "Error 404: Email does not exist!";
            }
            else if (!result.Password.Equals(password)) {
                return "Error 405: Password does not match!";
            }
            else {
                return "Login Success!";
            }

            //return "Error 405 Login Services is not avaliable!";
        }

        public void init()
        {
            accountTable.Add(new Account { Email = "123@gmail.com", Password = "123" });
            accountTable.Add(new Account { Email = "testAdminUser@gmail.com", Password = "AdminPassword" });
        }

    }

}
