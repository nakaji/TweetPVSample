using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using TweetPV;

namespace WebJob
{
    // To learn more about Microsoft Azure WebJobs SDK, please see http://go.microsoft.com/fwlink/?LinkID=320976
    class Program
    {
        static void Main()
        {
            var file = ConfigurationManager.AppSettings["analyticsKeyFile"];
            var helper = new AnalyticsHelper(file);

            var pv = helper.GetPvAsync().Result;
            var message = string.Format("昨日のなか日記のPVは{0}でしたyo http://nakaji.hatenablog.com/ ", pv);
            TwitterHelper.UpdateStatusAsync(message).Wait();

        }
    }
}
