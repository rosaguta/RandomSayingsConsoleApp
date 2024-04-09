using System;
using System.Diagnostics;
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
        bool rainbowmode = false;

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
                case "--rainbow":
                    rainbowmode = true;
                    Debug.Write("Rainbow activated");
                    break;
                default:
                    Console.WriteLine($"This option is not implemented yet. Please use -q, -r, or -i.");
                    return;
            }
        }
        
        if(!rainbowmode){
        Loop(endpoint, sleepTime, _RestClientHelper);
        }
        else
        {
            RainbowLoop(endpoint, sleepTime, _RestClientHelper);
        }
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

    static void RainbowLoop(string endpoint, int sleepTime, RestClientHelper restClientHelper)
    {
        while (true)
        {
            string? response = GetRandomSaying(endpoint, restClientHelper);
            ConsoleWriteRainbow(response);
            Thread.Sleep(sleepTime);
            Console.Clear();
        }
    }

    static void ConsoleWriteRainbow(string? text)
    {
        // Rainbow colors
        ConsoleColor[] colors = {
            ConsoleColor.Red,
            ConsoleColor.Yellow,
            ConsoleColor.Green,
            ConsoleColor.Cyan,
            ConsoleColor.Blue,
            ConsoleColor.Magenta
        };

        int colorIndex = 0;

        // Print text with rainbow coloring
        foreach (char c in text)
        {
            Console.ForegroundColor = colors[colorIndex];
            Console.Write(c);

            colorIndex = (colorIndex + 1) % colors.Length;
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
