using dotnet_code_challenge.FeedDataLoader;
using dotnet_code_challenge.Models.RaceFeeds;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace dotnet_code_challenge.Test
{
    public class HorseRaceDataProviderUnitTests
    {
        [Fact]
        public void TestCaulfieldFeed()
        {
            var aFeedDataLoaders = new List<IFeedDataLoader>();
            aFeedDataLoaders.Add(new CaulfieldFeedDataLoader());
            var aHorseRaceDataProvicer = new HorseRaceDataProvider(aFeedDataLoaders);

            var aRawData = "<?xml version=\"1.0\"?> <meeting xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"> <MeetingType>HorseRacing</MeetingType> <track name=\"Caulfield\"/> <races> <race> <horses> <horse name=\"Advancing\"> <number>1</number> </horse> <horse name=\"Coronel\"> <number>2</number> </horse> </horses> <prices> <price> <horses> <horse number=\"1\" Price=\"4.2\"/> <horse number=\"2\" Price=\"12\"/> </horses> </price> </prices> </race> </races> </meeting>";
            var aRawRaceFeed = new RawRaceFeed
            {
                FileName = "Caulfield_Race1.xml",
                FeedStream = new MemoryStream(Encoding.UTF8.GetBytes(aRawData))
            };

            var aHorseRace = aHorseRaceDataProvicer.GetHorseRace(aRawRaceFeed);
            Assert.NotNull(aHorseRace);
            Assert.Equal("Caulfield", aHorseRace.Track);
            Assert.Equal(2, aHorseRace.Horses.Count);
            Assert.NotNull(aHorseRace.Horses.FirstOrDefault(theHorse => theHorse.Name.Equals("Advancing")));
            Assert.NotNull(aHorseRace.Horses.FirstOrDefault(theHorse => theHorse.Name.Equals("Coronel")));
            Assert.Equal((decimal)4.2, aHorseRace.Horses.FirstOrDefault(theHorse => theHorse.Name.Equals("Advancing")).Price);
            Assert.Equal(12, aHorseRace.Horses.FirstOrDefault(theHorse => theHorse.Name.Equals("Coronel")).Price);

            var aHorcesByPriceAsc = aHorseRace.Horses.OrderBy(theHorce => theHorce.Price).ToList();

            Assert.Equal("Advancing", aHorcesByPriceAsc[0].Name);
            Assert.Equal("Coronel", aHorcesByPriceAsc[1].Name);
        }

        [Fact]
        public void TestWolferhamptonFeed()
        {
            var aFeedDataLoaders = new List<IFeedDataLoader>();
            aFeedDataLoaders.Add(new WolferhamptonFeedDataLoader());
            var aHorseRaceDataProvicer = new HorseRaceDataProvider(aFeedDataLoaders);

            var aRawData = "{ \"RawData\": { \"Tags\": { \"TrackCode\": \"Wolverhampton\", \"Sport\": \"HorseRacing\" }, \"Markets\": [ { \"Selections\": [ { \"Price\": 10.0, \"Tags\": { \"name\": \"Toolatetodelegate\" } }, { \"Price\": 4.4, \"Tags\": { \"name\": \"Fikhaar\" } } ] } ] } } ";
            var aRawRaceFeed = new RawRaceFeed
            {
                FileName = "Wolferhampton_Race1.json",
                FeedStream = new MemoryStream(Encoding.UTF8.GetBytes(aRawData))
            };

            var aHorseRace = aHorseRaceDataProvicer.GetHorseRace(aRawRaceFeed);
            Assert.NotNull(aHorseRace);
            Assert.Equal("Wolverhampton", aHorseRace.Track);
            Assert.Equal(2, aHorseRace.Horses.Count);
            Assert.NotNull(aHorseRace.Horses.FirstOrDefault(theHorse => theHorse.Name.Equals("Fikhaar")));
            Assert.NotNull(aHorseRace.Horses.FirstOrDefault(theHorse => theHorse.Name.Equals("Toolatetodelegate")));
            Assert.Equal((decimal)4.4, aHorseRace.Horses.FirstOrDefault(theHorse => theHorse.Name.Equals("Fikhaar")).Price);
            Assert.Equal((decimal)10.0, aHorseRace.Horses.FirstOrDefault(theHorse => theHorse.Name.Equals("Toolatetodelegate")).Price);

            var aHorcesByPriceAsc = aHorseRace.Horses.OrderBy(theHorce => theHorce.Price).ToList();

            Assert.Equal("Fikhaar", aHorcesByPriceAsc[0].Name);
            Assert.Equal("Toolatetodelegate", aHorcesByPriceAsc[1].Name);
        }
    }
}
