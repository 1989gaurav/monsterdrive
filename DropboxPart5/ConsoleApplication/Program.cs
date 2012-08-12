using System;
using Dropbox.Api;
using System.Diagnostics;
using System.Threading;
using OAuthProtocol;
using System.Web;
using System.IO;

namespace ConsoleApplication
{
    public class Program
    {
        private const string ConsumerKey = "7uuugyuteu7jr87";
        private const string ConsumerSecret = "p4exksgmj6h9xkq";

        private static OAuthToken GetAccessToken()
        {
            var oauth = new OAuth();

            var requestToken = oauth.GetRequestToken(new Uri(DropboxRestApi.BaseUri), ConsumerKey, ConsumerSecret);

            var authorizeUri = oauth.GetAuthorizeUri(new Uri(DropboxRestApi.AuthorizeBaseUri), requestToken);
            Process.Start(authorizeUri.AbsoluteUri);
            Thread.Sleep(5000); // Leave some time for the authorization step to complete

            return oauth.GetAccessToken(new Uri(DropboxRestApi.BaseUri), ConsumerKey, ConsumerSecret, requestToken);
        }


        public String TestUploadFiles()
        {
            Console.WriteLine("Attempting to get access token before uploading the file");
            //Your access token: 6on3pwn3smj94m4
            //Your access secret: tj4429nmp6nc9af
            var accessToken = new OAuthToken("6on3pwn3smj94m4", "tj4429nmp6nc9af");

            var api = new DropboxApi(ConsumerKey, ConsumerSecret, accessToken);

            var file = api.UploadFile("dropbox", "TargetFileName.ext", @"test.txt");

            var account = api.GetAccountInfo();
            Console.WriteLine(account.DisplayName);
            Console.WriteLine(account.Email);

            var total = account.Quota.Total / (1024 * 1024);
            var used = (account.Quota.Normal + account.Quota.Shared) / (1024 * 1024);

            Console.WriteLine(String.Format("Dropbox: {0}/{1} Mb used", used, total));

            Console.WriteLine(string.Format("{0} uploaded.", file.Path));

            Console.WriteLine();
            Console.WriteLine("Done. Press any key to continue...");
            Console.Read();

            return String.Format("{0}/{1} Mb used", used, total);
        }

        public Account getDropboxAccount()
        {
            Console.WriteLine("Attempting to get access token before uploading the file");
            //Your access token: 6on3pwn3smj94m4
            //Your access secret: tj4429nmp6nc9af
            var accessToken = new OAuthToken("6on3pwn3smj94m4", "tj4429nmp6nc9af");

            var api = new DropboxApi(ConsumerKey, ConsumerSecret, accessToken);

            var account = api.GetAccountInfo();
            Console.WriteLine(account.DisplayName);
            Console.WriteLine(account.Email);

            var total = account.Quota.Total / (1024 * 1024);
            var used = (account.Quota.Normal + account.Quota.Shared) / (1024 * 1024);

            Console.WriteLine(String.Format("Dropbox: {0}/{1} Mb used", used, total));


            return account;
        }

        public static void Main()
        {
            // Uncomment the following line or manually provide a valid token so that you
            // don't have to go through the authorization process each time.
            // var accessToken = GetAccessToken();

            //Your access token: 6on3pwn3smj94m4
            //Your access secret: tj4429nmp6nc9af
            var accessToken = new OAuthToken("6on3pwn3smj94m4", "tj4429nmp6nc9af");

            var api = new DropboxApi(ConsumerKey, ConsumerSecret, accessToken);

            var file = api.UploadFile("dropbox", "TargetFileName.ext", @"test.txt");

            Console.WriteLine(string.Format("{0} uploaded.", file.Path));

            Console.WriteLine();
            Console.WriteLine("Done. Press any key to continue...");
            Console.ReadKey();
        }
    }
}
