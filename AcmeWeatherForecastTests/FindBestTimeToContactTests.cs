using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AcmeWeatherForecastTests
{
    [TestClass]
    public class FindBestTimeToContactTests
    {
        /// <summary>
        /// The requirements are a bit hazy, but the way I read them, if it is Raining thats takes top precedence
        /// If it is over 75 but NOT sunny, it doesn't say what to do. I return "Fuzzying requirements"
        /// </summary>

        [TestMethod]
        public void RainTakesTopPrecedence()
        {
            bool isSunny = true;
            bool isRaining = true;

            string engagementType = AcmeWeatherForecast.BestTimeToContact.FindBestEngagementType(76.0, isSunny, isRaining);
            Assert.AreEqual("Phone Call - Raining", engagementType);
        }

        [TestMethod]
        public void TempOver75AndSunny()
        {
            bool isSunny = true;
            bool isRaining = false;

            string engagementType = AcmeWeatherForecast.BestTimeToContact.FindBestEngagementType(76.0, isSunny, isRaining);
            Assert.AreEqual("Text Message - over 75 and Sunny", engagementType);
        }

        [TestMethod]
        public void TempOver75AndNotSunny()
        {
            bool isSunny = false;
            bool isRaining = false;

            string engagementType = AcmeWeatherForecast.BestTimeToContact.FindBestEngagementType(76.0, isSunny, isRaining);
            Assert.AreEqual("Fuzzy requirements - over 75 but NOT sunny", engagementType);
        }

        [TestMethod]
        public void TempIs75AndSunny()
        {
            bool isSunny = true;
            bool isRaining = false;

            string engagementType = AcmeWeatherForecast.BestTimeToContact.FindBestEngagementType(75.0, isSunny, isRaining);
            Assert.AreEqual("Email - between 55 and 75", engagementType);
        }

        [TestMethod]
        public void TempIs55AndSunny()
        {
            bool isSunny = true;
            bool isRaining = false;

            string engagementType = AcmeWeatherForecast.BestTimeToContact.FindBestEngagementType(55.0, isSunny, isRaining);
            Assert.AreEqual("Email - between 55 and 75", engagementType);
        }

        [TestMethod]
        public void TempIs54AndSunny()
        {
            bool isSunny = true;
            bool isRaining = false;

            string engagementType = AcmeWeatherForecast.BestTimeToContact.FindBestEngagementType(54.0, isSunny, isRaining);
            Assert.AreEqual("Phone Call - less than 55 degrees", engagementType);
        }

        [TestMethod]
        public void TempIs54AndRaining()
        {
            bool isSunny = true;
            bool isRaining = true;

            string engagementType = AcmeWeatherForecast.BestTimeToContact.FindBestEngagementType(54.0, isSunny, isRaining);
            Assert.AreEqual("Phone Call - Raining", engagementType);
        }

    }
}
