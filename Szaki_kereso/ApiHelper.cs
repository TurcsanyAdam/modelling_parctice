using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Szaki_kereso
{
    public static class ApiHelper
    {
        // Initializes an TCP/IP port to allow api calls with json return
        internal static HttpClient ApiClient { get; set; } = new HttpClient();

        public static void InitializeClient()
        {
            ApiClient = new HttpClient();
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
