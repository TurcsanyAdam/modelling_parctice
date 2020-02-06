using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Szaki_kereso
{
    public class DistanceProcess
    {
        public Dictionary<Handyman, double> handymenWithRadius = new Dictionary<Handyman, double>();
        Initializer initializer;
        User user;
        public DistanceProcess(User user, Initializer initializer)
        {
            this.initializer = initializer;
            this.user = user;
            var a = LoadAllHandyman();
            a.Wait();
        }

        public async Task LoadAllHandyman()
        {
            foreach (Handyman handyman in initializer.handymanList)
            {
                string origin = user.Adress;
                string destination = handyman.Adress;
                string APIKey = Environment.GetEnvironmentVariable("GoogleMapsAPI");
                string url = "https://maps.googleapis.com/maps/api/distancematrix/json?units=metric&origins=" + origin + "&destinations=" + destination +
                "&key=" + APIKey;

                using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        DistanceResult.RootObject root = JsonConvert.DeserializeObject<DistanceResult.RootObject>(result);


                        foreach (var item in root.rows)
                        {
                            handymenWithRadius.Add(handyman, item.elements[0].distance.value);
                        }
                    }
                    else
                    {
                        throw new Exception(response.ReasonPhrase);
                    }
                }

            }
        }
    }
}
