using dotnet_code_challenge.Models.RaceFeeds;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace dotnet_code_challenge
{
    public static class RawRaceFeedDataProvider
    {
        public static RawRaceFeed CreateRawRaceFeed(string theFilePath)
        {
            if (string.IsNullOrEmpty(theFilePath))
            {
                throw new ArgumentNullException(theFilePath);
            }

            var aFullPath = string.Format(
                "{0}{1}", 
                AppDomain.CurrentDomain.BaseDirectory, 
                theFilePath.TrimStart('\\'));

            if (!File.Exists(aFullPath))
            {
                throw new FileNotFoundException(aFullPath);
            }

            var aMemoryStream = new MemoryStream();
            using (var aFileStream = new FileStream(aFullPath, FileMode.Open, FileAccess.Read))
            {
                aFileStream.CopyTo(aMemoryStream);
            }

            var aFile = new FileInfo(aFullPath);
            var aRawRaceFeed = new RawRaceFeed
            {
                FileName = aFile.Name,
                FeedStream = aMemoryStream
            };

            return aRawRaceFeed;
        }
    }
}
