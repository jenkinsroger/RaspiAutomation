using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using Nist;
using System.Net.Sockets;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using Windows.Networking.Sockets;
using Windows.Networking;
using System.Text.RegularExpressions;

namespace HomeAutomation
{
    class UpdateTimeClass
    {
        private static StreamSocket clientSocket;
        private static HostName serverHost;
        private static string serverHostnameString;
        private static string serverPort;
        private static bool connected = false;
        private static bool closing = false;

        public static async void getdata()
        {
            //Create an HTTP client object
            Windows.Web.Http.HttpClient httpClient = new Windows.Web.Http.HttpClient();

            //Add a user-agent header to the GET request. 
            var headers = httpClient.DefaultRequestHeaders;

            //The safe way to add a header value is to use the TryParseAdd method and verify the return value is true,
            //especially if the header value is coming from user input.
            string header = "ie";
            if (!headers.UserAgent.TryParseAdd(header))
            {
                throw new Exception("Invalid header value: " + header);
            }

            header = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
            if (!headers.UserAgent.TryParseAdd(header))
            {
                throw new Exception("Invalid header value: " + header);
            }

            Uri requestUri = new Uri("http://nist.time.gov/actualtime.cgi?lzbc=siqm9b");
            
            //Send the GET request asynchronously and retrieve the response as a string.
            Windows.Web.Http.HttpResponseMessage httpResponse = new Windows.Web.Http.HttpResponseMessage();
            string httpResponseBody = "";

            try
            {
                //Send the GET request
                httpResponse = await httpClient.GetAsync(requestUri);
                httpResponse.EnsureSuccessStatusCode();
                httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
            }

            CommunicationScreen.Message = httpResponseBody;

            
            string[] trimresponse = httpResponseBody.Split('"');
            long timestamp = Convert.ToInt64(trimresponse[1]);

            DateTime newdatetime = new DateTime(timestamp);
            TimeSpan correction = new TimeSpan(14, 30, 00);
            newdatetime.Subtract(correction);
            CommunicationScreen.Message += "\r\n\r\n" + string.Format("{0:hh:mm:ss tt}", newdatetime - correction);

            MainPage.newdatetime = newdatetime - correction;

        }

        public static async void Connect_Click(string address, string port)
        {
            if (connected)
            {
                CommunicationScreen.Message = "Already connected";
                return;
            }

            try
            {
                CommunicationScreen.Message = "Trying to connect ...";

                serverHost = new HostName(address);
                // Try to connect to the 
                await clientSocket.ConnectAsync(serverHost, port);
                connected = true;
                CommunicationScreen.Message  += "Connection established" + Environment.NewLine;

            }
            catch (Exception exception)
            {
                // If this is an unknown status, 
                // it means that the error is fatal and retry will likely fail.
                if (Windows.Networking.Sockets.SocketError.GetStatus(exception.HResult) == SocketErrorStatus.Unknown)
                {
                    throw;
                }

                CommunicationScreen.Message  += "Connect failed with error: " + exception.Message;
                // Could retry the connection, but for this simple example
                // just close the socket.

                closing = true;
                // the Close method is mapped to the C# Dispose
                clientSocket.Dispose();
                clientSocket = null;

            }
        }

        public static async void  GetNistTime()
        {
            DateTime dateTime = DateTime.MinValue;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://nist.time.gov/actualtime.cgi?lzbc=siqm9b");
            request.Method = "GET";
            request.Accept = "text/html, application/xhtml+xml, */*";
            //request.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; Trident/6.0)";
            request.ContentType = "application/x-www-form-urlencoded";
            //request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore); //No caching
            HttpWebResponse response =  (HttpWebResponse) await request.GetResponseAsync();//.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                StreamReader stream = new StreamReader(response.GetResponseStream());
                string html = stream.ReadToEnd();//<timestamp time="\"1395772696469995\"" delay="\"1395772696469995\"/">
                string time = Regex.Match(html, @"(?<=\btime="")[^""]*").Value;
                double milliseconds = Convert.ToInt64(time) / 1000.0;
                dateTime = new DateTime(1970, 1, 1).AddMilliseconds(milliseconds).ToLocalTime();
            }

            CommunicationScreen.Message = response.ToString();
        }

    }
}
