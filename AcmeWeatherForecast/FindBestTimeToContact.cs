using System;
using static AcmeWeatherForecast.JsonClasses;

namespace AcmeWeatherForecast
{
    /// <summary>
    /// There are some issues with the requirements being not clear so I tried to address them and put the confusion into the output messages
    /// 1 - What to do if over 75 degrees and NOT sunny i.e. clear
    /// 2 - with multiple data points it can be Sunny and cloudy or Clear and Rainy on the same day.  I choose to set variable if it is Rainy or Clear anytime during the day
    /// </summary>
    public static class BestTimeToContact
    {

        /// <summary>
        /// Per the requirements find the Best time to engage and the print it out to the Console
        /// </summary>
        /// <param name="deSerializedResponseString"></param>
        public static void FindAndPrintBestTimeToContact(Root deSerializedResponseString)
        {
            string dateWeAreProcessing = null;
            int temperatureDataPointCount = 0;
            double averageOfTemps = 0;
            double feelsLike = 0.0;
            double tempMin = 0.0;
            double tempMax = 0.0;
            double temp = 0.0;
            bool isSunny = false;
            bool isRainy = false;

            foreach (var item in deSerializedResponseString.list)
            {
                if(dateWeAreProcessing == null)
                {
                    dateWeAreProcessing = item.dt_txt;
                }
                else if (!dateWeAreProcessing.Contains(item.dt_txt.Substring(0,10)))
                {
                    averageOfTemps = (feelsLike + tempMin + tempMax + temp)/(temperatureDataPointCount * 4);
                    Console.WriteLine("{0} Avg Temp = {1} and best engagement type = {2}", item.dt_txt.Substring(0, 10), averageOfTemps, FindBestEngagementType(averageOfTemps, isSunny, isRainy));
                    dateWeAreProcessing = item.dt_txt;
                    temperatureDataPointCount = 0;
                    averageOfTemps = 0;
                    feelsLike = 0.0;
                    tempMin = 0.0;
                    tempMax = 0.0;
                    temp = 0.0;
                    isSunny = false;
                    isRainy = false;
                }

                temperatureDataPointCount++;
                feelsLike += item.main.feels_like;
                tempMin += item.main.temp_min;
                tempMax += item.main.temp_max;
                temp += item.main.temp;

                foreach(var weatherItem in item.weather)
                {
                    var bogus = weatherItem.main; 
                    if (weatherItem.main.Equals("Clear"))
                    {
                        isSunny = true;
                    }
                    else if(weatherItem.main.Equals("Rain"))
                    {
                        isRainy = true;
                    }
                }
            }
        }

        /// <summary>
        /// Find the best engagement type per the requirements and return the string
        /// </summary>
        /// <param name="averageOfTemps"></param>
        /// <param name="isSunny"></param>
        /// <param name="isRainy"></param>
        /// <returns></returns>
        public static string FindBestEngagementType(double averageOfTemps, bool isSunny, bool isRainy)
        {
            string bestEngagementType = "";

            if(isRainy)
            {
                bestEngagementType = "Phone Call - Raining";
            }
            else if ((averageOfTemps > 75.0) && isSunny)
            {
                bestEngagementType = "Text Message - over 75 and Sunny";
            }
            else if ((averageOfTemps > 75.0) && !isSunny)
            {
                bestEngagementType = "Fuzzy requirements - over 75 but NOT sunny";
            }
            else if (averageOfTemps < 55.0)
            {
                bestEngagementType = "Phone Call - less than 55 degrees";
            }
            else
            {
                bestEngagementType = "Email - between 55 and 75";
            }

            return bestEngagementType;
        }
    }
}
