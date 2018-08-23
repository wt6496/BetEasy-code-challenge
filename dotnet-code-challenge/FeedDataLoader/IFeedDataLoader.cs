using dotnet_code_challenge.Models;
using dotnet_code_challenge.Models.RaceFeeds;
using System;
using System.Collections.Generic;
using System.Text;

namespace dotnet_code_challenge.FeedDataLoader
{
    public interface IFeedDataLoader
    {
        bool CanHandle(RawRaceFeed theRawRaceFeed, out object theDeserializedObj);

        HorseRace GetHorseRace(object theDeserializedObj);
    }
}
