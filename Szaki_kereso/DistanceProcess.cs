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
        
        public async Task<double> GetClosestHandyman(User user, Login login)
        {
            double distance = 0;
            string handymanUsername = "";

            foreach (Handyman handyman in login.handymanList)
            {
                string origin = user.Adress;
                string destination = handyman.Adress;
                string APIKey = "AIzaSyA2VDeHXyseqIZ6PDPjBNIVmWXBeOeMT8w";
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
                            if(item.elements[0].distance.value<distance || distance == 0)
                            {
                                handymanUsername = handyman.Username;
                                distance = item.elements[0].distance.value;
                            }
                        }
                    }
                    else
                    {
                        throw new Exception(response.ReasonPhrase);
                    }
                }

            }
            Random rand = new Random();
            int workingFee = rand.Next(1, 100) * 10;
            Console.WriteLine($"\n{user.Username} is {Math.Round(distance / 1000, 2)} km away from the closest handyman - {handymanUsername}, working fee {workingFee} EUR");
            Console.Write($"Do you want to work with {handymanUsername}? (Yes/No)");
            string decision = Console.ReadLine();
            if(decision.ToLower() == "yes")
            {
                foreach (Handyman handyman in login.handymanList)
                {
                    if (handyman.Username == handymanUsername)
                    {
                        handyman.Work(user, workingFee);
                    }

                }

            }
            return distance;

        }
        public async Task<Dictionary<Handyman, double>> GetHandymanInRadius(User user, Login login, double radius)
        {
            Dictionary<Handyman, double> handymenInRadius = new Dictionary<Handyman, double>();
            foreach (Handyman handyman in login.handymanList)
            {
                string origin = user.Adress;
                string destination = handyman.Adress;
                string APIKey = "AIzaSyA2VDeHXyseqIZ6PDPjBNIVmWXBeOeMT8w";
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
                            if ((item.elements[0].distance.value)/1000 <= radius)
                            {
                                handymenInRadius[handyman] = ((item.elements[0].distance.value) / 1000);
                            }
                        }
                    }
                    else
                    {
                        throw new Exception(response.ReasonPhrase);
                    }
                }

            }
            return handymenInRadius;
        }
        public async Task<string> GetHandymanByUsername(User user, Login login, string username)
        {

            foreach (Handyman handyman in login.handymanList)
            {
                if(handyman.Username == username)
                {
                    string origin = user.Adress;
                    string destination = handyman.Adress;
                    string APIKey = "AIzaSyA2VDeHXyseqIZ6PDPjBNIVmWXBeOeMT8w";
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
                                string resultByUsername = ($"{handyman.Username} is {(item.elements[0].distance.value) / 1000} km away from you");
                                Console.WriteLine(resultByUsername);
                            }
                        }
                        else
                        {
                            throw new Exception(response.ReasonPhrase);
                        }
                    }
                }
            }
            throw new ArgumentException("Handyman not in database");
        }
        public async Task<string> GetHandymanByProfession(User user, Login login, string specialization)
        {

            foreach (Handyman handyman in login.handymanList)
            {
                if (handyman.Specialization == specialization)
                {
                    string origin = user.Adress;
                    string destination = handyman.Adress;
                    string APIKey = "AIzaSyA2VDeHXyseqIZ6PDPjBNIVmWXBeOeMT8w";
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
                                string resultByUsername = ($"{handyman.Username} is {(item.elements[0].distance.value) / 1000} km away from you");
                                Console.WriteLine(resultByUsername);
                            }
                        }
                        else
                        {
                            throw new Exception(response.ReasonPhrase);
                        }
                    }
                }
            }
            throw new ArgumentException("Handyman not in database");
        }
    }
}
