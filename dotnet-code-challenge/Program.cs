using dotnet_code_challenge.FeedDataLoader;
using System;
using System.Collections.Generic;
using System.Linq;

namespace dotnet_code_challenge
{
    class Program
    {
        static void Main(string[] args)
        {
            var aSources = new List<string>();
            aSources.Add(@"FeedData\Caulfield_Race1.xml");
            aSources.Add(@"FeedData\Wolferhampton_Race1.json");

            var aFeedDataLoaders = new List<IFeedDataLoader>();
            aFeedDataLoaders.Add(new CaulfieldFeedDataLoader());
            aFeedDataLoaders.Add(new WolferhamptonFeedDataLoader());
            var aHorseRaceDataProvicer = new HorseRaceDataProvider(aFeedDataLoaders);

            foreach (var aSource in aSources)
            {
                var aRawRaceFeed = RawRaceFeedDataProvider.CreateRawRaceFeed(aSource);
                if (aRawRaceFeed == null)
                {
                    continue;
                }

                var aHourseRace = aHorseRaceDataProvicer.GetHorseRace(aRawRaceFeed);
                if (aHourseRace == null)
                {
                    continue;
                }

                var aHorces = aHourseRace.Horses;
                if (aHorces == null)
                {
                    continue;
                }

                var aHorcesByPriceAsc = aHorces.OrderBy(theHorce => theHorce.Price).ToList();

                // TODO: print the aHorcesByPriceAsc
            }

            Console.WriteLine("Hello World!");
        }
    }
}
