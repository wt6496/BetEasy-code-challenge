using System;
using System.Collections.Generic;
using System.Text;
using dotnet_code_challenge.Models;
using dotnet_code_challenge.Models.RaceFeeds;

namespace dotnet_code_challenge.FeedDataLoader
{
    public class WolferhamptonFeedDataLoader : IFeedDataLoader
    {
        public bool CanHandle(RawRaceFeed theRawRaceFeed)
        {
            throw new NotImplementedException();
        }

        public HorseRace GetHorseRace(RawRaceFeed theRawRaceFeed)
        {
            throw new NotImplementedException();
        }
    }
}
