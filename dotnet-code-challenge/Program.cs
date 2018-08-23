using dotnet_code_challenge.FeedDataLoader;
using dotnet_code_challenge.Models;
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

                PrintHorsePrice(aHourseRace.Track, aHorcesByPriceAsc);
            }

            Console.ReadLine();
        }

        static void PrintHorsePrice(string theTrack, IList<Horse> theHorses)
        {
            Console.WriteLine("Horses in track:{0} in price ascending order:", theTrack);
            for (var aOrder = 0; aOrder < theHorses.Count; aOrder++)
            {
                var aHorse = theHorses[aOrder];
                Console.WriteLine("{0}, Name:{1}, Price:{2}", aOrder + 1, aHorse.Name, aHorse.Price);
            }

            Console.WriteLine("");
        }
    }
}
