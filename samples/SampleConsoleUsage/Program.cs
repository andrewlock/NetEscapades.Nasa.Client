using System;
using System.Linq;
using NetEscapades.Nasa;

namespace SampleConsoleUsage
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new NasaImageClient();

            Console.WriteLine("Enter your search term");
            var searchterm = Console.ReadLine();

            Console.WriteLine("Contacting NASA image client...");

            var result = client.Search(searchterm).Result;

            if (result.IsSuccess)
            {
                Console.WriteLine("Query successful. Found the following items: ");
                foreach (var item in result.Data.Items)
                {
                    Console.WriteLine(item.Title);
                }

                if (result.Data.TotalCount > 0)
                {
                    Console.WriteLine("");
                    Console.WriteLine("Fetching metadata for first asset:");
                    var nasaId = result.Data.Items.Skip(3).First().NasaId;
                    var metadataResult = client.GetAssetMetadata(nasaId).Result;
                    if (result.IsSuccess)
                    {
                        Console.WriteLine(metadataResult.Data.ToString());
                    }
                    else
                    {
                        Console.WriteLine("There was an error calling the server:");
                        Console.WriteLine(result.ErrorMessage);
                    }

                    Console.WriteLine("");
                    Console.WriteLine("Fetching asset list for first asset:");
                    var manifestList = client.GetAssetManifest(nasaId).Result;
                    if (result.IsSuccess)
                    {
                        foreach (var asset in manifestList.Data)
                        {
                            Console.WriteLine(asset);
                        }
                    }
                    else
                    {
                        Console.WriteLine("There was an error calling the server:");
                        Console.WriteLine(result.ErrorMessage);
                    }

                    Console.WriteLine("");
                    Console.WriteLine("Fetching captions for first asset:");
                    var captions = client.GetAssetCaptions(nasaId).Result;
                    if (captions.IsSuccess)
                    {
                        Console.WriteLine(captions.Data.Location);
                        Console.WriteLine(captions.Data.MimeType);
                        Console.WriteLine(captions.Data.Captions);
                    }
                    else
                    {
                        Console.WriteLine("There was an error calling the server:");
                        Console.WriteLine(captions.ErrorMessage);
                    }
                }
            }
            else
            {
                Console.WriteLine("There was an error calling the server");
                Console.WriteLine(result.ErrorMessage);
            }



            //get the recent items
            var recentAssets = client.GetRecentAssetIds().Result;

            foreach (var asset in recentAssets.Data)
            {
                Console.WriteLine(asset);
            }


            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }
    }
}