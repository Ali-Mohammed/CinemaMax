using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace CinemaMaxFeeder
{
    public class RequestHelper
    {
        public static T Call<T>(string reqUrl) 
        {
            var webRequest = WebRequest.Create(reqUrl) as HttpWebRequest;
            Console.WriteLine(reqUrl);
            if (webRequest == null)
            {
                Console.WriteLine("Error in the request");
            }

            webRequest.ContentType = "application/json";
            webRequest.UserAgent = "Nothing";

            using (var s = webRequest.GetResponse().GetResponseStream())
            {
                using (var sr = new StreamReader(s))
                {
                    var contributorsAsJson = sr.ReadToEnd();
                    var movies = JsonConvert.DeserializeObject<T>(contributorsAsJson);
                    return (T)Convert.ChangeType(movies, typeof(T));
                }
            }
        }
    }
}
