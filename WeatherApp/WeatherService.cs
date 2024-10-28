using System;

namespace WeatherApp
{
    public class WeatherService
    {
        public string GetWeatherCondition()
        {
            // Code to retrieve weather condition from an external API or data source
            // Replace this with your actual implementation

            // For demonstration purposes, return a random weather condition
            string[] weatherConditions = { "rainy", "sunny", "foggy", "cloudy", "storm", "overcast" };
            Random random = new Random();
            int index = random.Next(weatherConditions.Length);
            return weatherConditions[index];
        }

        public string GetBackgroundImage(string weatherCondition)
        {
            // Code to determine the background image based on the weather condition
            // Replace this with your actual implementation

            switch (weatherCondition)
            {
                case "rainy":
                    return "Resources/Images/rainy.png";
                case "sunny":
                    return "Resources/Images/sunny.png";
                case "foggy":
                    return "Resources/Images/foggy.png";
                case "cloudy":
                    return "Resources/Images/cloudy.png";
                case "storm":
                    return "Resources/Images/storm.png";
                case "overcast":
                    return "Resources/Images/overcast.png";
                default:
                    return string.Empty;
            }
        }
    }
}