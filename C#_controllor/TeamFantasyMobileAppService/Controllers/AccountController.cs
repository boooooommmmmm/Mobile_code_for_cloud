﻿using System.Web.Http;
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
            accountTable.Clear();
            accountTable.Add(new Account { Email = "123@gmail.com", Password = "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3" });
            accountTable.Add(new Account { Email = "testAdminUser@gmail.com", Password = "45961da9ce13da68788eac0836edf79c1a0b510746b26bb471acf8c53a9dd63e" });
        }
        public List<Account> accountTable = new List<Account>();
    }

}
