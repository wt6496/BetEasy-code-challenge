using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using dotnet_code_challenge.Models;
using dotnet_code_challenge.Models.RaceFeeds;
using Newtonsoft.Json;

namespace dotnet_code_challenge.FeedDataLoader
{
    public class WolferhamptonFeedDataLoader : IFeedDataLoader
    {
        private const string HORSE_RACING = "HorseRacing";
        private const string WOLVERHAMPTON = "Wolverhampton";
        private JsonSerializer jsonSerializer = new JsonSerializer();

        public bool CanHandle(RawRaceFeed theRawRaceFeed)
        {
            if (theRawRaceFeed == null ||
                theRawRaceFeed.FileName == null ||
                theRawRaceFeed.FeedStream == null)
            {
                return false;
            }

            if (!theRawRaceFeed.FileName.EndsWith("json", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            var aWolferhamptonRaceFeed = GetWolferhamptonRaceFeed(theRawRaceFeed.FeedStream);
            if (aWolferhamptonRaceFeed == null)
            {
                return false;
            }

            return aWolferhamptonRaceFeed.RawData != null &&
                aWolferhamptonRaceFeed.RawData.Tags != null &&
                !string.IsNullOrEmpty(aWolferhamptonRaceFeed.RawData.Tags.Sport) &&
                aWolferhamptonRaceFeed.RawData.Tags.Sport.Equals(HORSE_RACING, StringComparison.OrdinalIgnoreCase) &&
                !string.IsNullOrEmpty(aWolferhamptonRaceFeed.RawData.Tags.Track) &&
                aWolferhamptonRaceFeed.RawData.Tags.Track.Equals(WOLVERHAMPTON, StringComparison.OrdinalIgnoreCase);
        }

        public HorseRace GetHorseRace(RawRaceFeed theRawRaceFeed)
        {
            var aWolferhamptonRaceFeed = GetWolferhamptonRaceFeed(theRawRaceFeed.FeedStream);
            if (aWolferhamptonRaceFeed == null ||
                aWolferhamptonRaceFeed.RawData == null ||
                aWolferhamptonRaceFeed.RawData.Markets == null ||
                aWolferhamptonRaceFeed.RawData.Markets.Length == 0 ||
                aWolferhamptonRaceFeed.RawData.Markets[0].Selections == null ||
                aWolferhamptonRaceFeed.RawData.Markets[0].Selections.Length == 0)
            {
                return null;
            }

            var aHourseRace = new HorseRace
            {
                Track = aWolferhamptonRaceFeed.RawData?.Tags?.Track,
                Horses = new List<Horse>()
            };

            foreach (var aSelection in aWolferhamptonRaceFeed.RawData.Markets[0].Selections)
            {
                if (aSelection.Tags == null ||
                    string.IsNullOrEmpty(aSelection.Tags.Name))
                {
                    continue;
                }
                
                var aHorse = new Horse
                {
                    Name = aSelection.Tags.Name,
                    Price = aSelection.Price
                };
                aHourseRace.Horses.Add(aHorse);
            }

            return aHourseRace;
        }

        public WolferhamptonRaceFeed GetWolferhamptonRaceFeed(MemoryStream theFeedStream)
        {
            try
            {
                theFeedStream.Seek(0, SeekOrigin.Begin);
                using (var aStreamReader = new StreamReader(theFeedStream, Encoding.UTF8, false, 1024, true))
                {
                    using (var aJsonTextReader = new JsonTextReader(aStreamReader))
                    {
                        return jsonSerializer.Deserialize<WolferhamptonRaceFeed>(aJsonTextReader);
                    }
                }
                
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
