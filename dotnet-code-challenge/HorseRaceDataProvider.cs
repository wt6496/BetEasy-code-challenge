using dotnet_code_challenge.FeedDataLoader;
using dotnet_code_challenge.Models;
using dotnet_code_challenge.Models.RaceFeeds;
using System;
using System.Collections.Generic;
using System.Text;

namespace dotnet_code_challenge
{
    public class HorseRaceDataProvider
    {
        public HorseRaceDataProvider(
            ICollection<IFeedDataLoader> theFeedDataLoaders)
        {
        }

        public HorseRace GetHorseRace(RawRaceFeed theRawRaceFeed)
        {
            throw new NotImplementedException();
        }
    }
}
