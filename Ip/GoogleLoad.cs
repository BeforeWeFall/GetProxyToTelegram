using System;
using System.IO;
using System.Net;
using Telegram.Bot;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Threading;
using File = Google.Apis.Drive.v3.Data.File;
using System.Net.Http;
using Microsoft.Win32;

namespace Ip
{
    class GoogleLoad
    {
        private static string[] Scopes = { DriveService.Scope.Drive };

        readonly static string ApplicationName = "GDTry";
        readonly string page = "YOUR_URL";

        public GoogleLoad(string PathIp)
        {
            Console.WriteLine("Cred");
            var credentials = GetUserCredential();
            Console.WriteLine("serv");
            var servies = GetDriveServies(credentials);
            Console.WriteLine("File");

            CheckFileAndRemove(servies);

            UploadFileToDrive(servies, "IP", PathIp, @"application/msword");//application/msword     text/plain
        }

        private UserCredential GetUserCredential()
        {
            using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string creedPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                creedPath = Path.Combine(creedPath, "driveApiCredentials", "drive-credentials.json");
                return GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "User",
                    CancellationToken.None,
                    new FileDataStore(creedPath, true)).Result;
            }
        }

        private DriveService GetDriveServies(UserCredential cred)
        {
            return new DriveService(
                new BaseClientService.Initializer
                {
                    HttpClientInitializer = cred,
                    ApplicationName = ApplicationName
                });
        }

        private string UploadFileToDrive(DriveService service, string fileName, string filePath, string contentType)
        {
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey helloKey = currentUserKey.OpenSubKey("GoogleApiKey");
            string apiKey = helloKey.GetValue("Key").ToString();

            currentUserKey.Close();

            var fileMeta = new File();
            fileMeta.Name = fileName;
            fileMeta.Parents = new List<string> { apiKey };

            FilesResource.CreateMediaUpload request;

            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                request = service.Files.Create(fileMeta, stream, contentType);
                request.Upload();
            }
            var file = request.ResponseBody;
            return file.Id;
        }

        private void RemoveFile(DriveService serv, string fileID)
        {
            try
            {
                serv.Files.Delete(fileID).Execute();
            }
            catch
            {
                Console.WriteLine("ohNoooo");
            }
        }

        private async void CheckFileAndRemove(DriveService servies)
        {
            string result = "";
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(page))
            using (HttpContent content = response.Content)
            {
                result = await content.ReadAsStringAsync();
            }

            if (!result.Equals(""))
            {
                var regex = new Regex(@"(?<=\\x22).{33}(?=\\x22,)");
                var reg = regex.Matches(result);
                if (reg != null)
                {
                    foreach (var t in reg)
                    {
                        RemoveFile(servies, t.ToString());
                    }
                }
            }
        }
    }
}
