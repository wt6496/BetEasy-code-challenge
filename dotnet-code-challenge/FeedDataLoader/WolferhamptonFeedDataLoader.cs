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

            return false;
        }

        public HorseRace GetHorseRace(RawRaceFeed theRawRaceFeed)
        {
            throw new NotImplementedException();
        }

        public WolferhamptonRaceFeed GetWolferhamptonRaceFeed(MemoryStream theFeedStream)
        {
            try
            {
                var aSerializer = new JsonSerializer();

                using (var aStreamReader = new StreamReader(theFeedStream))
                {
                    using (var aJsonTextReader = new JsonTextReader(aStreamReader))
                    {
                        return aSerializer.Deserialize<WolferhamptonRaceFeed>(aJsonTextReader);
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
