using System;
using HttpHelper;
using RestSharp;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0 || args[0].Equals("-h") || args[0].Equals("--help"))
        {
            DisplayHelp();
            return;
        }
        RestClientHelper _RestClientHelper = new RestClientHelper("https://quotesapi.divsphere.net");
        
        string endpoint = "";
        int sleepTime = 3000; // Default sleep time

        for (int i = 0; i < args.Length; i++)
        {
            switch (args[i].ToLower())
            {
                case "-q":
                    endpoint = "quotes";
                    break;
                case "-r":
                    endpoint = "rizzes";
                    break;
                case "-i":
                    endpoint = "insults";
                    break;
                case "-t":
                    if (i + 1 < args.Length)
                    {
                        if (int.TryParse(args[i + 1], out int time))
                        {
                            sleepTime= int.Parse(time.ToString() + "000");
                            i++; // Move to the next argument as it's the value of -t
                        }
                        else
                        {
                            Console.WriteLine("Invalid time value provided after -t flag.");
                            return;
                        }
                    }
                    else
                    {
                        Console.WriteLine("No time value provided after -t flag.");
                        return;
                    }
                    break;
                default:
                    Console.WriteLine($"This option is not implemented yet. Please use -q, -r, or -i.");
                    return;
            }
        }

        Loop(endpoint, sleepTime, _RestClientHelper);
    }

    static string? GetRandomSaying(string endpoint, RestClientHelper restClientHelper)
    {
        RestResponse response = restClientHelper.ExecuteGetRequest(endpoint);
        string? saying = response.Content;
        return saying;
    }

    static void Loop(string endpoint, int sleepTime, RestClientHelper restClientHelper)
    {
        while (true)
        {
            string? response = GetRandomSaying(endpoint, restClientHelper);
            Console.WriteLine(response);
            Thread.Sleep(sleepTime);
            Console.Clear();
        }
    }
    static void DisplayHelp()
    {
        Console.WriteLine("Usage: RandomSayings [OPTIONS]");
        Console.WriteLine("Options:");
        Console.WriteLine("-q\t\tGet random quotes");
        Console.WriteLine("-r\t\tGet random rizzes");
        Console.WriteLine("-i\t\tGet random insults");
        Console.WriteLine("-t <time>\tSet the sleep time in seconds (default: 3 seconds)");
        Console.WriteLine("-h, --help\tDisplay this help message");
    }
}
