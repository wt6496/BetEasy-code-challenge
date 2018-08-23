using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using dotnet_code_challenge.Models;
using dotnet_code_challenge.Models.RaceFeeds;
using System.Xml.Serialization;

namespace dotnet_code_challenge.FeedDataLoader
{
    public class CaulfieldFeedDataLoader : IFeedDataLoader
    {
        private const string HORSE_RACING = "HorseRacing";
        private const string CAULFIELD = "Caulfield";
        private XmlSerializer xmlSerializer = new XmlSerializer(typeof(CaulfieldRaceFeed));

        public bool CanHandle(RawRaceFeed theRawRaceFeed, out object theDeserializedObj)
        {
            theDeserializedObj = null;
            if (theRawRaceFeed == null || 
                theRawRaceFeed.FileName == null || 
                theRawRaceFeed.FeedStream == null)
            {
                return false;
            }

            if (!theRawRaceFeed.FileName.EndsWith("xml", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            var aCaulfieldRaceFeed = GetCaulfieldRaceFeed(theRawRaceFeed.FeedStream);
            if (aCaulfieldRaceFeed == null)
            {
                return false;
            }

            theDeserializedObj = aCaulfieldRaceFeed;
            return !string.IsNullOrEmpty(aCaulfieldRaceFeed.MeetingType) &&
                   aCaulfieldRaceFeed.MeetingType.Equals(HORSE_RACING, StringComparison.OrdinalIgnoreCase) &&
                   aCaulfieldRaceFeed.Track != null &&
                   !string.IsNullOrEmpty(aCaulfieldRaceFeed.Track.Name) &&
                   aCaulfieldRaceFeed.Track.Name.Equals(CAULFIELD, StringComparison.OrdinalIgnoreCase);
        }

        public HorseRace GetHorseRace(object theDeserializedObj)
        {
            var aCaulfieldRaceFeed = theDeserializedObj as CaulfieldRaceFeed;
            if (aCaulfieldRaceFeed == null || 
                aCaulfieldRaceFeed.Races == null ||
                aCaulfieldRaceFeed.Races.Length == 0 ||
                aCaulfieldRaceFeed.Races[0].Horses == null ||
                aCaulfieldRaceFeed.Races[0].Horses.Length == 0 ||
                aCaulfieldRaceFeed.Races[0].Prices == null ||
                aCaulfieldRaceFeed.Races[0].Prices.Length == 0 ||
                aCaulfieldRaceFeed.Races[0].Prices[0].HorsePrices == null ||
                aCaulfieldRaceFeed.Races[0].Prices[0].HorsePrices.Length == 0)
            {
                return null;
            }

            var aHourseRace = new HorseRace
            {
                Track = aCaulfieldRaceFeed.Track?.Name,
                Horses = new List<Horse>()
            };

            var aHorseNumberPriceMapping = new Dictionary<int, decimal>();
            foreach (var aHorsePrices in aCaulfieldRaceFeed.Races[0].Prices[0].HorsePrices)
            {
                aHorseNumberPriceMapping[aHorsePrices.Number] = aHorsePrices.Price;
            }

            foreach (var aCaulfieldHorse in aCaulfieldRaceFeed.Races[0].Horses)
            {
                if (aCaulfieldHorse.Name == null)
                {
                    continue;
                }

                var aHorse = new Horse { Name = aCaulfieldHorse.Name };
                aHourseRace.Horses.Add(aHorse);

                decimal aPrice;
                if (aHorseNumberPriceMapping.TryGetValue(aCaulfieldHorse.Number, out aPrice))
                {
                    aHorse.Price = aPrice;
                }
            }

            return aHourseRace;
        }

        private CaulfieldRaceFeed GetCaulfieldRaceFeed(MemoryStream theFeedStream)
        {
            try
            {
                theFeedStream.Seek(0, SeekOrigin.Begin);
                var aResult = (CaulfieldRaceFeed)xmlSerializer.Deserialize(theFeedStream);
                return aResult;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
