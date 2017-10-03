using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using TeamFantasyMobileAppService.DataObjects;
using TeamFantasyMobileAppService.Models;

namespace TeamFantasyMobileAppService.Controllers
{
    public class LoginController : TableController<Login>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            TeamFantasyMobileAppContext context = new TeamFantasyMobileAppContext();
            DomainManager = new EntityDomainManager<Login>(context, Request);
        }

        // GET tables/TodoItem
        public IQueryable<Login> GetAllTodoItems()
        {
            return Query();
        }

        // GET tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Login> GetTodoItem(string id)
        {
            return Lookup(id);
        }

        // Test: validation
        public bool Validate(string email, string password)
        {
            bool b = false;
            IQueryable<Login> total = Query();
            foreach (Login item in total)
            {
                if (item.Email.Equals(email) && item.Password.Equals(password))
                {
                    b = true;
                    break;
                }
            }

            return b;
        }



        // PATCH tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Login> PatchTodoItem(string id, Delta<Login> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/TodoItem
        public async Task<IHttpActionResult> PostTodoItem(Login item)
        {
            Login current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteLogin(string id)
        {
            return DeleteAsync(id);
        }
    }
}