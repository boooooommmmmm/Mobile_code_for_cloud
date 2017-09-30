using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;

namespace TeamFantasyMobileAppService.Controllers
{
    [MobileAppController]
    public class NumberController : ApiController
    {
        // GET api/Number
        public ResultViewModel Get(int? first, int? second)
        {
            if (first == null || second == null) {
                throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest);
            }
            ResultViewModel results = new ResultViewModel
            {
                First = first.GetValueOrDefault(),
                Second = second.GetValueOrDefault()
            };
            results.Result = results.First + results.Second;
            return results;
            
        }
    }
}


public class ResultViewModel
{
    public int First { get; set; }
    public int Second { get; set; }
    public int Result { get; set; }
}