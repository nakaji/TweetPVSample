using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Google.Apis.Analytics.v3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;

namespace TweetPV
{
    public class AnalyticsHelper
    {
        private AnalyticsService _service;
        public AnalyticsHelper(string analyticsKeyFile)
        {
            // Azure Web サイトで動かす場合には WEBSITE_LOAD_USER_PROFILE = 1 必須
            var certificate = new X509Certificate2(analyticsKeyFile, "notasecret", X509KeyStorageFlags.Exportable);

            // Scopes は指定しないとエラーになる
            var analyticsCredentialId = ConfigurationManager.AppSettings["analyticsCredentialId"];
            var credential = new ServiceAccountCredential(new ServiceAccountCredential.Initializer(analyticsCredentialId)
            {
                Scopes = new[] { AnalyticsService.Scope.Analytics, AnalyticsService.Scope.AnalyticsReadonly }
            }.FromCertificate(certificate));

            // HttpClientInitializer に credential 入れるのは違和感あるけど正しいらしい
            _service = new AnalyticsService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "TweetPVSample",
            });
        }

        public async Task<int> GetPvAsync()
        {
            // Azure は UTC なので +9 時間して -1 日
            var date = DateTime.UtcNow.AddHours(9).AddDays(-1).ToString("yyyy-MM-dd");

            // ビューのIDを指定してデータを取得する
            var analyticsViewId = ConfigurationManager.AppSettings["analyticsViewId"];
            var data = await _service.Data.Ga.Get("ga:" + analyticsViewId, date, date, "ga:pageviews").ExecuteAsync();

            return int.Parse(data.Rows[0][0]);
        }
    }
}
