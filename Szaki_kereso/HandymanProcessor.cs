using System;
using System.Collections.Generic;
using System.Text;

namespace Szaki_kereso
{
    public class HandymanProcessor
    {
        public string GetClosestHandyman(DistanceProcess distanceProcess)
        {
            double distance = 0;
            string handymanUsername = "";

            foreach (KeyValuePair<Handyman, double> kvp in distanceProcess.handymenWithRadius)
            {
                if (kvp.Value < distance || distance == 0)
                {
                    distance = kvp.Value;
                    handymanUsername = kvp.Key.Username;
                }
            }
            string result = $"\nYou are {Math.Round(distance / 1000, 2)} km away from the closest handyman - {handymanUsername}";
            return result;

        }

        public Dictionary<Handyman, double> GetHandymanInRadius(DistanceProcess distanceProcess, double distance)
        {
            Dictionary<Handyman, double> handymenInRadius = new Dictionary<Handyman, double>();

            foreach (KeyValuePair<Handyman, double> kvp in distanceProcess.handymenWithRadius)
            {
                if (kvp.Value <= distance)
                {
                    handymenInRadius.Add(kvp.Key, kvp.Value);
                }
            }

            return handymenInRadius;
        }

        public string GetHandymanByUsername(DistanceProcess distanceProcess, string username)
        {
            foreach (KeyValuePair<Handyman, double> kvp in distanceProcess.handymenWithRadius)
            {
                if (kvp.Key.Username == username)
                {
                    return kvp.Key.ToString();
                }

            }
            throw new ArgumentException ("Handyman not in database");

        }
        public string GetHandymanBySpecialization(DistanceProcess distanceProcess, string specialization)
        {
            foreach (KeyValuePair<Handyman, double> kvp in distanceProcess.handymenWithRadius)
            {
                if (kvp.Key.Specialization == specialization)
                {
                    return kvp.Key.ToString();
                }

            }
            throw new ArgumentException("No handyman with the given specialization in database");

        }
    }
}
