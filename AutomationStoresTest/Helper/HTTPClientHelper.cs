using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace AutomationStoresTest.Helper
{
    public class HttpClientHelper
    {
        public string response = null;
        public string requestUrl = null;
        Stopwatch timer = new Stopwatch();
        public string baseURL = "http://localhost:54356";

        HttpClient client = new HttpClient();

        public class ApiResponse
        {
            public int statusCode;
            public string statusMessage;
            public string jsonResponse;
            public int responseTimeInMilliseconds;
        }

        public ApiResponse getApiReponse(string methodType, string carType)
        {

            ApiResponse apiResponse = new ApiResponse();
            requestUrl = string.Empty;
            if (methodType.ToLower() == "get")
            {
                requestUrl = baseURL+ $"/api/cars/{carType}";
            }
            client.BaseAddress = new Uri(requestUrl);

            try
            {
                switch (methodType.ToLower())
                {
                    case "get":
                        timer.Start();
                        var responseTask = client.GetAsync(client.BaseAddress);
                        responseTask.Wait();
                        timer.Stop();
                        apiResponse = getAllData(responseTask, timer);
                        return apiResponse;

                    case "post":
                    //ToBeImplemented

                    case "put":
                    //ToBeImplemented

                    case "delete":
                    //ToBeImplemented

                    default:
                        return null;
                }
            }
            catch (WebException ex)
            {
                timer.Stop();
                HttpWebResponse responseEx = (HttpWebResponse)ex.Response;
                apiResponse.statusCode = (int)responseEx.StatusCode;
                apiResponse.statusMessage = Convert.ToString(responseEx.StatusCode);
                apiResponse.jsonResponse = ex.Message;
                apiResponse.responseTimeInMilliseconds = Convert.ToInt32(timer.ElapsedMilliseconds);
                return apiResponse;
            }


        }

        public ApiResponse getAllData(Task<HttpResponseMessage> responseTask, Stopwatch timer)
        {
            ApiResponse response = new ApiResponse();

            response.jsonResponse = responseTask.Result.Content.ReadAsStringAsync().Result;
            response.statusCode = (int)responseTask.Result.StatusCode;
            response.statusMessage = Convert.ToString(responseTask.Result.StatusCode);
            response.responseTimeInMilliseconds = Convert.ToInt32(timer.ElapsedMilliseconds);

            return response;
        }
    }
}