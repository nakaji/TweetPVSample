using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using CoreTweet;
using TweetPV;

namespace WebApplication.Controllers
{
    public class TweetPVController : ApiController
    {
        // GET api/TweetPV
        public async Task<string> Get()
        {
            var file = ConfigurationManager.AppSettings["analyticsKeyFile"];
            var analyticsKeyFile = file.StartsWith("~") ? HttpContext.Current.Server.MapPath(file) : file; // ~で始まっていれば絶対パスに変換
            var helper = new AnalyticsHelper(analyticsKeyFile);
            
            var pv = await helper.GetPvAsync();
            var message = string.Format("昨日のなか日記のPVは{0}でしたyo http://nakaji.hatenablog.com/ ", pv);
            await TwitterHelper.UpdateStatusAsync(message);

            return message;
        }
    }
}
