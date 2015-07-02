using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreTweet;

namespace TweetPV
{
    public static class TwitterHelper
    {
        public static async Task<Status> UpdateStatusAsync(string message)
        {
            var consumerKey = ConfigurationManager.AppSettings["consumerKey"];
            var consumerSecret = ConfigurationManager.AppSettings["consumerSecret"];
            var accessToken = ConfigurationManager.AppSettings["accessToken"];
            var accessTokenSecret = ConfigurationManager.AppSettings["accessTokenSecret"];

            var tokens = Tokens.Create(consumerKey, consumerSecret, accessToken, accessTokenSecret);

            return await tokens.Statuses.UpdateAsync(status => message);
        }
    }
}
