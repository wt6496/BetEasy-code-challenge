﻿using dotnet_code_challenge.FeedDataLoader;
using dotnet_code_challenge.Models;
using dotnet_code_challenge.Models.RaceFeeds;
using System;
using System.Collections.Generic;
using System.Text;

namespace dotnet_code_challenge
{
    public class HorseRaceDataProvider
    {
        private readonly ICollection<IFeedDataLoader> feedDataLoaders;

        public HorseRaceDataProvider(
            ICollection<IFeedDataLoader> theFeedDataLoaders)
        {
            feedDataLoaders = theFeedDataLoaders;
        }

        public HorseRace GetHorseRace(RawRaceFeed theRawRaceFeed)
        {
            foreach (var aFeedDataLoader in feedDataLoaders)
            {
                object aDeserializedObj;
                if (aFeedDataLoader.CanHandle(theRawRaceFeed, out aDeserializedObj))
                {
                    return aFeedDataLoader.GetHorseRace(aDeserializedObj);
                }
            }

            return null;
        }
    }
}
