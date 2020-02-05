using System;
using System.Collections.Generic;
using System.Text;

namespace Szaki_kereso
{
    public class HandymanProcessor
    {
        public Dictionary<Handyman, double> GetClosestHandyman(DistanceProcess distanceProcess)
        {
            Handyman closestHandyman = null;
            double distance = 0;
            

            foreach (KeyValuePair<Handyman, double> kvp in distanceProcess.handymenWithRadius)
            {
                if (kvp.Value < distance || distance == 0)
                {
                    distance = kvp.Value;
                    closestHandyman = kvp.Key;
                }
            }
            Dictionary<Handyman, double> closestHandymanWithRadius = new Dictionary<Handyman, double>();
            closestHandymanWithRadius.Add(closestHandyman, distance);
            return closestHandymanWithRadius;

        }

        public Dictionary<Handyman, double> GetHandymanInRadius(DistanceProcess distanceProcess, double distance)
        {
            Dictionary<Handyman, double> handymenInRadius = new Dictionary<Handyman, double>();

            foreach (KeyValuePair<Handyman, double> kvp in distanceProcess.handymenWithRadius)
            {
                if (kvp.Value/1000 <= distance)
                {
                    handymenInRadius.Add(kvp.Key, kvp.Value/1000);
                }
            }

            return handymenInRadius;
        }

        public Handyman GetHandymanByUsername(DistanceProcess distanceProcess, string username)
        {
            foreach (KeyValuePair<Handyman, double> kvp in distanceProcess.handymenWithRadius)
            {
                if (kvp.Key.Username == username)
                {
                    return kvp.Key;
                }

            }
            throw new ArgumentException ("Handyman not in database");

        }
        public Dictionary<Handyman, double> GetHandymanBySpecialization(DistanceProcess distanceProcess, string specialization)
        {
            Dictionary<Handyman, double> handymenBySpecialization = new Dictionary<Handyman, double>();

            foreach (KeyValuePair<Handyman, double> kvp in distanceProcess.handymenWithRadius)
            {
                if (kvp.Key.Specialization == specialization)
                {
                    handymenBySpecialization.Add(kvp.Key, kvp.Value / 1000);
                }
            }

            return handymenBySpecialization;

        }
    }
}
