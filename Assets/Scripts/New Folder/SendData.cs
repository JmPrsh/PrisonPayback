using Newtonsoft.Json;
using System;
using System.Collections;
using System.IO;
using System.Net;

namespace Assets.Scripts
{
    public class SendData
    {
		private static string URL = "http://stuiis.cms.gre.ac.uk/dd50/PJ210-DDA/PJ210-DDA/api/player";
        public static PlayerClass POSTData(PlayerClass player, string command)
        {
            var request = HttpWebRequest.Create(String.Format(URL+command));// "/save" or "/update"
            request.ContentType = "application/json"; // tell the API we want Json returned
            request.Method = "POST";

            try
            {
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string JSONPost = JsonConvert.SerializeObject(player, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });

                    streamWriter.Write(JSONPost);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        return null;
                    }
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        return JsonConvert.DeserializeObject<PlayerClass>(reader.ReadToEnd());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
//                return null;
            }
        }

        public static PlayerClass GETData(string input)
        {
            //creating a request
            var request = HttpWebRequest.Create(String.Format(URL + "?value={0}", input));
            request.ContentType = "application/json"; // tell the API we want Json returned
            request.Method = "GET";

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        return null;
                    }
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        return JsonConvert.DeserializeObject<PlayerClass>(reader.ReadToEnd());
                    }
                }
            }
            catch (Exception ex)
            {
				throw ex;
//                return null;
            }

        }

    }
}